using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
                OnSwithToPatrol?.Invoke();
            break;

            case GuardState.Chasing:
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
}
