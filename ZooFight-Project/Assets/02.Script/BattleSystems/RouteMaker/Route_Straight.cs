using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Route_Straight : Route , IRoute
{

    RouteTypes IRoute.Type => myRouteType;

    Component IRoute.Comp => this as Component;

    public Route_Straight() : base() 
    {

    }

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

    void IRoute.RouteStart(Transform Target, Vector3 Dir, float Speed, float Dist, UnityAction e = null)
    {
        throw new System.NotImplementedException();
    }

    protected override void RouteStart(Transform Target, Vector3 Dir, float Speed, float Dist, UnityAction e = null)
    {
        base.RouteStart(Target, Dir, Speed, Dist, e);

    }

    public IEnumerator RouteMake(Transform target, Vector3 Dir, float Speed, float Dist, UnityAction e = null)
    {
        float duringTime = 0;

        Vector3 dir = Vector3.Normalize(Dir);
        Vector3 StartPos = target.position;
        Vector3 pos = Vector3.zero;
        float dist = Vector3.Magnitude(new Vector3(Dir.x, 0, Dir.z));

        while (duringTime < dist/Speed)
        {
            duringTime += Time.deltaTime;

            pos = StartPos + dir;
            transform.position = pos;

            yield return null;  
        }

    }




}
