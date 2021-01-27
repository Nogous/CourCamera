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

    public float GetLength()
    {
        return length;
    }

    public Vector3 GetPosition(float distance)
    {
        if (isLoop)
        {
            while (distance > length)
            {
                distance -= length;
            }
            while (distance < 0)
            {
                distance += length;
            }
        }
        else
        {
            if (distance >= length) return transform.GetChild(transform.childCount -1).position;
            else if (distance <= 0) return transform.GetChild(0).position;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            int j = i + 1;
            if (j == transform.childCount)
            {
                j = 0;
            }

            if (distance > Vector3.Distance(transform.GetChild(j).position, transform.GetChild(i).position))
            {
                distance -= Vector3.Distance(transform.GetChild(j).position, transform.GetChild(i).position);
            }
            else
            {
                Vector3 tmp = transform.GetChild(j).position - transform.GetChild(i).position;
                return transform.GetChild(i).position + tmp.normalized * distance;
            }
        }

        return Vector3.zero;
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
