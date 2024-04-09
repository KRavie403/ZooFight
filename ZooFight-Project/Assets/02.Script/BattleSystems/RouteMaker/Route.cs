using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Route : MonoBehaviour
{
    
    public Route()
    {

    }



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

    //
    protected virtual void RouteSetUp(Transform target,PlayerController player,Vector3 angle)
    {
        Debug.Log("BB");
    }

    protected virtual void RouteStart()
    {

    }

    public void SetUp(Transform target, PlayerController player, Vector3 angle)
    {
        RouteSetUp(target,player,angle);
    }

    

}
