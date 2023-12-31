﻿using PatrolNChase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 상태머신으로 제어하고싶다.
// agent를 이용해서 이동하고 싶다.
// 나를 생성한 spawnManager 기억하고, 내가 죽을때 개한테 알려주고싶다.
// 태어날 때 순찰 상태로 하고싶다.
// 순찰할 때 플레이어가 근처에 오면
// 플레이어를 추적하고싶다.
// 추적중에 플레이어가 너무 멀어지면 다시 순찰 상태로 전이하고 싶다.
public class Enemy2 : MonoBehaviour
{
    public Action<GameObject> onDestoryed;

    public void Init(Action<GameObject> callback)
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        enemyHP = GetComponent<EnemyHP>();

        onDestoryed = callback;


        // 초기화 ㅈ처리
        // 체력, 현재상태, targetIndex, agent 위치를 초기화해야함
        enemyHP.HP = enemyHP.maxHP;
        state = State.Idle;
        targetIndex = 0;
    }
    void DestroyMySelf()
    {
        if (onDestoryed != null)
        {
            onDestoryed(gameObject); 
        }
        gameObject.SetActive(false);
    }

    EnemyHP enemyHP;
    public Animator anim;
    public enum State
    {
        Idle,
        Chase, // 추적
        Attack,
        React, //데미지
        Die, //죽음
        Patrol
    }
    public State state;

    NavMeshAgent agent;
    public GameObject target;
    public float attackRange = 3;
    public int targetIndex;
 
    // 델리게이트 : 변수인데 함수를 담는 변수
    // 람다식
    // 무명함수
 

    private void Update()
    {
        // state를 기준으로 분기러치 해보세요
        switch (state)
        {
            case State.Idle: UpdateIdle(); break;
            case State.Chase: UpdateChase(); break;
            case State.Attack: UpdateAttack(); break;
            case State.Patrol: UpdatePatrol(); break;
        }
    }

    private void UpdatePatrol()
    {
        //길정보를 알고싶다.
        Vector3 pos = PathManager.instance.points[targetIndex].position;
        //내가 길의 어떤 위치로 갈것인지 알고싶다.
        agent.SetDestination(pos);
        //0.1m까지 근접했다면 도착한 것으로 하고싶다.
        float dist = Vector3.Distance(transform.position, pos);
        //도착했다면 targetIndex를 1 증가 시키고 싶다.
        if (dist < 1f)
        {
            targetIndex = (targetIndex + 1) % PathManager.instance.points.Length;
        }

        //만약 플레이어가 내 인식거리안에 들어있다면
        float dist2 = Vector3.Distance(transform.position, target.transform.position);
        if (dist2 < attackDistance)
        {
            //추적상태로 전이하고싶다.
            state = State.Chase;
        }
    }

    float attackDistance = 5;

    private void UpdateAttack()
    {
        //공격중에는 목적지를 바라보게 하고싶다.
        transform.LookAt(target.transform);
    }

    private void UpdateChase()
    {
        agent.destination = target.transform.position;
        //목적지와 나의 거리를 재고싶다.
        float distance = Vector3.Distance(target.transform.position, transform.position);
        // 만약 그 거리가 공격가능거리보다 작다면
        if (distance < attackRange)
        {
            // 공격상태로 전이하고싶다.
            state = State.Attack;
            anim.SetTrigger("Attack");
            agent.isStopped = true;
        }
        else if(distance > farDistance)
        {
            //만약 추격중에 플레이어와의 거리가 포기거리 보다 크다면
            state = State.Patrol;
            // 순찰 상태로 전이하고싶다.
        }
    }
    public float farDistance = 10;

    private void UpdateIdle()
    {
        //태어날 때 목적지를 찾고싶다.
        target = GameObject.Find("Player");
        //만약 목적지를 찾았다면 target != null
        if (target != null)
        {
            // 순찰상태로 전이하고싶다.
            state = State.Patrol;
            anim.SetTrigger("Move");
            agent.isStopped = false;

        }
        //이동상태로 전이하고싶다.
    }

    #region 애니메이션 이벤트 함수를 통해 호출되는 함수들
    public void OnAttack_Finished()
    {
        //대기상태로 가야하는가?
        //목적지와의 거리가 공격가능거리 이상이라면
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > attackRange)
        {
            state = State.Chase;
            anim.SetTrigger("Move");
            agent.isStopped = false;
        }

    }

    public void OnAttack_Hit()
    {
        anim.SetBool("ReAttack", false);
        //타격할 수 있는 조건
        //목적지와의 거리가 공격가능거리 이하일때 가능
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < attackRange)
        {
            print("Enemy -> Player Hit!");
            HitManager.instance.DoHit();
            state = State.Attack;
            anim.SetTrigger("Attack");
            agent.isStopped = true;
        }
        print("에너미는 플레이러를 공격한다.");
    }
    public void OnAttackWait_Finished()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > attackRange)
        {
            state = State.Chase;
            anim.SetTrigger("Move");
            agent.isStopped = false;
        }
        //이동상태로 전이하고싶다.
        else
        {
            anim.SetBool("ReAttack", true);
        }
    }

    public void OnReact_Finished()
    {
        //리액션이 끝났으니 Move상태로 전이하고 싶다.
        state = State.Chase;
        anim.SetTrigger("Move");
    }
    #endregion

    // 데미지를 입으면 데미지 UI를 내 위치 위쪽으로 1M 위에 배치하고싶다.
    public GameObject dammageUIFactory;

    internal void DamageProcess(int damage = 1)
    {
        if (state == State.Die) return;
        //적 체력을 1감소하고싶다.
        enemyHP.HP -= damage;
        agent.isStopped = true;
        GameObject ui = Instantiate(dammageUIFactory);
        ui.transform.position = transform.position + Vector3.up * 1.2f;
        //만약 적 체력이 0이하라면
        if (enemyHP.HP <= 0)
        {
            state = State.Die;
            // 파괴하고싶다.
            Invoke("DestroyMySelf", 5);
            anim.SetTrigger("Die");

        }
        else // 체력이 남아있으면 리액션 하고싶다.
        {
            state = State.React;
            anim.SetTrigger("React");
        }
    }

}
