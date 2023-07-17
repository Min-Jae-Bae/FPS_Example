using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//목적지를 바라보고싶다.
public class RotateLerpTest : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        //부드럽게 회전하고싶다.
        //1.바라볼 방향
        Vector3 lookDir = target.position - transform.position;
        lookDir.Normalize();
        //2.그 방향의 회전값
        Quaternion targetRotate = Quaternion.LookRotation(lookDir);
        //3.그 회전값으로 Lerp하고싶다.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, Time.deltaTime * 5);
    }
}
