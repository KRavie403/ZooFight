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

    #region �̵� ��� �Լ�

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


    #region ���ݸ�� �Լ�
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

    #region ������� �Լ�
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

    #region �����ۻ���� �Լ�

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

    #region ���и���Լ�

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

