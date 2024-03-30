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
                // 승리팀 정보 & 게임 종료 정보 저장
                Gamemanager.Inst.IsGameEnd = true;
                Gamemanager.Inst.VictoryTeam = enterBlock.myTeam;

                // 승리 & 패배 애니메이션 등등 출력
                WinnerTeamAction(Gamemanager.Inst.VictoryTeam);
                LoseTeamAction(Gamemanager.Inst.VictoryTeam);

                // 씬 전환 실행
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
        // 서버 업로드시 사용할 부분
        //for (int i = 0; i < Gamemanager.Inst.GetTeamId(WinnerTeam).Count; i++)
        //{
        //    // 승리 애니메이션 동작
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

        // 서버 업로드시 사용할부분
        //for (int i = 0; i < Gamemanager.Inst.GetTeamId((HitScanner.Team)((int)WinnerTeam*-1)).Count; i++)
        //{
        //    // 승리 애니메이션 동작
        //    Gamemanager.Inst.GetEnemyTeam(WinnerTeam)
        //        [Gamemanager.Inst.GetLoserTeamId()[i]].LoseAction();
        //}
    }



}
