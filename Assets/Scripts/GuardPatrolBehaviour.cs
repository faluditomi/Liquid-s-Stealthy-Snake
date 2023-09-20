using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrolBehaviour : MonoBehaviour
{
    private GuardStateMachine myStateMachine;

    private void Awake()
    {
        myStateMachine = GetComponent<GuardStateMachine>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}