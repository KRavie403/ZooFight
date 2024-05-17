using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// 아이템명 : 바나나
/// Value 1 밀려나는 시간
/// Value 2 함정 유지시간
/// Value 3 밀려나는 거리
/// Value 4 투사체 속도 
/// Value 5 투척 사거리
/// </summary>

public class Item_BananaTrap : Items
{
    public Item_BananaTrap(PlayerController player) : base(player) 
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

        // 히트스캐너 생성
        if(GetComponent<HitScanner>() != null )
        {
            myHitScanner = GetComponent<HitScanner>();
            myHitScanner.enabled = false;
        }
        else
        {
            // 미실행 상태로 생성
            myHitScanner = transform.AddComponent<HitScanner>();
            myHitScanner.enabled = false;
        }

        myCode = ItemCode.BananaTrap;

        
    }

    protected override void Start()
    {
        base.Start();
        
        Transform[] objs = GetComponentsInChildren<Transform>();
        foreach( Transform obj in objs )
        {
            if( obj.gameObject.name == "NonActiveObj")
            {
                NonActiveObj = obj.gameObject;
            }
            if( obj.gameObject.name == "ActivedObj")
            {
                ActivedObj = obj.gameObject;
            }
        }

        myHitScanner.SetMyTeam(myPlayer.myTeam);
        
    }

    protected override void Update()
    {
        base.Update();

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
    }


    public override void Initate(List<float> Values, PlayerController player)
    {
        base.Initate(Values, player);

        myHitScanner.Initiate(this, myPlayer.GetEnemyTeam());
    }



    public override void ItemUse()
    {
        base.ItemUse();

        // 아이템 발동시작시 필요한 동작

        // 코루틴의 동작을 본체로 가져오기
        isItemUse = true;
    }

    public override void ItemHitAction()
    {
        base.ItemHitAction();

    }


    protected override IEnumerator ItemActions()
    {
        // 정보갱신 돌리기
        yield return base.ItemActions();
        transform.SetParent(null);

        bool isMoveEnd = false;

        //transform.localPosition = Vector3.zero;
        // 특정 경로로 이동 시작
        BattleSystems.Inst.routeMaker.RouteKeys[RouteTypes.Arc].
            GetComponent<IRoute>().RouteStart(transform, dir, Value5, Value4, () =>
            {
                NonActiveObj.SetActive(false);
                ActivedObj.SetActive(true);
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
            duringTime += Time.deltaTime;
            // 유지시간 체크


            // 동작감지시 효과적용 및 이펙트 , 사운드 출력
            if(Targets != null)
            {
                // 타겟이 잡히면 동작시키고 터트림
                foreach (var target in Targets)
                {
                    if(target.GetComponent<PlayerController>() != null)
                    {

                    }
                }

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

        // 사용이 끝나면 오브젝트 반환
        ReturnItem();

    }



}
