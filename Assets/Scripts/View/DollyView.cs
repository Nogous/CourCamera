using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyView : AView
{
    [Range(-180, 180)]
    public float roll = 0f;

    [Range(1, 179)]
    public float fov = 60;

    public float distance = 0;

    public GameObject target;

    public Rail rail;

    private float railPosition = 0f;

    public float speed = 1f;

    public bool isAuto = false;

    private void Update()
    {
        if (isAuto)
            railPosition = rail.NearPosOnRail(target.transform.position);
        else
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                railPosition += Time.deltaTime * speed;
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                railPosition -= Time.deltaTime * speed;
            }
        }

    }

    public override CameraConfiguration GetConfiguration()
    {
            config.pivot = rail.GetPosition(railPosition);

        Vector3 dir = (target.transform.position - config.pivot).normalized;

        config.yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        config.pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        config.roll = roll;
        config.distance = 0;
        config.fov = fov;

        return config;
    }

}
