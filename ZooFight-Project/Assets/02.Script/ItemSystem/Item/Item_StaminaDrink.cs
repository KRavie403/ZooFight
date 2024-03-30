using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 스테미너드링크
/// Value 1 스테미너 회복량
/// Value 2 스테미너 회복시간 - 미사용 가능성 존재
/// 
/// </summary>

public class Item_StaminaDrink : Items
{


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

    public override void ItemUse(PlayerController player)
    {
        base.ItemUse(player);
        base.ItemAction = DrinkAction();
    }


    // 모션동작동안 회복 & 애니메이션 & 이펙트 출력동작
    public IEnumerator DrinkAction()
    {
        // 회복


        // 애니메이션

        // 이펙트


        while (true)
        {
            yield return null;
        }
    }


}
