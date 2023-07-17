using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PatrolNChase
{
    public class Enemy : MonoBehaviour
    {
        //적의 상태를 만들고싶다.
        public enum State
        {
            Patrol,
            Chase
        }

        public State state;

        NavMeshAgent agent;
        public int targetIndex;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            switch (state)
            {
                case State.Patrol:
                    UpdatePatrol();
                    break;
                case State.Chase:
                    UpdateChase();
                    break;
            }
        }
        public float untraceableDistance = 10;
        void UpdateChase()
        {
            Vector3 targetPosition = chaseTarget.transform.position;
            //1. 추적대상을 향해 계속 이동하고싶다.
            agent.SetDestination(targetPosition);
            //2. 만약 추적대상과의 거리가 추적포기거리보다 커진다면
            float distance = Vector3.Distance(transform.position, targetPosition);
            //3. 만약 들어왓다면 내 상태를 추적상태로 전이하고싶다.
            if (distance > untraceableDistance)
            {
                state = State.Patrol;
            }
        }

        public float chaseDistance = 5;
        public GameObject chaseTarget;
        //
        void UpdatePatrol()
        {
            Vector3 target = PathManager.instance.points[targetIndex].position;

            agent.SetDestination(target);
            target.y = transform.position.y;
            // 만약 목적지에 도착했다면 (두 지점의 거리가 0.1M 이하라면)
            float destination = Vector3.Distance(target, transform.position);
            if (destination < 0.1f)
            {
                // 인덱스를 1증가시키고싶다.
                // 만약 인덱스가 points배열의 크기이상이되면 0으로 초기화 하고싶다.
                targetIndex = (targetIndex + 1) % PathManager.instance.points.Length;

                //2 플레이어가 내 감지거리 안에 있는지를 계속 확인하고
                int layerMask = 1 << LayerMask.NameToLayer("Player");
                Collider[] cols = Physics.OverlapSphere(transform.position, chaseDistance, layerMask);
                //3. 만약 cols의 길이가 0보다 크다면
                if (cols.Length > 0)
                {
                    // 만약 나의 앞방향 벡터와 플레이어와의 방향벡터를 내적해서 0.85이상라면
                    // 0.3이하라면
                    //4. 검출된 녀석을 추적대상으로 하고싶다.
                    Vector3 targetVector = chaseTarget.transform.position - transform.position;
                    targetVector.Normalize();
                    //5. 내 상태를 추적상태로 전이하고싶다.
                    Vector3 forwardVector = transform.forward;
                    float dot = Vector3.Dot(targetVector, forwardVector);
                    if (dot >= 0.85f)
                    {

                        chaseTarget = cols[0].gameObject;
                        state = State.Chase;
                    }

                }
            }
        }
    }
}