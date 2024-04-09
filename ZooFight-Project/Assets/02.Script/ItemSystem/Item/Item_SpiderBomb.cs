using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¾ÆÀÌÅÛ¸í : °Å¹ÌÁÙ ÆøÅº
/// Value 1 µĞÈ­·®
/// Value 2 È¿°ú Áö¼Ó ½Ã°£
/// Value 3 Æø¹ß ¹üÀ§
/// Value 4 ÆøÅº ¼Óµµ
/// Value 5 ÆøÅº »ç°Å¸®
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
}
