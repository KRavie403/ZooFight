using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 블럭교환스크롤
/// Value 1 지속시간
/// 
/// </summary>

public class BlockChangeScroll : Items
{
    [SerializeField]
    EffectPlayer myEffect;

    public GameObject myBlock;
    public GameObject enemyBlock;

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

    public IEnumerator ScrollActive()
    {
        // 아군 , 상대편 블럭정보 가져오기
        //myBlock = Gamemanager.Inst.???
        //enemyBlock = Gamemanager.Inst.???

        // 블럭교환 이펙트 출력준비

        // 블럭교환 사운드 출력준비


        // 블럭교환 실행
        while (true)
        {

        }
    }



}
