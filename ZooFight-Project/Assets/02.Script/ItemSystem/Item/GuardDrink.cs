using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �������
/// Value 1 ����
/// Value 2 ���ӽð�
/// 
/// </summary>


public class GuardDrink : Items
{

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
