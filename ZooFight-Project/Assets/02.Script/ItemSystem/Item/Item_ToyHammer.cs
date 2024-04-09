using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �и�ġ
/// Value 1 ������ -  �̻�� ���ɼ� O
/// Value 2 ���� �ð�
/// Value 3 �ߵ� ���� - Ÿ���������� ����
/// Value 4 
/// Value 5 ��Ÿ� - �ֵθ��� �ִ� ����
/// </summary>

public class Item_ToyHammer : Items
{
    public Item_ToyHammer(PlayerController player) : base(player)
    {

    }


    [SerializeField]
    EffectPlayer myEffect;
    [SerializeField]
    SoundSpeaker mySound;

    public enum HammerAction
    {
        Smash = 0, hit , disappear
    }
    

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
    public override void ItemUse(PlayerController player)
    {
        base.ItemUse(player);
    }
    public override void Initate(List<float> Values, PlayerController player)
    {
        base.Initate(Values, player);
    }
    public override void Standby(Items item, ItemSystem.ActiveType itemType)
    {
        base.Standby(item, itemType);
    }

    public void HammerSetting(Vector3 pos)
    {
        ItemAction = HammerActive(pos);
    }

    public IEnumerator HammerActive(Vector3 pos)
    {
        // ���� ����
        Vector3 dir = Vector3.Normalize(pos - myPlayer.transform.position);

        // �ظ� ����Ʈ ��´��
        HammerEffectSetting();
        // �ظ� ���� ������
        HammerSoundSetting();

        // �ظ� �ֵθ���
        while (!isItemUseEnd)
        {

            // Ÿ�� ������ ����Ʈ ���� ��� & Ÿ�ݴ�� ����

            
            yield return null;
        }
    }

    public void HammerEffectSetting()
    {

    }

    public void HammerSoundSetting()
    {

    }

    public void HammerEffectPlay(HammerAction action)
    {
        switch (action)
        {
            case HammerAction.Smash:
                //Effectmanager.Inst.effectPool.
                break;
            case HammerAction.hit:
                break;
            case HammerAction.disappear:
                break;
            default:
                break;
        }
    }

    public void HammerSoundPlay(HammerAction action)
    {
        switch (action)
        {
            case HammerAction.Smash:
                break;
            case HammerAction.hit:
                break;
            case HammerAction.disappear:
                break;
            default:
                break;
        }
    }

    public override void UseItems()
    {
        base.UseItems();
    }
    
}
