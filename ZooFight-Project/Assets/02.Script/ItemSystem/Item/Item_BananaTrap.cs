using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �ٳ���
/// Value 1 �з����� �ð�
/// Value 2 ���� �����ð�
/// Value 3 �з����� �Ÿ�
/// Value 4 ����ü �ӵ� 
/// </summary>

public class Item_BananaTrap : Items
{
    [SerializeField]
    EffectPlayer myEffect;
    [SerializeField]
    GameObject NonActiveObj;
    [SerializeField]
    GameObject ActivedObj;

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

    public void BananaSetting(Vector3 pos)
    {
        ItemAction = TrapActive(pos);
    }

    public IEnumerator TrapActive(Vector3 pos)
    {
        // �������� Ȯ��

        // ������ �غ�

        // �ٳ��� ����Ʈ ����غ�

        // �ٳ��� ���� ����غ�


        // �ٳ��� ���ӽð����� ����
        while (true)
        {
            // �����ð� üũ

            // ���۰����� ȿ������ �� ����Ʈ , ���� ���

            // �ߵ��� ������Ʈ �۵� �Ҵ�ó��

            yield return null;
        }
    }



}
