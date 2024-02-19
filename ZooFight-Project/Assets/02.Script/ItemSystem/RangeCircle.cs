using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCircle : MonoBehaviour , IRangeEvent    
{
    public List<GameObject> DetectedObject;

    Component IRangeEvent.comp 
    {
        get => this as Component;
    }

    bool IRangeEvent.CheckRange(GameObject range)
    {
        return false;
    }

    GameObject[] IRangeEvent.DetectedObjectSender(GameObject ReceiveObj)
    {

        return DetectedObject.ToArray();
    }
    void IRangeEvent.RouteStart(Vector3 pos)
    {
        throw new System.NotImplementedException();
    }

    public void InsertData(float Range, float Height)
    {
        Distance = Range;

    }

    public float Distance = 1.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





}
