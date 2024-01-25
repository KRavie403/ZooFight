using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCircle : MonoBehaviour , IRangeChecker    
{
    public List<GameObject> DetectedObject;

    Component IRangeChecker.comp 
    {
        get => this as Component;
    }

    bool IRangeChecker.CheckRange(GameObject range)
    {
        return false;
    }

    GameObject[] IRangeChecker.DetectedObjectSender(GameObject ReceiveObj)
    {

        return DetectedObject.ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





}
