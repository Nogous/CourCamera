﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    [Range(0, 1)]
    public float weight;
    //public bool isActiveOnStart = false;

    protected CameraConfiguration config = new CameraConfiguration();

    public Color color = Color.red;

    public virtual CameraConfiguration GetConfiguration()
    {
        return config;
    }

    private void Start()
    {
        /*if (isActiveOnStart)
        {
            SetActive(true);
        }*/
    }

    public virtual void SetActive(bool isActive)
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

    private void OnDrawGizmos()
    {
        if (GetConfiguration() != null)
        {
            GetConfiguration().DrawGizmos(color);
        }
    }
}
