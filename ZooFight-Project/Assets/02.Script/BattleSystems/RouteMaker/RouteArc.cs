using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 포물선 경로 설정하는 클래스
/// 
/// </summary>


public class RouteArc : Route , IRoute  
{
    RouteTypes curType = RouteTypes.Arc;
    RouteTypes IRoute.Type { get => curType; }

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

    void IRoute.RouteStart(Vector3 pos)
    {
        throw new System.NotImplementedException();
    }

    float startHeight = 1.5f;
    float ShootAngle = 45.0f;
    float DropSpeed = 2.0f;
    float endHeight = 0.5f;

    [SerializeField]
    float4 RouteInfo;
    // <StartHeight,EndHeight,DropSpeed,ShootAngle>

    protected override void RouteSetUp(Vector3 pos, Vector4 routeInfo,Transform target,PlayerController player)
    {
        base.RouteSetUp(pos, RouteInfo, target, player);
        RouteInfo = routeInfo;
        SetupCoroutine = RouteMaker(pos, target, player, RouteInfo);
        Debug.Log("aa");
    }

    Transform myTargetObj = null;
    PlayerController myPlayer = null;
    IEnumerator SetupCoroutine = null;

    void IRoute.RouteSetUp(Vector3 pos,Transform target, PlayerController player)
    {
        SetupCoroutine = RouteMaker(pos, target, player,RouteInfo);

    }


    public IEnumerator RouteMaker(Vector3 Value, Transform Target, PlayerController player,float4 Rinfo)
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

}
