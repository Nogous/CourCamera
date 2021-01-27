using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop;

    private float length;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i < transform.childCount - 1)
            {
                length += Vector3.Distance(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            }
        }
        if (isLoop)
        {
            length += Vector3.Distance(transform.GetChild(transform.childCount -1).position, transform.GetChild(0).position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetLength()
    {
        return length;
    }

    public Vector3 GetPosition(float distance)
    {
        float addedDistance = 0;
        float segmentDistance = 0;
        Vector3 pointA = Vector3.one;
        Vector3 pointB = Vector3.one;
        bool segmentFound = false;

        if (isLoop)
        {
            if(distance > length)
            {
                distance -= length;
            }
            else if (distance < 0)
            {
                distance = length - distance;
            }
        }
        else
        {
            if (distance > length) return;
            else if (distance < 0) return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i < transform.childCount - 1 && !segmentFound)
            {
                segmentDistance = Vector3.Distance(transform.GetChild(i).position, transform.GetChild(i + 1).position);
                addedDistance += segmentDistance;
                if(addedDistance > distance)
                {
                    segmentFound = true;
                    pointA = transform.GetChild(i).position;
                    pointB = transform.GetChild(i +1).position;
                }
            }
        }
        if (isLoop && !segmentFound)
        {
            segmentDistance = Vector3.Distance(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
            addedDistance += segmentDistance;
            if (addedDistance > distance)
            {
                segmentFound = true;
                pointA = transform.GetChild(transform.childCount - 1).position;
                pointB = transform.GetChild(0).position;
            }
        }

        float finalDistance = segmentDistance - (addedDistance - distance);

        return Vector3.Lerp(pointA, pointB, finalDistance);
    }

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        for(int i = 0; i < transform.childCount; i++)
        {
            if (i < transform.childCount - 1)
            {
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i +1).position);
            }
            Gizmos.DrawSphere(transform.GetChild(i).position, .25f);

        }
        if (isLoop)
        {
            Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
        }
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(Color.red);
    }
}
