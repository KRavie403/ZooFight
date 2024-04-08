using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charcater_ItemReady : BaseState
{
    public Charcater_ItemReady(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }


    public override void Initate()
    {

        base.Initate();
        ableFuncs.Add(PlayerController.pFunc.Move, player.CurAxisMove);
        ableFuncs.Add(PlayerController.pFunc.ItemUse, player.ItemUse);

    }

    public override void Enter(BaseState BeforeState)
    {
        base.Enter(BeforeState);
        player.SetState(PlayerController.pState.ItemReady);


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
