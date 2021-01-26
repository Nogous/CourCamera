using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    [Range(-180, 180)]
    public float roll = 0f;

    [Range(1, 179)]
    public float fov = 60;

    public Vector3 target = new Vector3();

    public GameObject centralPoint;

    public float yawOffsetMax = 10f;
    public float pitchOffsetMax = 10f;

    public override CameraConfiguration GetConfiguration()
    {
        Vector3 dir = target - transform.position;

        config.yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        config.pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        config.roll = roll;
        config.pivot = transform.position;
        config.distance = 0;
        config.fov = fov;


        Vector3 dirCenter = centralPoint.transform.position;

        float yawCenter = Mathf.Atan2(dirCenter.x, dirCenter.z) * Mathf.Rad2Deg;
        if (yawCenter >= 0)
        {
            if (config.yaw > yawCenter)
            {
                config.yaw = yawCenter;
            }
        }
        else
        {
            if (config.yaw < yawCenter)
            {
                config.yaw = yawCenter;
            }
        }

        float pitchCenter = -Mathf.Asin(dirCenter.y) * Mathf.Rad2Deg;
        if (pitchCenter >= 0)
        {
            if (config.yaw > pitchCenter)
            {
                config.yaw = pitchCenter;
            }
        }
        else
        {
            if (config.yaw < pitchCenter)
            {
                config.yaw = pitchCenter;
            }
        }


        return config;
    }
}
