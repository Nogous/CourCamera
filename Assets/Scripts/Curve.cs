using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{

    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A, B, C, D, t);
    }

    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        return localToWorldMatrix.MultiplyPoint(GetPosition(t));
    }

    public void DrawGizmo(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = c;
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(A), 0.5f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(B), 0.5f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(C), 0.5f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(D), 0.5f);

        int stepCount = 30;
        for (int index = 0; index < stepCount - 1; ++index)
        {
            Gizmos.DrawLine(GetPosition(index / (float)stepCount, localToWorldMatrix), GetPosition((index + 1) / (float)stepCount, localToWorldMatrix));
        }

    }
}
