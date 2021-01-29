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
        Vector3 dir2;

        Vector3 nodePos = transform.GetChild(node).position;

        if (node >= transform.childCount-1)
            dir = transform.GetChild(0).position - nodePos;
        else
            dir = transform.GetChild(node+1).position - nodePos;


        if (node == 0)
            dir2 = transform.GetChild(transform.childCount - 1).position - nodePos;
        else
            dir2 = transform.GetChild(node - 1).position - nodePos;

        float dot1 = Vector3.Dot(dir.normalized,target-nodePos);

        dot1 = Mathf.Clamp(dot1, 0, dir.magnitude);

        float dot2 = Vector3.Dot(dir2.normalized,target-nodePos);

        dot2 = Mathf.Clamp(dot2, 0, dir2.magnitude);

        Vector3 pos1 = nodePos + dir.normalized * dot1;
        Vector3 pos2 = nodePos + dir2.normalized * dot2;



        if (Vector3.Distance(pos1, target) < Vector3.Distance(pos2, target))
        {
            distanceOnRail += dot1;
        }
        else
        {
            distanceOnRail -= dot2;
        }



        return distanceOnRail;

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
