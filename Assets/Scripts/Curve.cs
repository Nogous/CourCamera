using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;

    public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A, B, C, D, t);
    }
    
    public Vector3 GetPositionWorld(float t, Matrix4x4 localToWorldMatrix)
    {
        Vector3 pLocal = GetPosition(t);
        Vector3 pWorld = localToWorldMatrix.MultiplyPoint(pLocal);

        return pWorld;
    }

    private void DrawGizmo(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = c;
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(A), .25f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(B), .25f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(C), .25f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(D), .25f);

    }

    private void OnDrawGizmos()
    {
        DrawGizmo(Color.red, Matrix4x4.zero);
    }
}
