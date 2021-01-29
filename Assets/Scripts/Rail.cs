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

    public GameObject draw;

    public float NearPosOnRail(Vector3 target)
    {
        float distance = Mathf.Infinity;
        int node = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
                if (Vector3.Distance(target, transform.GetChild(i).position) < distance)
                {
                    distance = Vector3.Distance(target, transform.GetChild(i).position);
                    node = i;
                }
        }

        float distanceOnRail = 0f;

        for (int i = 0; i < node; i++)
        {
            distanceOnRail += Vector3.Distance(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        ////////////////////////////////////////////////////

        Vector3 dir;

        if (node>= transform.childCount-1)
            dir = transform.GetChild(0).position - transform.GetChild(node).position;
        else
            dir = transform.GetChild(node+1).position - transform.GetChild(node).position;

        Vector3 pos = Vector3.Project(target - transform.GetChild(node).position, dir) + transform.GetChild(node).position;
        distance = Vector3.Distance(pos, transform.GetChild(node).position);
        if (dir.normalized == (pos - transform.GetChild(node).position).normalized)
        {
            distanceOnRail += distance;
        }
        else
        {
            if (!(node == 0 && !isLoop))
                distanceOnRail -= distance;
        }

        return distanceOnRail;




        /*
        if (node == transform.childCount - 1)
        {
            if (isLoop)
                distance = Vector3.Distance(target, GetProjectPoint(target, transform.GetChild(node).position, transform.GetChild(0).position));
            else
            {

            }
        }
        else
            distance = Vector3.Distance(target, GetProjectPoint(target, transform.GetChild(node).position, transform.GetChild(node + 1).position));

        if (isLoop)
        {
            if (node == 0)
            {
                float distance2 = Vector3.Distance(target, GetProjectPoint(target, transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position));
                if (distance2< distance)
                {
                    return distanceOnRail - distance2;
                }
                else
                {
                    return distanceOnRail + distance;
                }
            }
        }
        */
    }

    private Vector3 GetProjectPoint(Vector3 target, Vector3 node, Vector3 nextNode)
    {
        Vector3 direction = nextNode - node;

        Vector3 tmp = node + Mathf.Clamp(Vector3.Dot(target - node, direction),0f, Vector3.Distance(node, nextNode)) * direction;
        return tmp;
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
