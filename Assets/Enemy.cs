using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 태어날 때 목적지(플레이어)를 알려주고싶다.
// 살아가면서 플레이어 방향으로 agent를 이용해서 이동하고 싶다.
public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    GameObject target;
    NavMeshAgent agent;

    private void Start()
    {
        target = GameObject.Find("Player");
        //agent야 너의 목적지는 target의 위치야
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

        agent.destination = target.transform.position;
    }
}
