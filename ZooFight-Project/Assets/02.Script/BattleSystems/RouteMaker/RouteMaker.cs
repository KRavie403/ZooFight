using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이동 경로를 만들어주는 클래스
/// 포물선 , 원형 , 직선 등등 추가예정
/// </summary>

public enum RouteTypes
{
    Arc = 0,
    Circle ,
    straight
}

interface IRoute
{
    public RouteTypes Type { get;  }
    Component Comp { get; }
    public void RouteStart(Vector3 pos);

    public void RouteSetUp(Vector3 pos, Transform target, PlayerController curPlayer);

}

public struct RouteInfo
{
    public Vector3 Pos;
    public Transform Target;
    public PlayerController myPlayer;

    public float StartHeight;
    public float DirAngle;
    public float DownForce;
    public float EndHeight;

    public Vector4 RouteBase;

    public void BaseSetup(float startHeight,float dirAngle,float downForce,float endHeight)
    {
        StartHeight = startHeight;
        DirAngle = dirAngle;
        DownForce = downForce;
        EndHeight = endHeight;
        RouteBase.x = StartHeight;
        RouteBase.y = DirAngle;
        RouteBase.z = DownForce;
        RouteBase.w = EndHeight;
    }
    public void InfoSetup(Vector3 pos,Transform target, PlayerController curPlayer)
    {
        Pos = pos;
        Target = target;
        myPlayer = curPlayer;
    }


}

public class RouteMaker : MonoBehaviour
{

    public List<Route> routes ;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        

    }


    public void RouteSetup(Vector3 pos , Route route , Vector4 RouteInfo , Transform target , PlayerController player)
    {
        //route.GetComponent<IRoute>().RouteSetUp(pos);
        route.SetUp(pos, RouteInfo, target, player);
    }
    public void RouteSetup(Route route,RouteInfo rInfo)
    {
        //route.GetComponent<IRoute>().RouteSetUp(pos);
        route.SetUp(rInfo.Pos,rInfo.RouteBase ,rInfo.Target,rInfo.myPlayer);
    }

}
