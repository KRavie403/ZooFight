using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VictoryDecision : MonoBehaviour
{
    [SerializeField]
    HitScanner.Team DecisionTeam = HitScanner.Team.NotSetting;

    BlockObject enterBlock;
    bool isVictory = false;
    private void Update()
    {
        if (isVictory) return;
        if (DecisionTeam == HitScanner.Team.NotSetting) return;
        if (enterBlock != null)
        {
            if (enterBlock.myTeam == HitScanner.Team.NotSetting) return;

            if(enterBlock.myTeam == DecisionTeam)
            {
                // 승리 함수 실행
                enterBlock.VictoryDecide(DecisionTeam);
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





}
