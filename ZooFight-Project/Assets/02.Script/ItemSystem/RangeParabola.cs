using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeParabola : MonoBehaviour , IRangeChecker
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

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // 사거리내부에 들어온 대상에 대한 이펙트 출력 예정
    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
