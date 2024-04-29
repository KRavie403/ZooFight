using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 포물선 경로 설정하는 클래스
/// 
/// </summary>


public class Route_Arc : Route , IRoute  
{
    public Route_Arc() : base()
    {

    }

    
    RouteTypes IRoute.Type { get => myRouteType; }

    Component IRoute.Comp => this as Component;

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

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    IEnumerator RouteCoroutine = null;

    protected override void RouteStart(Transform Target, Vector3 Dir, float Speed,float Dist, UnityAction e = null)
    {
        base.RouteStart(Target, Dir,Dist ,Speed);

        RouteCoroutine = RouteMaker2(Target, Dir, Speed,Dist,e);
        if (RouteCoroutine != null)
        {
            StartCoroutine(RouteCoroutine);
        }
        //e?.Invoke();
    }


    void IRoute.RouteStart(Transform Target, Vector3 Dir, float Speed,float Dist, UnityAction e = null)
    {

        RouteStart(Target, Dir, Speed, Dist, e);
    }

    public IEnumerator RouteMaker(Vector3 Value, Transform Target, PlayerController player, float4 Rinfo, UnityAction e = null)
    {
        // 변수 초기화
        float duringTime = 0;

        //DrawRoute(Value,Target,player);

        // 기본벡터 정의
        Vector3 startPos = player.transform.position;
        Vector3 dir = Value - startPos;

        //startPos.y = dir.y = startHeight;
        startPos.y = dir.y = Rinfo.x;

        Vector3 dirUp = Vector3.up;

        float dist = dir.magnitude;
        float VelX = dist / (Mathf.Cos(Mathf.Deg2Rad * Rinfo.w) * Rinfo.z);
        float VelY = dist / Mathf.Sin(Mathf.Deg2Rad * Rinfo.w);

        float downForce = (VelY * 2) / Rinfo.z;

        float distX = 0.0f;
        float distY = 0.0f;

        Vector3 dirN = Vector3.Normalize(dir);

        while (Target.position.y < Rinfo.y)
        {
            duringTime += Time.deltaTime;

            VelY -= Time.deltaTime * downForce;

            distX += VelX * Time.deltaTime;
            distY += VelY * Time.deltaTime;
            Target.position = startPos + dirN * distX + dirUp * distY;

            yield return null;
        }
    }

    public IEnumerator RouteMaker2(Transform Target,Vector3 Dir,float Speed,float Dist, UnityAction e = null)
    {
        float duringTime = 0;

        Debug.Log($"{Target.name} move {Dir} ,Speed {Speed}");
        // 플레이어의 전방벡터를 가져옴
        Vector3 dir = Vector3.Normalize(Dir);
        Vector3 StartPos = Target.position;
        Vector3 pos = Vector3.zero;

        float WholeTime = Dist / Speed;
        float StartHeight = Target.position.y;
        
        while (!isEnd)
        {
            duringTime += Time.deltaTime;

            pos = StartPos + dir * (Dist * duringTime / WholeTime);
            pos.y = StartHeight + Dist * Mathf.Sin(duringTime / WholeTime);

            Target.position = pos;



            // 지면보다 낮아지면 멈추게하기
            if(Target.position.y < StartHeight)
            {
                isEnd = true;
            }

            yield return null;  
        }

        e?.Invoke();
    }


}
