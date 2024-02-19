using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Value1 레이저길이
// Value2 사출속도
// Value3 효과지속시간
// Value4 
public class MinimalRazer : Items
{

    public EffectPlayer effectPlayer;
    // 방향테스트용
    public GameObject Test1;


    Vector3 Dir = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        Effectmanager.Inst.CreateEffectObj(effectPlayer, transform);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
       
        effectPlayer = EffectSetting.keys[EffectCode.MinimalRazer];

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

        Vector3 dir = new Vector3(pos.x, 0, pos.z);

        //effectPlayer.myEffect[1].main.startLifetime;

        while (effectPlayer.myEffect[1].main.duration < 1.0)
        {

            yield return null;
        }

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
