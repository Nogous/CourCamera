using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedView : AView
{
    [Range(0, 360)]
    public float yaw = 0f;

    [Range(-90, 90)]
    public float pitch = 0f;

    [Range(-180, 180)]
    public float roll = 0f;

    [Range(1, 179)]
    public float fov = 60;

    public override CameraConfiguration GetConfiguration()
    {
        config = new CameraConfiguration();
        config.yaw = yaw;
        config.pitch = pitch;
        config.roll = roll;
        config.pivot = transform.position;
        config.distance = 0;
        config.fov = fov;

        return config;
    }
}
