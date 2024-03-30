using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Jump : BaseState
{


    public Character_Jump(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }


    public override void Initate()
    {
        base.Initate();
        ableFuncs.Add(PlayerController.pFunc.Move, player.CurAxisMove);
        ableFuncs.Add(PlayerController.pFunc.Jump, player.CharacterJump);
    }

    public override void Enter(BaseState BeforeState)
    {
        base.Enter(BeforeState);
        if (!player.GetisJump())
        {
            player.SetisJump(true);
            ableFuncs[PlayerController.pFunc.Jump]();
        }

    }

    public override void Exit()
    {
        base.Exit();
        player.SetisJump(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ableFuncs[PlayerController.pFunc.Move]();
        if (!player.GetisJump())
        {
            player.MoveStateCheck();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }



}
