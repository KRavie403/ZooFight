using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 폭탄
/// Value 1 데미지
/// Value 2 
/// Value 3 폭발범위
/// Value 4 폭탄 속도
/// Value 5 사거리
/// </summary>


public class Item_Bomb : Items
{
    public Item_Bomb(PlayerController player) : base(player)
    {

    }


    [SerializeField]
    EffectPlayer myEffect;
    [SerializeField]
    GameObject NonActiveObj;
    [SerializeField]
    GameObject ActivedObj;


    public HitScanner myHitScanner;


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

    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();
        transform.SetParent(null);

        bool isMoveEnd = false;

        //transform.localPosition = Vector3.zero;
        // 특정 경로로 이동 시작
        BattleSystems.Inst.routeMaker.RouteKeys[RouteTypes.Arc].
            GetComponent<IRoute>().RouteStart(transform, dir, Value5, Value4, () =>
            {
                //NonActiveObj.SetActive(false);
                //ActivedObj.SetActive(true);
                myHitScanner.Initiate(this, myPlayer.GetEnemyTeam());
                isMoveEnd = true;
                myPlayer.ItemUseEnd();
            });
        //myPlayer.SetState()
        float duringTime = 0;
        myHitScanner.SetScanActive(true);
        // 바나나 지속시간동안 동작
        while (duringTime < Value2)
        {

            // 던지는동안 지속시간 감소 X
            if (!isMoveEnd)
            {
                yield return null;
                continue;
            }

            // 유지시간 체크
            duringTime += Time.deltaTime;


            // 동작감지시 효과적용 및 이펙트 , 사운드 출력
            if (Targets != null)
            {
                // 타겟이 잡히면 동작시키고 터트림


                // duringTime = Value2;
            }

            // 발동시 오브젝트 작동 불능처리

            if (isItemUse)
            {
                yield return null;

            }
            else
            {
                yield return base.ItemActions();
            }
            yield return null;
        }
        myHitScanner.SetScanActive(false);
        // 사용이 끝나면 오브젝트 반환
        ReturnItem();
    }

    

    public IEnumerator BombActive(Vector3 pos)
    {

        while (true)
        {


            yield return null;
        }
    }

}
