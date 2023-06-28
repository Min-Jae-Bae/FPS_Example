using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������� �Է¿� ���� �յ� �¿�� �̵��ϰ� �ʹ�.
//����ڰ� ������ư�� ������ ������ �ٰ�ʹ�.
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
        // ��ü���� cc�� ������ �ʹ�.
        cc = gameObject.GetComponent<CharacterController>();
    }
    private void Update()
    {
        // �߷��� ���� y�ӵ��� �ۿ��ؾ��Ѵ�.
        // 9.81 m/s
        yVelocity += gravity * Time.deltaTime;

        // ���� ���� �ִ� �׸��� ����ڰ� ������ư�� ������
        if (cc.isGrounded && Input.GetButtonDown("Jump"))
        {
            // JumpPower�� �ۿ��ؾ��Ѵ�.
            yVelocity = jumpPower;
        }

        //1.������� �Է¿� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //2.�յ� �¿�� �̵��ϰ� �ʹ�.
        Vector3 dir = new Vector3(h, 0, v);

        //3. ���� ������ ī�޶��� �չ����� �������� ��ȯ�ϰ�ʹ�.
        dir = cam.transform.TransformDirection(dir);
        dir.y = 0;

        transform.position += dir.normalized * speed * Time.deltaTime;
        // ������ y�ӵ��� dir�� y �׸� �ݿ��Ǿ���Ѵ�.
        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;
        //4.�� �������� 1�� �̵��ϰ� �ʹ�.
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
