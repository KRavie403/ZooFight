using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : ����ȯ��ũ��
/// Value 1 ���ӽð�
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

    public GameObject myBlockObj;
    public BlockObject myBlock;
    public GameObject enemyBlockObj;
    public BlockObject enemyBlock;

    protected override void Awake()
    {
        base.Awake();

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
        // ����ȯ ����
        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;


            yield return null;
        }

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
