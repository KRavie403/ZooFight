using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 파워 드링크
/// Value 1 방어량
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

    public IEnumerator DrinkActive()
    {
        // 회복대상 캐릭터 확정

        // 회복 이펙트 출력준비

        // 회복 사운드 재생준비

        // 회복 동작 작동
        while (true)
        {
            yield return null;
        }
    }

}
