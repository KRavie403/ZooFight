using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : ����ȭ ����
/// Value 1 ȿ�� ���� �ð�
/// Value 2 ����ð�
/// Value 3 ������ ����
/// Value 4 ����ӵ�
/// Value 5 ������ ��Ÿ�
/// </summary>


public class Item_MinimalRazer : Items 
{
   

    public EffectPlayer effectPlayer;
    // �����׽�Ʈ��
    public GameObject Test1;


    Vector3 Dir = Vector3.zero;


    protected override void Awake()
    {
        base.Awake();
        //Effectmanager.Inst.CreateEffectObj(effectPlayer, transform);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
       
        //effectPlayer = EffectSetting.keys[EffectCode.Item_MinimalRazer];

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Standby(ItemSystem.ItemType itemType)
    {
        base.Standby(itemType);
        
        base.ItemAction = RazerShooting(Dir);
        //Effectmanager.Inst.GetEffectObj(effectPlayer)
    }

    
    public IEnumerator RazerShooting(Vector3 pos)
    {
        // ������� ����
        Vector3 dir = Vector3.Normalize(pos - myPlayer.transform.position) * Value1;



        // ����Ʈ �ߵ����� ���� - �Լ�ȭ ����
        RazerEffectSetting();

        // ���� ������� ���� - �Լ�ȭ ����
        RazerSoundSetting();

        //effectPlayer.myEffect[1].main.startLifetime;

        // ����ð����� ����
        while (effectPlayer.myEffect[1].main.duration < Value4)
        {

            yield return null;
        }

    }
    
    public void RazerEffectSetting()
    {
        // 1������Ʈ ���ܽ���

        // 2������Ʈ ����Ÿ���ŭ ����

    }

    public void RazerSoundSetting()
    {
        // ���� ���� ���
    }

    public void RazerShoot()
    {

    }

    public override void ItemUse(PlayerController player)
    {
        base.ItemUse(player);
        //effectPlayer.
    }


}
