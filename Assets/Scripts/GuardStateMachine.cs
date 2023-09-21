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

    private Light light;

    private Color lightColor;

    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float chaseSpeed = 6f;
    
    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<PlayerController>().transform;

        light = FindObjectOfType<Light>();
    }

    private void Start()
    {
        SetState(GuardState.Patrolling);

        lightColor = light.color;
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

                myAgent.speed = patrolSpeed;

                light.color = lightColor;

                light.intensity = 1f;

                OnSwithToPatrol?.Invoke();
            break;

            case GuardState.Chasing:
                currentState = GuardState.Chasing;

                myAgent.speed = chaseSpeed;

                light.color = Color.red;

                light.intensity = 10f;

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
