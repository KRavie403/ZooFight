using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 파워 드링크
/// Value 1 이동 속도 증가량
/// Value 2 동작 시간
/// 
/// </summary>


public class Item_PowerDrink : Items
{
    public Item_PowerDrink(PlayerController player) : base(player)
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

    public override void ItemUse()
    {
        base.ItemUse();


    }

    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();

        float duringTime = 0;

        // 기본 속도값 저장
        float BaseSpeedRate = 1.0f;

        myPlayer.BaseSpeedRate = Value1;

        // 지속시간 체크
        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;

            yield return null;

        }

        // 지속시간 종료 후 속도 복귀
        myPlayer.BaseSpeedRate = BaseSpeedRate;
        ReturnItem();
    }

    public IEnumerator DrinkActive()
    {
        // 회복대상 캐릭터 확정

        // 회복 이펙트 출력준비

        // 회복 사운드 재생준비

        // 회복 동작 작동
        myPlayer.isShield = true;
        while (myPlayer.isShield)
        {


            yield return null;
        }
    }

}
