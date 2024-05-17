using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryDecision : MonoBehaviour
{
    [SerializeField]
    HitScanner.Team DecisionTeam = HitScanner.Team.NotSetting;

    BlockObject enterBlock;

    private Coroutine movingNextSceneCoroutine;

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
                //if (movingNextSceneCoroutine != null)
                //{
                //    StopCoroutine(movingNextSceneCoroutine);
                //}
                movingNextSceneCoroutine = StartCoroutine(MovingNextScene());
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
        //for (int i = 0; i < Gamemanager.Inst.GetTeamId((myHitScanner.Team)((int)WinnerTeam*-1)).Count; i++)
        //{
        //    // �¸� �ִϸ��̼� ����
        //    Gamemanager.Inst.GetEnemyTeam(WinnerTeam)
        //        [Gamemanager.Inst.GetLoserTeamId()[i]].LoseAction();
        //}
    }

    IEnumerator MovingNextScene()
    {
        Debug.Log("MovingNextScene");

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(3);

        //  �� �ε� �� �ڷ�ƾ ������ �ʱ�ȭ
        movingNextSceneCoroutine = null;
    }

}
