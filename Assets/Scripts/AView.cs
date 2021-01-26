using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;
    public bool isActiveOnStart = false;

    public abstract CameraConfiguration GetConfiguration();

    private void Start()
    {
        if (isActiveOnStart)
        {
            SetActive(true);
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
