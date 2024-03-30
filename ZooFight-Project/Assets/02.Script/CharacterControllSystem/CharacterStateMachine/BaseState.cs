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

    public delegate void ableFunc();
    public Dictionary<PlayerController.pFunc, ableFunc> ableFuncs = new();

    public virtual void Initate()
    {
        act = () => { Debug.Log($"{stateMachine.CurrentState} State Exit End"); };

    }

    public virtual void Enter(BaseState BeforeState)
    {
        Debug.Log($"현재 상태 : {stateMachine.CurrentState}");
    }

    public UnityAction act;

    public virtual void Exit()
    {
        //Debug.Log($"{stateMachine.CurrentState} Exit Start");

    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
        
    }

}
