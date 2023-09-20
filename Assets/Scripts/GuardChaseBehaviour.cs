using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardChaseBehaviour : MonoBehaviour
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