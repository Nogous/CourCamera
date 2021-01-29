using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;

    protected bool IsActive { get; private set; }


    public virtual float ComputeSelfWeight()
    {
        return 1.0f;
    }

    protected void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

}
