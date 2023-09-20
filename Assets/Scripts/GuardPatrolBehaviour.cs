using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrolBehaviour : MonoBehaviour
{
    private enum PatrolTypes
    {
        Looping,
        Linear
    }

    [SerializeField] private PatrolTypes currentPatrolType = PatrolTypes.Looping;

    private GuardStateMachine myStateMachine;

    private NavMeshAgent myAgent;

    [SerializeField] private List<Transform> waypoints;

    private int currentWaypoint = 0;

    private bool isWaypointAscending = true;

    private void Awake()
    {
        myStateMachine = GetComponent<GuardStateMachine>();

        myAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        myStateMachine.OnSwithToPatrol += GetNewDestination;
    }

    private void OnDisable()
    {
        myStateMachine.OnSwithToPatrol -= GetNewDestination;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!myStateMachine.GetCurrentGuardState().Equals(GuardStateMachine.GuardState.Patrolling))
        {
            return;
        }

        if(other.transform.Equals(waypoints[currentWaypoint]))
        {
            GetNewDestination();
        }
    }

    private void GetNewDestination()
    {
        if(waypoints.Count < 0)
        {
            return;
        }

        if(currentPatrolType.Equals(PatrolTypes.Looping))
        {
            if(currentWaypoint + 1 < waypoints.Count)
            {
                currentWaypoint++;
            }
            else
            {
                currentWaypoint = 0;
            }
        }
        else if(currentPatrolType.Equals(PatrolTypes.Linear))
        {
            if(isWaypointAscending)
            {
                if(currentWaypoint + 1 < waypoints.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    isWaypointAscending = false;

                    currentWaypoint--;
                }
            }
            else
            {
                if(currentWaypoint - 1 >= 0)
                {
                    currentWaypoint--;
                }
                else
                {
                    isWaypointAscending = true;

                    currentWaypoint++;
                }
            }
        }

        myAgent.SetDestination(waypoints[currentWaypoint].position);
    }
}