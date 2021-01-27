using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyView : MonoBehaviour
{
    [Range(-180, 180)]
    public float roll = 0f;

    [Range(1, 179)]
    public float fov = 60;

    public float distance = 0;

    public GameObject target;

    //public Rail rail;

    public Vector3 railPosition = new Vector3();

    public float speed = 1f;
}
