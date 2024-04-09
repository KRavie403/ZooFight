using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 휘핑기
/// Value 1 미는 힘
/// Value 2 미는 시간
/// Value 3 미는 반경
/// Value 4 미는 길이
/// </summary>

public class Item_WhippingMachine : Items
{
    public Item_WhippingMachine(PlayerController player) : base(player)
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

    public void MachineSetting(Vector3 pos)
    {
        ItemAction = MachineActive(pos);

    }

    public IEnumerator MachineActive(Vector3 pos)
    {
        // 방향 지정
        Vector3 dir = Vector3.Normalize(pos - myPlayer.transform.position) * Value4;

        // 이펙트 출력대기

        // 사운드 재생대기


        // 지속시간동안 동작
        while (true)
        {
            // 지속시간 카운트


            // 피격대상 감지

            // 피격대상 감지시 이펙트 사운드 출력 & 밀어내기

            yield return null;
        }
    }




}
