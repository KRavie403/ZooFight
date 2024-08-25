using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 스테미너드링크
/// Value 1 스테미너 회복량
/// Value 2 스테미너 회복시간 - 미사용 가능성 존재
/// 1 과 2를 합치면 총 시간
/// </summary>

public class Item_StaminaDrink : Items
{

    public Item_StaminaDrink(PlayerController player) : base(player)
    {

    }

    protected override void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void ItemUse()
    {
        base.ItemUse();
    }




    // 모션동작동안 회복 & 애니메이션 & 이펙트 출력동작
    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();

        float duringTime = 0;



        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;

            myPlayer.CurSP += Time.deltaTime * (Value1 / Value2);

            yield return null;

        }



    }




}
