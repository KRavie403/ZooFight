using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : ����ȯ��ũ��
/// Value 1 
/// Value 2 ����Ʈ ���ӽð�
/// Value 3 ???
/// </summary>

public class Item_BlockChangeScroll : Items
{
    public Item_BlockChangeScroll(PlayerController player) : base(player)
    {

    }

    [SerializeField]
    EffectPlayer myEffect;
    bool isEffectPlay = false;
    [SerializeField]
    SoundSpeaker mySound;
    bool isSoundPlay = false;   

    //public GameObject myBlockObj;
    public BlockObject myBlock;
    //public GameObject enemyBlockObj;
    public BlockObject enemyBlock;

    protected override void Awake()
    {
        base.Awake();

        myCode = ItemCode.BlockChangeScroll;
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();

    }

    public override void ItemUse()
    {
        base.ItemUse();



        // �ڷ�ƾ�� ������ ��ü�� ��������
        isItemUse = true;

    }
    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();

        float duringTime = 0;
        // �Ʊ� , ����� ������ ��������
        if (myPlayer.myTeam == HitScanner.Team.RedTeam)
        {
            myBlock = Gamemanager.Inst.RedTeamBlock;
            enemyBlock = Gamemanager.Inst.BlueTeamBlock;
        }
        else if (myPlayer.myTeam == HitScanner.Team.BlueTeam)
        {
            myBlock = Gamemanager.Inst.BlueTeamBlock;
            enemyBlock = Gamemanager.Inst.RedTeamBlock;
        }

        // �����ִ� ���� ����
        myBlock.isChangeActive = true;
        enemyBlock.isChangeActive = true;
        myBlock.ForceDeGrab();
        enemyBlock.ForceDeGrab();
        // �� ��ȯ
        myBlock.ChangeBlockTeam();
        enemyBlock.ChangeBlockTeam();
        

        // ����ȯ ����Ʈ ����غ�

        // ����ȯ ���� ����غ�


        myBlock.isChangeActive = false;
        enemyBlock.isChangeActive = false;

        myPlayer.ItemUseEnd();

        // ����ȯ ����
        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;


            if (isItemUse)
            {
                yield return null;
            }
            else
            {
                yield return base.ItemActions();
            }

            yield return null;
        }

        Debug.Log($"{this} ActiveEnd");
        // ������ ��ȯ
        ReturnItem();
    }

    public override void ReturnItem()
    {
        myBlock = null;
        enemyBlock = null;
        base.ReturnItem();
    }

    public IEnumerator ScrollActive()
    {
        float duringTime = 0;
        // �Ʊ� , ����� ������ ��������
        if(myPlayer.myTeam == HitScanner.Team.RedTeam)
        {
            myBlock = Gamemanager.Inst.RedTeamBlock;
            enemyBlock = Gamemanager.Inst.BlueTeamBlock;
        }
        else if (myPlayer.myTeam == HitScanner.Team.BlueTeam)
        {
            myBlock = Gamemanager.Inst.BlueTeamBlock;
            enemyBlock = Gamemanager.Inst.RedTeamBlock;
        }


        // ����ȯ ����Ʈ ����غ�

        // ����ȯ ���� ����غ�


        // ����ȯ ����
        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;   

            yield return null;
        }




    }



}
