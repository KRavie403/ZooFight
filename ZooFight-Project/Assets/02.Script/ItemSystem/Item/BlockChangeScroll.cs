using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : ����ȯ��ũ��
/// Value 1 ���ӽð�
/// 
/// </summary>

public class BlockChangeScroll : Items
{
    [SerializeField]
    EffectPlayer myEffect;

    public GameObject myBlock;
    public GameObject enemyBlock;

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

    public IEnumerator ScrollActive()
    {
        // �Ʊ� , ����� ������ ��������
        //myBlock = Gamemanager.Inst.???
        //enemyBlock = Gamemanager.Inst.???

        // ����ȯ ����Ʈ ����غ�

        // ����ȯ ���� ����غ�


        // ����ȯ ����
        while (true)
        {

        }
    }



}
