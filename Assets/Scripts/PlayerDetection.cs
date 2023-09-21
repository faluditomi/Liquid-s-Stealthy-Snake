using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private GuardStateMachine myStateMachine;

    private PlayerController playerController;

    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private float viewRadius = 10f;
    [SerializeField] [Range(0,360)] private float viewAngle = 90;

    private void Awake()
    {
        myStateMachine = GetComponent<GuardStateMachine>();
    }

    private void Start()
    {
        playerController = myStateMachine.GetPlayer().GetComponent<PlayerController>();
    }

    public bool IsPlayerInSight()
    {
        Vector3 vectorToPlayer = (myStateMachine.GetPlayer().position - transform.position).normalized;
        
        float distanceToPlayer = Vector3.Distance(transform.position, myStateMachine.GetPlayer().position);

        if((Vector3.Angle(transform.forward, vectorToPlayer) < viewAngle / 2f && distanceToPlayer < viewRadius) ||
        (Vector3.Angle(transform.forward, vectorToPlayer) >= viewAngle / 2f && playerController.GetIsSprinting()))
        {
            return !Physics.Raycast(transform.position, vectorToPlayer, distanceToPlayer, obstacleMask);
        }

        return false;
    }

    public Vector3 VectorFromAngle(float angleInDegrees, bool isAngleGlobal)
    {
        if(!isAngleGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public float GetViewRadius()
    {
        return viewRadius;
    }

    public float GetViewAngle()
    {
        return viewAngle;
    }
}
