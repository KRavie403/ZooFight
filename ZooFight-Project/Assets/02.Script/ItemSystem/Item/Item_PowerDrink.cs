using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �Ŀ� �帵ũ
/// Value 1 �̵� �ӵ� ������
/// Value 2 ���� �ð�
/// 
/// </summary>


public class Item_PowerDrink : Items
{
    public Item_PowerDrink(PlayerController player) : base(player)
    {

    }

    EffectPlayer myEffect;

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

        // �⺻ �ӵ��� ����
        float BaseSpeedRate = 1.0f;

        myPlayer.BaseSpeedRate = Value1;

        // ����Ʈ ���

        // ���ô� ��� ���
        myPlayer.myAnim.SetBool("isDrink", true);
        myPlayer.myAnim.SetTrigger("Drinking");

        myPlayer.ItemUseEnd();


        // ���ӽð� üũ
        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;


            // UI �߰��ҽ� ������������ ���⼭

            yield return null;

        }

        // ���ӽð� ���� �� �ӵ� ����
        myPlayer.BaseSpeedRate = BaseSpeedRate;
        ReturnItem();
    }


}
