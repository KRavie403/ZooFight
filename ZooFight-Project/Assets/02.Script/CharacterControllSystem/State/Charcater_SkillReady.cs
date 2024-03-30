using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charcater_SkillReady : BaseState
{
    public Charcater_SkillReady(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }


    public override void Initate()
    {

        base.Initate();
        ableFuncs.Add(PlayerController.pFunc.Move, player.CurAxisMove);

    }

    public override void Enter(BaseState BeforeState)
    {
        base.Enter(BeforeState);

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
