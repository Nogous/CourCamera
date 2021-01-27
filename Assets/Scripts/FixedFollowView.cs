using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    [Range(-180, 180)]
    public float roll = 0f;

    [Range(1, 179)]
    public float fov = 60;

    public GameObject target;
    public GameObject centralPoint;

    public float yawOffsetMax = 10f;
    public float pitchOffsetMax = 10f;

    public override CameraConfiguration GetConfiguration()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;

        config.yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        config.pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        config.roll = roll;
        config.pivot = transform.position;
        config.distance = 0;
        config.fov = fov;


        Vector3 dirCenter = (centralPoint.transform.position - transform.position).normalized;

        float yawCenter = Mathf.Atan2(dirCenter.x, dirCenter.z) * Mathf.Rad2Deg;

        float dif = config.yaw - yawCenter;

        if (dif < -180)
        {
            dif += 360;
        }
        if (dif > 180)
        {
            dif -= 360;
        }

        //Debug.Log(con);

        if (dif > yawOffsetMax)
        {
            config.yaw = yawCenter + yawOffsetMax;
        }
        else if (dif < -yawOffsetMax)
        {
            config.yaw = yawCenter - yawOffsetMax;
        }

        float pitchCenter = -Mathf.Asin(dirCenter.y) * Mathf.Rad2Deg;

        float dif2 = pitchCenter - config.pitch;

        if (dif2 < -180)
        {
            dif2 += 360;
        }
        if (dif2 > 180)
        {
            dif2 -= 360;
        }

        if (dif2 > pitchOffsetMax)
        {
            config.pitch = pitchCenter + pitchOffsetMax;
        }
        else if (dif2 < -pitchOffsetMax)
        {
            config.pitch = pitchCenter - pitchOffsetMax;
        }


        return config;
    }
}
