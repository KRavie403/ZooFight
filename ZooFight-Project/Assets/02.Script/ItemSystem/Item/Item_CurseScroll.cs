using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �����۸� : ���� ��ũ��
/// Value 1 ���ӽð�
/// 
/// </summary>


public class Item_CurseScroll : Items
{
    public Item_CurseScroll(PlayerController player) : base(player)
    {

    }


    public EffectPlayer effectPlayer;

    HitScanner.Team TargetTeam = 0;

    List<PlayerController> myTargets = new List<PlayerController>();

    protected override void Awake()
    {
        base.Awake();
        myCode = ItemCode.CurseScroll;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void ItemHitAction()
    {
        base.ItemHitAction();

    }

    public override void Initate(List<float> Values, PlayerController player)
    {
        base.Initate(Values, player);
    }

    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();

        float duringTime = 0.0f;

        // ��� ĳ���� Ȯ��
        //myPlayer.myTeam
        PlayerController[] TargetCharcaters = new PlayerController[] { };


        // ��� ĳ���� Ű ����
        foreach (PlayerController c in TargetCharcaters)
        {
            c.isKeyReverse = true;
        }

        // 

        // ���� ����Ʈ ��� ���
        CuresEffectSetting();
        // ���� ���� ��� ���
        CurseSoundSetting();



        while (duringTime < Value1)
        {
            duringTime += Time.deltaTime;

            Gamemanager.Inst.GetEnemyTeam(myPlayer.myTeam);


            yield return null;

        }

        // ��� ĳ���� Ű ���� ����
        foreach (PlayerController c in TargetCharcaters)
        {
            c.isKeyReverse = false;
        }

        // ������ ��ȯ
        ReturnItem();
    }

    public void CuresTargetSetting()
    {
        if(myPlayer.myTeam != HitScanner.Team.NotSetting)
        {
            TargetTeam = (HitScanner.Team)((int)myPlayer.myTeam * -1);
            // Ÿ������ ������ �����ؿ���

        }
    }

    public void CuresEffectSetting()
    {
        for (int i = 0; i < myTargets.Count; i++)
        {
            // ���� ����Ʈ �Ŵ����� ���ؼ� �ߵ��Ҽ��ֵ��� ��������
            effectPlayer.EffectPlay(0, myTargets[i].transform);
        }
    }

    public void CurseSoundSetting()
    {
        // ������� ���� �ʿ�
        for (int i = 0; i < myTargets.Count; i++)
        {
            // ���� ���� �Ŵ����� ���ؼ� �ߵ��Ҽ��ֵ��� ��������

        }
    }


}
