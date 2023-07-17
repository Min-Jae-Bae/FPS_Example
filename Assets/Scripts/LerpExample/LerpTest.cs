using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//두 지점 사이를 선형보간으로
// 태어날 때 a위치로 이동하고싶다.
// 살아가면서 b위치로 계속 이동하고싶다.
public class LerpTest : MonoBehaviour
{
    public Transform a, b;
    [Range(0, 1)]
    public float t;

    void Start()
    {
        transform.position = a.position;
        t = 0.001f;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, b.position, t);
    }
}
