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

    private PlayerDetection myPlayerDetection;

    [SerializeField] private List<Transform> waypoints;

    [SerializeField] private float detectionCheckFrequency = 0.5f;

    private int currentWaypoint = 0;

    private bool isWaypointAscending = true;

    private void Awake()
    {
        myStateMachine = GetComponent<GuardStateMachine>();

        myPlayerDetection = GetComponent<PlayerDetection>();
    }

    private void OnEnable()
    {
        myStateMachine.OnSwithToPatrol += GetNewDestination;

        myStateMachine.OnSwithToPatrol += PlayerDetection;
    }

    private void OnDisable()
    {
        myStateMachine.OnSwithToPatrol -= GetNewDestination;

        myStateMachine.OnSwithToPatrol -= PlayerDetection;
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

    private void PlayerDetection()
    {
        StartCoroutine(PlayerDetectionBehaviour());
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

        myStateMachine.GetAgent().SetDestination(waypoints[currentWaypoint].position);
    }

    private IEnumerator PlayerDetectionBehaviour()
    {
        yield return new WaitForSeconds(detectionCheckFrequency);

        if(myPlayerDetection.IsPlayerInSight())
        {
            myStateMachine.SetState(GuardStateMachine.GuardState.Chasing);
        }
        else
        {
            StartCoroutine(PlayerDetectionBehaviour());
        }
    }
}