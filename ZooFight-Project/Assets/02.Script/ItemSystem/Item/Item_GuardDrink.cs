using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �������
/// Value 1 ����
/// Value 2 ���ӽð�
/// Value 3
/// 
/// </summary>


public class Item_GuardDrink : Items
{
    public Item_GuardDrink(PlayerController player) : base(player)
    {

    }


    EffectPlayer myEffect;

    protected override void Update()
    {
        base.Update();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initate(List<float> Values, PlayerController player)
    {
        base.Initate(Values, player);
        if(Values.Count >= 2)
        {
            Value1 = Values[0];
            Value2 = Values[1];
        }
        myPlayer = player;
    }

    public void ItemSetUp()
    {

    }

    public IEnumerator DrinkActive()
    {
        // ��ȣ�� ��� ����

        // ��ȣ�� ����Ʈ ����غ�

        // ��ȣ�� ���� ����غ�


        // ��ȣ�� ���ӵ��� ����
        while (true)
        {
            // ���ӽð� ī��Ʈ
            
            // �ǰݰ����� ���� ����Ʈ ���

            // �ǰݽ� ��ȣ���� ����

            yield return null;
        }
    }



}
