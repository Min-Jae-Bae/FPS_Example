using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//사용자가 마우스 왼쪽 버튼을 누르면
// 총알공장에서 총알을 만들고
// 그 총알을 배치하고 싶다.
// 태어날 때 수류탄UI공장에서 수류탄UI를 만들어서 던지면 화면에 배치하고 쿨타임이 돌게하고싶다.
//쿨타임이 1일 때 수류탄을 던질 수 있다. 던지면 쿨타임이 0이되게 하고싶다.
public class PlayerFire : MonoBehaviour
{
    public GameObject grenadeUIFactory;
    public ScrollRect scrollRectSkill;
    enum BImpactName
    {
        Floor,
        Enemy
    }
    public GameObject bulletFactory;
    public Transform firePosition;

    public GameObject[] bImpactFactorys;
    SkillItem item;
    void Awake()
    {
        GameObject ui = Instantiate(grenadeUIFactory);
        //캐싱
        item = ui.GetComponent<SkillItem>();
        ui.transform.parent = scrollRectSkill.content;
    }

    private void Update()
    {
        UpdateGrenade();
        UpdateFire();

    }

    private void UpdateFire()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            //카메라의 위치에서 카메라의 앞방향으로 시선을 만들고
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            int layer = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Floor"));
            //부딪힌곳의 정보를 얻고싶다.
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
            {
                bool isEnemy = false;
                // 3. 시선이 닿은 곳에 총알자국공장에서 총알자국을 만들어서 배치하고싶다.
                BImpactName biName = BImpactName.Floor;
                // 만약 hitinfo의 물체의 레이어가 적이라면
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    biName = BImpactName.Enemy;
                    isEnemy = true;
                }
                // 바라본곳에 뭔가 있다.
                GameObject bulletImpact = Instantiate(bImpactFactorys[(int)BImpactName.Floor]);
                bulletImpact.transform.position = hitInfo.point;
                //방향을 회전하고 싶다. (튀는 방향:forward을 부딪힌 면의 Normal 방향으로
                bulletImpact.transform.forward = hitInfo.normal;

                // 만약 총에 맞는것이 적이라면
                if (isEnemy)
                {
                    // 적에게 너 총에 맞았어! 라고 알려주고싶다.
                    Enemy2 enemy = hitInfo.transform.gameObject.GetComponent<Enemy2>();
                    enemy.DamageProcess();
                }
            }
            else
            {
                //허공
            }

        }  //그곳에 총알자국공장에서 총알자국을 만들어서 배치하고 싶다.
    }

    public GameObject granadeFactory;

    private void UpdateGrenade()
    {
        //만약 스킬을 사용할 수 있다면 그리고사용자가 G키를 누르면 폭탄을 던지고싶다.
        if (Input.GetKeyDown(KeyCode.G) && item.CanDoIt())
        {
            item.DoIt();
            //총알공장에서 총알을 만들고
            GameObject granade = Instantiate(granadeFactory);
            //그 총알을 총구위치에 배치하고싶다.
            granade.transform.position = firePosition.position;
            granade.transform.forward = firePosition.forward;

            // 총알의 speed를 바꾸고싶다.
            granade.GetComponent<Grenade>().speed = 10;
        }
    }
}
