using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_ItemUse : BaseState
{
    public Character_ItemUse(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }


    public override void Initate()
    {

        base.Initate();
        //ableFuncs.Add(PlayerController.pFunc.Move, player.CurAxisMove);
        ableFuncs.Add(PlayerController.pFunc.ItemUse, player.ItemUse);

    }

    public override void Enter(BaseState BeforeState)
    {
        base.Enter(BeforeState);
        player.SetState(PlayerController.pState.ItemUse);
        //ableFuncs[PlayerController.pFunc.ItemUse]();

        // ������ ������ ���Խ� ĳ���� �̵���� ���
        player.AxisX = 0;
        player.AxisY = 0;
        player.myAnim.SetBool("IsMoving", false);
        player.myAnim.SetBool("IsRunning", false);

        // ������ ��� ��� ���
        player.myAnim.SetBool("ItemUse",true);

        player.curItems.ItemUse();
    }

    public override void Exit()
    {
        base.Exit();

        player.myAnim.SetBool("ItemUse", false);

        player.curItems = null;
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
