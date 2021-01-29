using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;

    public virtual float ComputeSelfWeight()
    {
        return 1.0f;
    }
}
