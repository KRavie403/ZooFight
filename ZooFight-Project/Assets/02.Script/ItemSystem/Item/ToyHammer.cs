using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �и�ġ
/// Value 1 ȿ�� ���ӽð�
/// Value 2 �ߵ� ����
/// ???
/// </summary>

public class ToyHammer : Items
{
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

    public void HammerSetting(Vector3 pos)
    {
        ItemAction = HammerActive(pos);
    }

    public IEnumerator HammerActive(Vector3 pos)
    {
        // ���� ����
        Vector3 dir = Vector3.Normalize(pos - myPlayer.transform.position);

        // �ظ� ����Ʈ ��´��

        // �ظ� ���� ������


        // �ظ� �ֵθ���
        while (true)
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

}
