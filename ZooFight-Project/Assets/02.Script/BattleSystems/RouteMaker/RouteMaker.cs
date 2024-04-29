using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �̵� ��θ� ������ִ� Ŭ����
/// ������ , ���� , ���� ��� �߰�����
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
    public void RouteStart(Transform Target, Vector3 Dir, float Speed, float Dist, UnityAction e = null);


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

    public Dictionary<RouteTypes,Route> RouteKeys = new Dictionary<RouteTypes,Route>();
    public List<Route> routes ;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

        RouteKeys.Add(RouteTypes.Arc, routes[0]);


    }


    // Update is called once per frame
    void Update()
    {
        

    }






}
