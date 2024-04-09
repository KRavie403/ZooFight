using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¾ÆÀÌÅÛ¸í : ÆøÅº
/// Value 1 µ¥¹ÌÁö
/// Value 2 
/// Value 3 Æø¹ß¹üÀ§
/// Value 4 ÆøÅº ¼Óµµ
/// Value 5 »ç°Å¸®
/// </summary>


public class Item_Bomb : Items
{
    public Item_Bomb(PlayerController player) : base(player)
    {

    }

    [SerializeField]
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
    public void BombSetting(Vector3 pos)
    {
        ItemAction = BombActive(pos);

    }

    public IEnumerator BombActive(Vector3 pos)
    {

        while (true)
        {


            yield return null;
        }
    }

}
