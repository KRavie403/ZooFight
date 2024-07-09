using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 먹물폭탄
/// Value 1 시야 방해 정도
/// Value 2 효과 지속 시간
/// Value 3 효과 적용 범위
/// Value 4 폭탄 속도
/// Value 5 사거리
/// </summary>

public class Item_InkBomb : Items
{
    public Item_InkBomb(PlayerController player) : base(player)
    {

    }

    public HitScanner HitScanner;


    bool isItemActive = false;

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



    public override void Initate(List<float> Values, PlayerController player)
    {
        base.Initate(Values, player);

    }

    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();

        float duringTime = 0.0f;

        bool isActive = false;

        while (duringTime < Value2)
        {

            if (isActive)
            {
                duringTime += Time.deltaTime;
            }

            if(Targets.Count != 0)
            {

            }


        }
    }



    void BombEffectSetting()
    {

    }
    void BombSoundSetting()
    {

    }




}
