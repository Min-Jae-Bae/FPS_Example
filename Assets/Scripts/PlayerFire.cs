using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자가 마우스 왼쪽 버튼을 누르면
// 총알공장에서 총알을 만들고
// 그 총알을 배치하고 싶다.
public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public Transform firePosition;

    private void Update()
    {
        //만약 사용자가 마우스 왼쪽 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            //총알공장에서 총알을 만들고
            GameObject bullet = Instantiate(bulletFactory);
            //그 총알을 총구위치에 배치하고싶다.
            bullet.transform.position = firePosition.position;
            bullet.transform.forward = firePosition.forward;

            // 총알의 speed를 바꾸고싶다.
            Bullet bulletComp =bullet.GetComponent<Bullet>();
            bulletComp.speed = 20;
        }
    }
}
