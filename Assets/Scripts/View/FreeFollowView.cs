using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFollowView : AView
{
    public float[] pitch;
    public float[] roll;
    public float[] fov;

    public float yaw;
    public float yawSpeed;

    public GameObject target;
    public Curve curve;
    public float curvePosition;
    public float curveSpeed;

    private float fieldOfView;
    private float _roll;
    private float _pitch;
    private Vector3 positionCamera;

    // Start is called before the first frame update
    void Start()
    {
        SetActive(true);
    }

    public override void SetActive(bool isActive)
    {
        if (isActive)
        {
            CameraController.instance.AddView(this);
        }
        else
        {
            CameraController.instance.RemoveView(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
        yaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime * Mathf.Rad2Deg;

        curvePosition += Input.GetAxis("Vertical") * curveSpeed * Time.deltaTime;
        curvePosition = Mathf.Clamp01(curvePosition);

        _pitch = Lerp(pitch, curvePosition);
        _roll = Lerp(roll, curvePosition);
        fieldOfView = Lerp(fov, curvePosition);

        positionCamera = curve.GetPosition(curvePosition, Matrix4x4.TRS(transform.position, Quaternion.Euler(0f, yaw, 0f), Vector3.one));
    }

    private void OnDrawGizmos()
    {
        curve.DrawGizmo(Color.red, Matrix4x4.TRS(transform.position, Quaternion.Euler(0f, yaw, 0f), Vector3.one));
    }

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();
        config.yaw = yaw;
        config.pitch = _pitch;
        config.roll = _roll;
        config.fov = fieldOfView;
        config.pivot = positionCamera;
        config.distance = 0;

        return config;
    }

    private float Lerp(float[] list, float position)
    {
        if (position <= 0.5)
        {
            return Mathf.Lerp(list[0], list[1], position * 2);
        }
        else
        {
            return Mathf.Lerp(list[1], list[2], (position - 0.5f) * 2);
        }
    }
}
