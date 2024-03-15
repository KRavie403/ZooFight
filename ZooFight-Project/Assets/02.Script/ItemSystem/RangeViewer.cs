using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0 포물선 1 원형 2 박스 ...
// TypeCount = 전체 갯수확인
public enum RangeType { Arc = 0 , Circle , Box, TypeCount}


public static class RangeTypeSetting 
{ 
    public static Dictionary<RangeType, GameObject> keys 
        = new Dictionary<RangeType, GameObject>();
}

interface IRangeEvent
{

    bool CheckRange(GameObject range);
    Component comp { get; }

    public void RouteStart(Vector3 pos);
    // 타격범위에 들어와있는 오브젝트 표시용도
    public GameObject[] DetectedObjectSender(GameObject ReceiveObj);

    public void InsertData(float Value1, float Value2);
}


public class RangeViewer : MonoBehaviour 
{

    public GameObject[] RangeUI;

    public float Value1, Value2;

    public Component curRange;

    public bool isRangeShow = false;

    private void Awake()
    {
        for (int i = 0; i < (int)RangeType.TypeCount; i++)
        {
            if (RangeUI.Length != 0)
                RangeTypeSetting.keys.Add((RangeType)i, RangeUI[i]);
            if (i == RangeUI.Length)
                break;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateRangeUI(GameObject range)
    {
        range.GetComponent<IRangeEvent>().InsertData(Value1, Value2);
    }

    public GameObject CallRangeUI(RangeType rangeType)
    {
        return RangeTypeSetting.keys[rangeType];
    }



}
