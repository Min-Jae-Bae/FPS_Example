using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이벤트 함수를 제작하고싶다.

public class Enemy2AnimEvent : MonoBehaviour
{
    Enemy2 enemy2;

    private void Awake()
    {
        enemy2 = GetComponentInParent<Enemy2>();
    }
    private void Update()
    {

    }

    public void OnAttack_Finished()
    {
        enemy2.OnAttack_Finished();
    }

    public void OnAttack_Hit()
    {
        enemy2.OnAttack_Hit();
    }

    public void OnAttackWait_Finished()
    {
        enemy2.OnAttackWait_Finished();
    }

    public void OnReact_Finished()
    {
        enemy2.OnReact_Finished();
    }
}
