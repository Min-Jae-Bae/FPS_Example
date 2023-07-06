using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//물리로 힘을 받아서 이동하고
//어딘가에 부딪히면 파괴되고 싶다.
//그 때 만약 부딪힌 것이 적이라면
// 데미지를 2점 주고싶다.

// 스크립트를 추가했을 때 자동으로 추가하게 만들어준다.
[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10;
    public GameObject explosionFactory;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    bool isCollisionCheck;
    Collision other;
    private void OnCollisionEnter(Collision collision)
    {
        if (isCollisionCheck == true) return;
        other = collision;
        isCollisionCheck = true;
        StartCoroutine(IEBoom());

    }

    IEnumerator IEBoom()
    {
        // 삐소리내기
        yield return new WaitForSeconds(1);
        // 삐소리내기
        yield return new WaitForSeconds(1);
        // 삐소리내기
        yield return new WaitForSeconds(1);
        // 펑소리내기

        int layer = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] cols = Physics.OverlapSphere(transform.position, 3, layer);
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].GetComponent<Enemy2>().DamageProcess(2);
        }
        Destroy(gameObject);

        GameObject explosion =Instantiate(explosionFactory);
        explosion.transform.position = transform.position;
    }

}
