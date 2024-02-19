using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RangeArc : MonoBehaviour , IRangeEvent ,IPointerDownHandler ,IDragHandler
{
    public List<GameObject> DetectedObject;

    Vector2 RouteValues;
    public Transform TargetObjs;
    public float objSpeed = 10.0f;

    public Items items;



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
        StartRoute(pos,items.GetPlayer());
        //return RouteValues;
    }

    public void InsertData(float Range, float Height)
    {
        throw new System.NotImplementedException();
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

    bool PosInRangeCheck(Vector3 pos)
    {
        

        return false;
    }

    public void StartRoute(Vector3 RouteValues,PlayerController player)
    {
        StartCoroutine(RouteMaker3(RouteValues, TargetObjs,player));
    }

    public float WholeTime = 2.0f;

    public IEnumerator RouteMaker(Vector3 Value,Transform Target)
    {

        Vector3 startPos = Gamemanager.Inst.currentPlayer.transform.position;
        Plane PlayerPlane = new Plane(Vector3.up, startPos);
        float hitdist = 0.0f;

        float circleR = Mathf.Sqrt(Mathf.Pow(Value.x, 2) + Mathf.Pow(Value.z, 2)) / 2;

        Vector3 center = (startPos + Value) * 0.5f;
        center.y -= 2.0f;

        Quaternion targetRotation = Quaternion.LookRotation(center - startPos);

        Target.rotation = Quaternion.Slerp(Target.rotation, targetRotation, WholeTime * Time.deltaTime);



        //Vector3 Angle = Quaternion.AngleAxis(90, Vector3.up) * -dir;


        Debug.Log(Value);

        float deg = 0;

        while (true)
        {
            deg += Time.deltaTime * objSpeed;

            if(deg < 180)
            {

                yield return null;
            }
            else
                yield break;
        }

    }

    public float duringTime = 0.0f;
    public IEnumerator RouteMaker2(Vector3 Value,Transform Target)
    {
        // 변수 초기화
        duringTime = 0;

        // 변수선언
        float circleR = Mathf.Sqrt(Mathf.Pow(Value.x, 2) + Mathf.Pow(Value.z, 2));

        Vector3 dir = new Vector3(Value.x,0, Value.z);

        Vector3 startPos = Gamemanager.Inst.currentPlayer.transform.position;

        float deg = 0;
        float dist = 0;
        Debug.Log(Value + " , " + circleR);

        while (duringTime < 2.0f)
        {
            duringTime += Time.deltaTime;
            deg = duringTime;
            dist += Time.deltaTime / circleR ;
            //Debug.Log(deg + " , " + dist);
            if(deg < 180)
            {
                var rad = (180 / Mathf.Rad2Deg) * deg;
                Vector3 pos = dir * dist + new Vector3(0, Mathf.Sin(rad) * circleR / 2 ,0) ;

                Target.position = - startPos + pos;

                yield return null;
            }
            else
            {
                yield break;
            }
        }
    }

    public IEnumerator RouteMaker3(Vector3 Value,Transform target,PlayerController player)
    {
        duringTime = 0;
        target.rotation = Quaternion.identity;
        Vector3 startPos = player.transform.position;
        Vector3 center = (startPos + Value) / 2;
        center.y = 0.5f;
        Quaternion baseRot = Quaternion.AngleAxis(-90, startPos - center);
        Vector3 dir = baseRot * center;
        Vector3 lerp = startPos - center;


        LineRenderer line = new LineRenderer();

        int index = 0;

        while (duringTime < WholeTime)
        {
            index++;
            duringTime += Time.deltaTime;

            target.position = startPos + Quaternion.AngleAxis(180 * (duringTime / WholeTime), dir) * center - lerp ;
            

            //line.SetPosition(index, target.position);
            yield return null;
        }
    }



    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        //Vector3 TargetPos = Gamemanager.Inst.currentPlayer.TargetCamera.Cam.ScreenToWorldPoint(eventData.position);

        throw new System.NotImplementedException();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        

        throw new System.NotImplementedException();
    }


}
