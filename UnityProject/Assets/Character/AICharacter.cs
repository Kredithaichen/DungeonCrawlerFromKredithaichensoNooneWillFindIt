using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(NavMeshAgent))]
public class AICharacter : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }
}
