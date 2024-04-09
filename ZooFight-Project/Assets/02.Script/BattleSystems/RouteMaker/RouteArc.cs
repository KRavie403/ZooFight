using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// ������ ��� �����ϴ� Ŭ����
/// 
/// </summary>


public class RouteArc : Route , IRoute  
{
    public RouteArc() : base()
    {

    }

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

    void IRoute.RouteStart(Vector3 Angle)
    {
        throw new System.NotImplementedException();
    }


    protected override void RouteSetUp(Transform target, PlayerController player, Vector3 angle)
    {
        base.RouteSetUp(target, player, angle);

        Debug.Log("aa");
    }


    IEnumerator SetupCoroutine = null;

    void IRoute.RouteSetUp(Vector3 pos,Transform target, PlayerController player)
    {

    }

    public void MoveStart()
    {
        StartCoroutine(SetupCoroutine);
    }

    public IEnumerator RouteMaker(Vector3 Value, Transform Target, PlayerController player,float4 Rinfo)
    {
        // ���� �ʱ�ȭ
        float duringTime = 0;

        //DrawRoute(Value,Target,player);

        // �⺻���� ����
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

    public IEnumerator RouteMaker2(Transform Target,PlayerController player,Vector3 angle,float Speed)
    {
        float duringTime = 0;

        // �÷��̾��� ���溤�͸� ������
        Vector3 dir = player.transform.forward;


        while (true)
        {
            duringTime += Time.deltaTime;

            // �÷��̾��� ���溤�͸� �߻�ӵ��� ����


            yield return null;  
        }


    }


}
