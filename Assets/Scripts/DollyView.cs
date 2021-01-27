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

    public float railPosition = 0;

    public float speed = 1f;

    //[Range(0,1)]
    public float currentPos = 0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            currentPos += Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            currentPos -= Time.deltaTime * speed;
        }
    }

    public override CameraConfiguration GetConfiguration()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;

        config.yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        config.pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        config.roll = roll;
        config.pivot = rail.GetPosition(currentPos * rail.GetLength());
        config.distance = 0;
        config.fov = fov;

        return config;
    }

}
