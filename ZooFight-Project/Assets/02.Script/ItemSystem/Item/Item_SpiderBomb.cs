using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 거미줄 폭탄
/// Value 1 둔화량
/// Value 2 효과 지속 시간
/// Value 3 폭발 범위
/// Value 4 폭탄 속도
/// Value 5 폭탄 사거리
/// </summary>

public class Item_SpiderBomb : Items
{
    public Item_SpiderBomb(PlayerController player) : base(player)
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

    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();

        float duringTime = 0;

        
        // 기본 속도값 저장
        float BaseSpeedRate = 1.0f;

        myPlayer.BaseSpeedRate = Value1;



        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;


        }

        myPlayer.BaseSpeedRate = BaseSpeedRate;

        ReturnItem();
    }

    public override void Initate(List<float> Values, PlayerController player)
    {
        base.Initate(Values, player);

    }

    public override void ItemUse()
    {
        base.ItemUse();

        isItemUse = true;
    }



}
