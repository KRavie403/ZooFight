using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Create : BaseState
{

    public Character_Create(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }
    public delegate void ableFunc();
    public Dictionary<PlayerController.pFunc, ableFunc> ableFuncs = new();

    public override void Initate()
    {
        base.Initate();
        ableFuncs.Add(PlayerController.pFunc.Move,player.CurAxisMove); 

    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }


}
