using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Anim_Character : MonoBehaviour
{
    PlayerController myPlayer = null;
    private void Start()
    {
        myPlayer = transform.parent.GetComponent<PlayerController>();
    }

    #region �̵� ��� �Լ�

    public void MoveAction()
    {
        // ???
    }
    public void MoveStartAction()
    {
        // ������ �̵� ���� ����
    }
    public void MoveEndAction()
    {
        // ������ �̵� ���� ����
    }


    #endregion


    #region ���ݸ�� �Լ�
    public void AttackAction()
    {
        // ���� �׼� ����
        myPlayer.PlayerAttack();
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

        myPlayer.curItems.ItemEnd();
    }

    public void ItemUseCancelAction()
    {
        myPlayer.myAnim.SetTrigger("ItemUseCancel");
    }

    public void ItemReadyAction()
    {
        myPlayer.myAnim.SetTrigger("ItemReady");
    }

    public void ItemReadyStartAction()
    {

    }

    // Ÿ�ݴ�� ����
    public void ItemHitAction()
    {
        //myPlayer.AttackPoint
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

