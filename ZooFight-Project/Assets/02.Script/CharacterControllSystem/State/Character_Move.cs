using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : BaseState
{

    public Character_Move(PlayerController player,StateMachine stateMachine) : base (player, stateMachine)  
    {

    }


    public override void Initate()
    {

        base.Initate();
        ableFuncs.Add(PlayerController.pFunc.Move, player.CurAxisMove);

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
