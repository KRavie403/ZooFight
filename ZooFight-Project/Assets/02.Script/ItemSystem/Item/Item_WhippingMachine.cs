using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : ���α�
/// Value 1 �̴� ��
/// Value 2 �̴� �ð�
/// Value 3 �̴� �ݰ�
/// Value 4 �̴� ����
/// </summary>

public class Item_WhippingMachine : Items
{
    public Item_WhippingMachine(PlayerController player) : base(player)
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

    public void MachineSetting(Vector3 pos)
    {
        ItemAction = MachineActive(pos);

    }

    public IEnumerator MachineActive(Vector3 pos)
    {
        // ���� ����
        Vector3 dir = Vector3.Normalize(pos - myPlayer.transform.position) * Value4;

        // ����Ʈ ��´��

        // ���� ������


        // ���ӽð����� ����
        while (true)
        {
            // ���ӽð� ī��Ʈ


            // �ǰݴ�� ����

            // �ǰݴ�� ������ ����Ʈ ���� ��� & �о��

            yield return null;
        }
    }




}
