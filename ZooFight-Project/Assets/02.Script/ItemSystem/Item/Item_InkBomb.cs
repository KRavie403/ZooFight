using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �Թ���ź
/// Value 1 �þ� ���� ����
/// Value 2 ȿ�� ���� �ð�
/// Value 3 ȿ�� ���� ����
/// Value 4 ��ź �ӵ�
/// Value 5 ��Ÿ�
/// </summary>

public class Item_InkBomb : Items
{
    public Item_InkBomb(PlayerController player) : base(player)
    {

    }

    public HitScanner HitScanner;


    bool isItemActive = false;

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



    public override void Initate(List<float> Values, PlayerController player)
    {
        base.Initate(Values, player);

    }

    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();

        float duringTime = 0.0f;

        bool isActive = false;

        while (duringTime < Value2)
        {

            if (isActive)
            {
                duringTime += Time.deltaTime;
            }

            if(Targets.Count != 0)
            {

            }


        }
    }



    void BombEffectSetting()
    {

    }
    void BombSoundSetting()
    {

    }




}
