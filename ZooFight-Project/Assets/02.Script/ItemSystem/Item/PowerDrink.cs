using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �Ŀ� �帵ũ
/// 
/// </summary>


public class PowerDrink : Items
{

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

    public IEnumerator DrinkActive()
    {
        // ȸ����� ĳ���� Ȯ��

        // ȸ�� ����Ʈ ����غ�

        // ȸ�� ���� ����غ�

        // ȸ�� ���� �۵�
        while (true)
        {
            yield return null;
        }
    }

}
