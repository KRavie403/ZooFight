using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �Ź��� ��ź
/// Value 1 ��ȭ��
/// Value 2 ȿ�� ���� �ð�
/// Value 3 ���� ����
/// Value 4 ��ź �ӵ�
/// Value 5 ��ź ��Ÿ�
/// </summary>

public class Item_SpiderBomb : Items
{
    public Item_SpiderBomb(PlayerController player) : base(player)
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
}
