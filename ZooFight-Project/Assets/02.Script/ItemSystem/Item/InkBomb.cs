using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 먹물폭탄
/// Value 1 효과 지속시간
/// Value 2 효과 적용 범위
/// Value 3 ???
/// </summary>

public class InkBomb : Items
{

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

    public void BombShoot(Vector3 Pos)
    {
        // 
        ItemAction = BombActive(Pos);
        //StartCoroutine(BombActive(Pos));
    }

    public IEnumerator BombActive(Vector3 Pos)
    {
        // 발사지점 확정

        // 물체 이동궤적 준비

        // 폭탄 이펙트 준비
        BombEffectSetting();
        // 폭탄 사운드 준비
        BombSoundSetting();
        //

        while (!isItemActive)
        {
            yield return null;
        }
    }

    public override void UseItems()
    {
        base.UseItems();

    }

    void BombEffectSetting()
    {

    }
    void BombSoundSetting()
    {

    }




}
