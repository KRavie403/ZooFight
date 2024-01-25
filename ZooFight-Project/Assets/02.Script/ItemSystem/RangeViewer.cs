using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0 포물선 1 원형 2 박스 ...
// TypeCount = 전체 갯수확인
public enum RangeType { Parabola = 0 , Circle , Box, TypeCount}


public static class RangeTypeSetting 
{ 
    public static Dictionary<RangeType, GameObject> keys 
        = new Dictionary<RangeType, GameObject>();
}

interface IRangeChecker
{

    bool CheckRange(GameObject range);
    Component comp { get; }

    // 타격범위에 들어와있는 오브젝트 표시용도
    public GameObject[] DetectedObjectSender(GameObject ReceiveObj);
    
}


public class RangeViewer : MonoBehaviour
{

    public GameObject[] RangeUI;


    private void Awake()
    {
        for (int i = 0; i < (int)RangeType.TypeCount; i++)
        {
            if (RangeUI.Length != 0)
                RangeTypeSetting.keys.Add((RangeType)i, RangeUI[i]);
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

    


}
