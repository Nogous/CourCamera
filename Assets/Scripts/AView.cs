using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;

    public abstract CameraConfiguration GetConfiguration();
}
