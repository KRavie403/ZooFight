using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Anim_Character : MonoBehaviour
{
    CharacterController myPlayer = null;
    private void Start()
    {
        myPlayer = transform.parent.GetComponent<CharacterController>();
    }

    #region 이동 모션 함수

    public void MoveAction()
    {

    }
    public void MoveStartAction()
    {

    }
    public void MoveEndAction()
    {

    }


    #endregion


    #region 공격모션 함수
    public void AttackAction()
    {

    }

    public void AttackStartAction()
    {

    }

    public void AttackEndAction()
    {

    }

    public void AttackCancelAction()
    {

    }

    public void AttackPauseAction()
    {

    }

    public void AttackStopAction()
    {

    }
    #endregion

    #region 점프모션 함수
    public void JumpAction()
    {

    }
    public void JumpStartAction()
    {

    }
    public void JumpEndAction()
    {

    }
    #endregion

    #region 아이템사용모션 함수

    public void ItemUseAction()
    {

    }

    public void ItemUseStartAction()
    {

    }

    public void ItemUseEndAction()
    {

    }

    public void ItemUseCancelAction()
    {

    }

    public void ItemReadyAction()
    {

    }

    public void ItemReadyStartAction()
    {

    }

    #endregion

    #region 승패모션함수

    public void WinActionEnd()
    {
        myPlayer.enabled = false;
    }
    public void LoseActionEnd()
    {
        myPlayer.enabled = false;
    }

    #endregion



}

