using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 소형화 광선
/// Value 1 효과 지속 시간
/// Value 2 사출시간
/// Value 3 레이저 길이
/// Value 4 사출속도
/// Value 5 레이저 사거리
/// </summary>


public class Item_MinimalRazer : Items 
{
   

    public EffectPlayer effectPlayer;
    // 방향테스트용
    public GameObject Test1;


    Vector3 Dir = Vector3.zero;


    protected override void Awake()
    {
        base.Awake();
        //Effectmanager.Inst.CreateEffectObj(effectPlayer, transform);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
       
        //effectPlayer = EffectSetting.keys[EffectCode.Item_MinimalRazer];

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Standby(ItemSystem.ItemType itemType)
    {
        base.Standby(itemType);
        
        base.ItemAction = RazerShooting(Dir);
        //Effectmanager.Inst.GetEffectObj(effectPlayer)
    }

    
    public IEnumerator RazerShooting(Vector3 pos)
    {
        // 사출방향 설정
        Vector3 dir = Vector3.Normalize(pos - myPlayer.transform.position) * Value1;



        // 이펙트 발동상태 설정 - 함수화 예정
        RazerEffectSetting();

        // 사운드 재생상태 설정 - 함수화 예정
        RazerSoundSetting();

        //effectPlayer.myEffect[1].main.startLifetime;

        // 사출시간동안 동작
        while (effectPlayer.myEffect[1].main.duration < Value4)
        {

            yield return null;
        }

    }
    
    public void RazerEffectSetting()
    {
        // 1번이펙트 간단실행

        // 2번이펙트 사출거리만큼 실행

    }

    public void RazerSoundSetting()
    {
        // 사출 사운드 재생
    }

    public void RazerShoot()
    {

    }

    public override void ItemUse(PlayerController player)
    {
        base.ItemUse(player);
        //effectPlayer.
    }


}
