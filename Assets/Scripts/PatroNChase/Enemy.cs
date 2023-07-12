using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PatrolNChase
{
    public class Enemy : MonoBehaviour
    {
        NavMeshAgent agent;
        public int targetIndex;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
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


            }
        }
    }
}
