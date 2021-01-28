using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFollowView : AView
{
    public float[] pitch = new float[3];
    public float[] roll = new float[3];
    public float[] fov = new float[3];

    [HideInInspector]public float yaw;
    public float yawSpeed;

    public GameObject target;

    public Curve curve;
    public Vector3 curvePosition;
    public float curveSpeed;

    private float camYaw;
    private float camPitch;
    private float camRoll;
    private float camFov;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(pitch[1], -yaw*Mathf.Rad2Deg +90, roll[1]);
    }
}
