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

        StartCoroutine(RouteMaker(RouteValues, TargetObjs,player));
    }

    public float DropSpeed = 2.0f;

    //public float duringTime = 0.0f;
    public float startHeight = 1.5f;
    public float endHeight = 1.0f;
    public float ShootAngle = 45.0f;
    public float DownForce = 10.0f;
    public IEnumerator RouteMaker(Vector3 Value,Transform Target,PlayerController player)
    {
        // 변수 초기화
        float duringTime = 0;

        //DrawRoute(Value,Target,player);

        // 기본벡터 정의
        Vector3 startPos = player.transform.position;
        Vector3 dir = Value - startPos;
        startPos.y = dir.y = startHeight;

        Vector3 dirUp = Vector3.up;

        float dist = dir.magnitude;
        float VelX = dist / (Mathf.Cos(Mathf.Deg2Rad * ShootAngle) * DropSpeed);
        float VelY = dist / Mathf.Sin(Mathf.Deg2Rad * ShootAngle);

        float downForce = (VelY * 2) / DropSpeed; 

        float distX = 0.0f;
        float distY = 0.0f;

        Vector3 dirN = Vector3.Normalize(dir);

        while (Target.position.y < endHeight)
        {
            duringTime += Time.deltaTime;

            VelY -= Time.deltaTime * downForce;

            distX += VelX * Time.deltaTime;
            distY += VelY * Time.deltaTime;
            Target.position = startPos + dirN * distX + dirUp * distY;

            yield return null;
        }
    }

    public void DrawRoute(Vector3 Value, Transform Target, PlayerController player)
    {
        // 기본벡터 정의
        Vector3 startPos = player.transform.position;
        Vector3 dir = Value - startPos;
        startPos.y = dir.y = startHeight;

        Vector3 dirUp = Vector3.up;

        float dist = dir.magnitude;
        float VelX = dist / (Mathf.Cos(Mathf.Deg2Rad * ShootAngle) * DropSpeed);
        float VelY = dist / Mathf.Sin(Mathf.Deg2Rad * ShootAngle);

        float downForce = (VelY * 2) / DropSpeed;

        float distX = 0.0f;
        float distY = 0.0f;

        Vector3 dirN = Vector3.Normalize(dir);

        LineRenderer lines = new LineRenderer();
        //lines.

        for (int i = 0; distX < dist; i++)
        {
            lines.positionCount = i+1;
            VelY -= (1 / Gamemanager.Inst.PollingRate) * downForce;

            distX += VelX * (1 / Gamemanager.Inst.PollingRate);
            distY += VelY * (1 / Gamemanager.Inst.PollingRate);
            lines.SetPosition(i, startPos + dirN * distX + dirUp * distY);
        }

    }



    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        
        throw new System.NotImplementedException();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
