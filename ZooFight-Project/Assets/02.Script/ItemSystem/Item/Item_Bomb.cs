using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : ��ź
/// Value 1 ������
/// Value 2 
/// Value 3 ���߹���
/// Value 4 ��ź �ӵ�
/// Value 5 ��Ÿ�
/// </summary>


public class Item_Bomb : Items
{
    public Item_Bomb(PlayerController player) : base(player)
    {

    }

    [SerializeField]
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
    public void BombSetting(Vector3 pos)
    {
        ItemAction = BombActive(pos);

    }

    public IEnumerator BombActive(Vector3 pos)
    {

        while (true)
        {


            yield return null;
        }
    }

}
