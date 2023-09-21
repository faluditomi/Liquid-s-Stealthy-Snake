using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Transform player;

    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private float viewRadius = 10f;
    [SerializeField] [Range(0,360)] private float viewAngle = 90;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    public bool IsPlayerInSight()
    {
        Vector3 vectorToPlayer = (player.position - transform.position).normalized;

        if(Vector3.Angle(transform.forward, vectorToPlayer) < viewAngle / 2f)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if(distanceToPlayer < viewRadius)
            {
                return !Physics.Raycast(transform.position, vectorToPlayer, distanceToPlayer, obstacleMask);
            }
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
