using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1초동안 위로 이동하고싶다.
// 태어날 때 수명을 1초로 정하고싶다.
public class DamageUI : MonoBehaviour
{
    public AnimationCurve ac;
    public float lifespan = 0.8f;
    public float speed = 5;
    //태어날 때 내 위치를 기억하고싶다.
    Vector3 origin;
    void Start()
    {
        origin = transform.position;
        Destroy(gameObject, lifespan);
    }

    float currentTime;
    public float height = 1;
    void Update()
    {
        currentTime += Time.deltaTime / speed;
        float value = ac.Evaluate(currentTime);
        transform.position = origin + Vector3.up * value * height;
    }
}
