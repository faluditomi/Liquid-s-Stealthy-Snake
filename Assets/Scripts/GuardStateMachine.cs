using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GuardStateMachine : MonoBehaviour
{
    public enum GuardState
    {
        Patrolling,

        Chasing
    }

    private GuardState currentState = GuardState.Patrolling;

    public event Action OnPatrolling;
    public event Action OnChasing;
    public event Action OnSwithToPatrol;
    public event Action OnSwithToChase;

    private NavMeshAgent myAgent;

    private Transform player;
    
    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<PlayerController>().transform;
    }

    private void Start()
    {
        SetState(GuardState.Patrolling);
    }

    private void Update()
    {
        switch(currentState)
        {
            case GuardState.Patrolling:
                OnPatrolling?.Invoke();
            break;

            case GuardState.Chasing:
                OnChasing?.Invoke();
            break;

            default:
            break;
        }
    }

    public void SetState(GuardState state)
    {
        switch(state)
        {
            case GuardState.Patrolling:
                currentState = GuardState.Patrolling;

                OnSwithToPatrol?.Invoke();
            break;

            case GuardState.Chasing:
                currentState = GuardState.Chasing;

                OnSwithToChase?.Invoke();
            break;

            default:
            break;
        }
    }

    public GuardState GetCurrentGuardState()
    {
        return currentState;
    }

    public NavMeshAgent GetAgent()
    {
        return myAgent;
    }

    public Transform GetPlayer()
    {
        return player;
    }
}
