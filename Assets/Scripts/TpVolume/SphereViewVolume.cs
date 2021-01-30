using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereViewVolume : AViewVolume
{
    public GameObject target;
    public float outerRadius;
    public float innerRadius;

    private float distance;

    private void Start()
    {
        if (innerRadius> outerRadius)
        {
            innerRadius = outerRadius;
            Debug.LogError("innerRadius> outerRadius");
        }
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= outerRadius)
            SetActive(true);
        else
            SetActive(false);
    }

    public override float ComputeSelfWeight()
    {
        return 1 - (Vector3.Distance(target.transform.position, transform.position) - innerRadius)/(outerRadius-innerRadius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.transform.position, outerRadius);
        Gizmos.DrawWireSphere(transform.transform.position, innerRadius);
    }
}
