using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseState
{

    protected PlayerController player;
    protected StateMachine stateMachine;

    public BaseState(PlayerController player,StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;

    }

    public virtual void Initate()
    {

    }

    public virtual void Enter()
    {
        //Debug.Log($"현재 상태 : {stateMachine.CurrentState}");
    }


    public virtual void Exit()
    {
        UnityAction act = () => { Debug.Log($"{stateMachine.CurrentState} State Exit End"); };
        //Debug.Log($"{stateMachine.CurrentState} Exit Start");

    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
        
    }

}
