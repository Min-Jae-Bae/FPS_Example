using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자의 입력에 따라 앞뒤 좌우로 이동하고 싶다.
//사용자가 점프버튼을 누르면 점프를 뛰고싶다.
public class PlayerMove : MonoBehaviour
{
    public float jumpPower = 10;
    public float gravity = -9.81f;
    float yVelocity;

    CharacterController cc;

    public float speed = 5f;

    Camera cam; // cache
    private void Start()
    {
        cam = Camera.main;
        // 본체에게 cc를 얻어오고 싶다.
        cc = gameObject.GetComponent<CharacterController>();
    }
    private void Update()
    {
        // 중력의 힘이 y속도에 작용해야한다.
        // 9.81 m/s
        yVelocity += gravity * Time.deltaTime;

        // 만약 땅에 있다 그리고 사용자가 점프버튼을 누르면
        if (cc.isGrounded && Input.GetButtonDown("Jump"))
        {
            // JumpPower가 작용해야한다.
            yVelocity = jumpPower;
        }

        //1.사용자의 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //2.앞뒤 좌우로 이동하고 싶다.
        Vector3 dir = new Vector3(h, 0, v);

        //3. 현재 방향을 카메라의 앞방향을 기준으로 변환하고싶다.
        dir = cam.transform.TransformDirection(dir);
        dir.y = 0;

        transform.position += dir.normalized * speed * Time.deltaTime;
        // 결정된 y속도를 dir의 y 항목에 반영되어야한다.
        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;
        //4.그 방향으로 1로 이동하고 싶다.
        //transform.position += velocity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 from = transform.position;
        Vector3 to = from + Vector3.up * yVelocity;
        Gizmos.DrawLine(from, to);

    }
}
