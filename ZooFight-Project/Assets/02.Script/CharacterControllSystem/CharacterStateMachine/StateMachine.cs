using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StateMachine 
{
    
    public BaseState CurrentState { get; protected set; }

    public void Initalize(BaseState startState)
    {
        CurrentState = startState;
        startState.Enter();
    }

    public void ChangeState(BaseState newState)
    {
        Debug.Log($"{CurrentState} Exit Start");
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();

    }
    



}
