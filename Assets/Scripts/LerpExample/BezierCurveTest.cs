using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveTest : MonoBehaviour
{
    LineRenderer lr;
    public Transform a, b, c;
    private int pointCount = 100;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        lr.positionCount = pointCount;
        for (int i = 0; i < pointCount; i++)
        {
            float t = (float)i / (pointCount - 1);
            lr.SetPosition(i, GetCurvePoint(a.position, b.position, c.position, t));
        }
    }

    Vector3 GetCurvePoint(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(ab, bc, t);
    }
}
