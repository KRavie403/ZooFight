using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 뿅망치
/// Value 1 효과 지속시간
/// Value 2 발동 범위
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
        // 방향 설정
        Vector3 dir = Vector3.Normalize(pos - myPlayer.transform.position);

        // 해머 이펙트 출력대기

        // 해머 사운드 재생대기


        // 해머 휘두르기
        while (true)
        {

            // 타격 감지시 이펙트 사운드 출력 & 타격대상 스턴


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
