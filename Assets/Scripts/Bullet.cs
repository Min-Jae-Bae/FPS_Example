using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 태어날 때 앞 방향으로 물리 기반의 이동 하고싶다.
public class Bullet : MonoBehaviour
{

    public float speed = 10;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
    private void Update()
    {
        transform.forward = rb.velocity.normalized;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //만약 부딪힌 물체에 Rigidbody가 있다면
        var otherRB = collision.gameObject.GetComponent<Rigidbody>();

        if (otherRB != null)
        {
            // 내 앞 방향으로 힘을 가하고 싶다.
            otherRB.AddForce(transform.forward * 10 * otherRB.mass, ForceMode.Impulse);
        }

        Destroy(gameObject);
    }
}
