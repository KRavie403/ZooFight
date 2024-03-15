using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 폭탄
/// Value 1 사거리
/// Value 2 폭발범위
/// Value 3 밀어내는 범위
/// </summary>


public class Bomb : Items
{
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
