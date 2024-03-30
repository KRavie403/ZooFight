using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VictoryDecision : MonoBehaviour
{
    [SerializeField]
    HitScanner.Team DecisionTeam = HitScanner.Team.NotSetting;

    BlockObject enterBlock;


    private void Update()
    {
        if (Gamemanager.Inst.IsGameEnd) return;
        if (DecisionTeam == HitScanner.Team.NotSetting) return;
        if (enterBlock != null)
        {
            if (enterBlock.myTeam == HitScanner.Team.NotSetting) return;

            if (enterBlock.myTeam == DecisionTeam) 
            {
                // �¸��� ���� & ���� ���� ���� ����
                Gamemanager.Inst.IsGameEnd = true;
                Gamemanager.Inst.VictoryTeam = enterBlock.myTeam;

                // �¸� & �й� �ִϸ��̼� ��� ���
                WinnerTeamAction(Gamemanager.Inst.VictoryTeam);
                LoseTeamAction(Gamemanager.Inst.VictoryTeam);

                // �� ��ȯ ����
                //???
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BlockObject>() != null)
        {
            enterBlock = other.GetComponent<BlockObject>();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent <BlockObject>() != null)
        {
            if(other.GetComponent<BlockObject>() == enterBlock)
            {
                enterBlock = null;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<BlockObject>() != null)
        {
            if(enterBlock == null)
            {
                enterBlock = other.GetComponent<BlockObject>();
            }
        }
    }

    public void WinnerTeamAction(HitScanner.Team WinnerTeam)
    {
        //Gamemanager.Inst.GetTeam(WinnerTeam);
        if (WinnerTeam == HitScanner.Team.NotSetting) return;
        Gamemanager.Inst.currentPlayer.WinAction();
        // ���� ���ε�� ����� �κ�
        //for (int i = 0; i < Gamemanager.Inst.GetTeamId(WinnerTeam).Count; i++)
        //{
        //    // �¸� �ִϸ��̼� ����
        //    //if (Gamemanager.Inst.)
        //    Gamemanager.Inst.GetTeam(WinnerTeam)
        //        [Gamemanager.Inst.GetWinnerTeamId()[i]].WinAction();
        //}

    }
    public void LoseTeamAction(HitScanner.Team WinnerTeam)
    {
        //Gamemanager.Inst.GetEnemyTeam(WinnerTeam);
        if (WinnerTeam == HitScanner.Team.NotSetting) return;
        Gamemanager.Inst.currentPlayer.LoseAction();

        // ���� ���ε�� ����Һκ�
        //for (int i = 0; i < Gamemanager.Inst.GetTeamId((HitScanner.Team)((int)WinnerTeam*-1)).Count; i++)
        //{
        //    // �¸� �ִϸ��̼� ����
        //    Gamemanager.Inst.GetEnemyTeam(WinnerTeam)
        //        [Gamemanager.Inst.GetLoserTeamId()[i]].LoseAction();
        //}
    }



}
