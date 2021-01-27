using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop;

    public float length;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (i > 0)
            {
                length += Vector3.Distance(transform.GetChild(i).position, transform.GetChild(i - 1).position);
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

    /*public Vector3 GetPosition(float distance)
    {
        return 
    }*/

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        for(int i = transform.childCount - 1; i>=0; i--)
        {
            if (i > 0)
            {
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i - 1).position);
            }
            Gizmos.DrawSphere(transform.GetChild(i).position, .25f);

        }
        if (isLoop)
        {
            Gizmos.DrawLine(transform.GetChild(transform.childCount -1).position, transform.GetChild(0).position);
        }
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(Color.red);
    }
}
