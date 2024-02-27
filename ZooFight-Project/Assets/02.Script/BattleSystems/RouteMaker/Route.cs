using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Route : MonoBehaviour
{
    


    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
    protected virtual void LateUpdate()
    {

    }
    protected virtual void FixedUpdate()
    {

    }

    protected virtual void RouteSetUp(Vector3 pos, Vector4 RouteInfo,Transform target,PlayerController player)
    {
        Debug.Log("BB");
    }

    public void SetUp(Vector3 pos,Vector4 RouteInfo,Transform target,PlayerController player)
    {
        RouteSetUp(pos,RouteInfo,target,player);
    }


}
