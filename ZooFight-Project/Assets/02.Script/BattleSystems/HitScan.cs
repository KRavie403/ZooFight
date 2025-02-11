using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using UnityEngine;


public enum ScanType
{
    Sphere, //구체형
    Circle, //원형

    Square, //정사각형
    Cube, //정육면체
    Type
}

public enum ScanTarget
{
    Player,
    Block,
    Item,

    Test1,
    TypeCount
}

interface IHitScanTarget
{
    Component myComp
    {
        get;
    }

    public virtual void Hit()
    {

    }

    int testcode { get; }
}

interface IHitScanner
{
    Component myComp
    {
        get;
    }

    Component[] myTargets
    {
        get; 
    }

    public virtual void AddTarget(List<Component> target)
    {
       
    }

    public virtual void Hit()
    {

    }

}

/// <summary>
/// 특정 오브젝트의 탐색을 하는 스크립트
/// </summary>
public class HitScan : Singleton<HitScan> 
{

    Component[] comps;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("InputSpace");

            comps = HitScans(this.gameObject, 2.0f, ScanTarget.Test1,ScanType.Sphere);

            foreach (var comp in comps)
            {
                Debug.Log(comp.GetComponent<IHitScanTarget>().testcode);
                comp.GetComponent<IHitScanTarget>().Hit();
            }

        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Component[] HitScans(GameObject obj, float range, ScanTarget targets, ScanType type)
    {

        switch (type)
        {
            case ScanType.Sphere:
                return ScanSphere(obj, range, GetTargets(targets));
            case ScanType.Circle:
                break;
            case ScanType.Square:
                break;
            case ScanType.Cube:
                //return ScanBox(obj, range, Targets);                
            case ScanType.Type:
                break;
            default:
                break;
        }


        return null;
    }


    Component[] ScanSphere(GameObject obj, float range, Type Targets)
    {

        List<Collider> objs = Physics.OverlapSphere(obj.transform.position, range).ToList();
        List<Component> Target= new List<Component>();

        foreach (Collider collider in objs)
        {


            if(collider.GetComponent<IHitScanTarget>() != null)
            {
                if(collider.GetComponent<IHitScanTarget>().myComp.GetType() == Targets)
                {
                    Target.Add(collider.gameObject.GetComponent<IHitScanTarget>().myComp);
                }
            }

        }
        obj.GetComponent<IHitScanner>().AddTarget(Target);

        return Target.ToArray();
    }

    Component[] ScanBox(GameObject obj, float range, Component Targets)
    {
        List<Collider> objs = Physics.OverlapBox(obj.transform.position,new Vector3(range/2,range/2,range/2)).ToList();
        List<Component> Target = new List<Component>();

        foreach (Collider collider in objs)
        {
            if (collider.GetComponent<IHitScanTarget>() != null)
            {
                if (collider.GetComponent<IHitScanTarget>().myComp == Targets)
                {
                    Target.Add(collider.gameObject.GetComponent<IHitScanTarget>().myComp);
                }
            }

        }
        obj.GetComponent<IHitScanner>().AddTarget(Target);


        return Target.ToArray();
    }

    public Component Test1<T>(T componets)
    {
        return null;

    }

    Type GetTargets(ScanTarget target)
    {
        switch (target)
        {
            case ScanTarget.Player:
                return typeof(PlayerController);
            case ScanTarget.Block:
                return typeof(BlockObject);
            case ScanTarget.Item:
                return typeof(Items);
            case ScanTarget.Test1:
                return typeof(TypeTest);
            case ScanTarget.TypeCount:
            default:
                break;
        }

        return null;
    }


    //[SecuritySafeCritical]
    //public unsafe T abce<T>(ScanTarget scanTarget)
    //{


    //    return null;
    //}

}
