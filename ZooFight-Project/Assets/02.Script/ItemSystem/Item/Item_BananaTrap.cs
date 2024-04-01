using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 바나나
/// Value 1 밀려나는 시간
/// Value 2 함정 유지시간
/// Value 3 밀려나는 거리
/// Value 4 투사체 속도 
/// </summary>

public class Item_BananaTrap : Items
{
    [SerializeField]
    EffectPlayer myEffect;
    [SerializeField]
    GameObject NonActiveObj;
    [SerializeField]
    GameObject ActivedObj;

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

    public void BananaSetting(Vector3 pos)
    {
        ItemAction = TrapActive(pos);
    }

    public IEnumerator TrapActive(Vector3 pos)
    {
        // 사출지점 확정

        // 사출경로 준비

        // 바나나 이펙트 출력준비

        // 바나나 사운드 출력준비


        // 바나나 지속시간동안 동작
        while (true)
        {
            // 유지시간 체크

            // 동작감지시 효과적용 및 이펙트 , 사운드 출력

            // 발동시 오브젝트 작동 불능처리

            yield return null;
        }
    }



}
