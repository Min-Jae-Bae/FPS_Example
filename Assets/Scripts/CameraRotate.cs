using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 입력에 따라 X와 Y축의 회전을 하고싶다.
public class CameraRotate : MonoBehaviour
{
    float rx, ry;
    public float rotSpeed = 200;
    public float targetFOV;
    void Start()
    {
        targetFOV = Camera.main.fieldOfView;
        gunTargetlocalPosition = zoomOutPosition.localPosition;

    }
    private void Update()
    {
        //1. 사용자의 입력에 따라
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        //2. x와 y축의 값을 누적하고.
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        //3. rx에 대해 각도를 제한하고 싶다.
        rx = Mathf.Clamp(rx, -75, 75);
        //4. 그 누적값으로 회전을 하고 싶다.
        transform.eulerAngles = new Vector3(-rx, ry, 0);

        UpdateZoom();
    }

    public float zoomInFOV = 15;
    public float zoomOutFOV = 60;
    // targetFOV의 목적지 변수를 만들고 카메라의 targetFOV가 수렴하고싶다.
    // zoom에 따라 총의 위치를 변경하고싶다.
    public Transform zoomInPosition;
    public Transform zoomOutPosition;
    public Transform gun;
    Vector3 gunTargetlocalPosition;
    public float zoomSpeed = 20;

    private void UpdateZoom()
    {
        // 마우스 오른쪽 버튼을 누르면 Zoom In
        if (Input.GetButtonDown("Fire2"))
        {
            targetFOV = zoomInFOV;
            gunTargetlocalPosition = zoomInPosition.localPosition;
            zoomSpeed = 20;
        }
        // 마우스 오른쪽 버튼을 떼면 Zooom Out
        else if (Input.GetButtonUp("Fire2"))
        {
            targetFOV = zoomOutFOV;
            gunTargetlocalPosition = zoomOutPosition.localPosition;
            zoomSpeed = 5;
        }

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        gun.localPosition = Vector3.Lerp(gun.localPosition, gunTargetlocalPosition, Time.deltaTime * zoomSpeed);
    }
}
