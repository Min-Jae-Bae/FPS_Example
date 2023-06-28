using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� �Է¿� ���� X�� Y���� ȸ���� �ϰ�ʹ�.
public class CameraRotate : MonoBehaviour
{
    float rx, ry;
    public float rotSpeed = 200;
    private void Update()
    {
        //1. ������� �Է¿� ����
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        //2. x�� y���� ���� �����ϰ�.
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        //3. rx�� ���� ������ �����ϰ� �ʹ�.
        rx = Mathf.Clamp(rx, -75, 75);
        //4. �� ���������� ȸ���� �ϰ� �ʹ�.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
