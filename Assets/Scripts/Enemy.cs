using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 태어날 때 목적지(플레이어)를 알려주고싶다.
// 살아가면서 플레이어 방향으로 agent를 이용해서 이동하고 싶다.
// 상태머신을 이용해서 적을 제어하고싶다.
// 대기, 이동, 공격
// 상태머신이 바뀌면 애니메이션 상태도 같이 바뀌게 하고싶다.
public class Enemy : MonoBehaviour
{
    private Animator anim;

    public enum State
    {
        Idle,
        Move,
        Attack
    }

    public State state = 0;

    // 공격 가능거리
    public float attackRange = 3;

    public float speed = 5f;
    GameObject target;
    NavMeshAgent agent;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle: UpdateIdle(); break;
            case State.Move: UpdateMove(); break;
            case State.Attack: UpdateAttack(); break;
            default: break;
        }
    }
    private void UpdateIdle()
    {
        //태어날 때 목적지를 찾고싶다.
        target = GameObject.Find("Player");
        //만약 목적지를 찾았다면 target != null
        if (target != null)
        {
            state = State.Move;
            anim.SetTrigger("Move");
        }
        //이동상태로 전이하고싶다.
    }
    private void UpdateMove()
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
        }
    }

    enum AttackSubState
    {
        Attack,
        Wait
    }
    AttackSubState attackSubState;
    bool isAttackHit;
    float currentTime;
    float attackHitTime = 0.91f;
    float attackFinishedTime = 2.2f;
    float attackWaitTime = 2;

    private void UpdateAttack()
    {
        //시간이 흐르다가 타격시간을 초과하면
        currentTime += Time.deltaTime;
        if (attackSubState == AttackSubState.Wait)
        {
            // 시간이 흐르다가
            if (currentTime > attackWaitTime)
            {
                attackSubState = AttackSubState.Attack;
                // 대기시간을 초과하면
                currentTime = 0;
                // 공격상태로 전이하고 싶다.
                isAttackHit = false;
                anim.SetBool("ReAttack", true);
            }

        }
        else if (attackSubState == AttackSubState.Attack)
        {
            // 타격 시간을 초과하면
            if (currentTime > attackHitTime)
            {
                // 계속 시간이 흐르다가
                if (false == isAttackHit)
                {
                    // 타격 !
                    isAttackHit = true;
                    anim.SetBool("ReAttack", false);
                    print("에너미는 플레이러를 공격한다.");
                }
                // 공격끝시간이 초과하면
                if (currentTime > attackFinishedTime)
                {
                    attackSubState = AttackSubState.Wait;
                }
                // 공격대기 상태로 전이하고 싶다.

            }
        }
    }

}
