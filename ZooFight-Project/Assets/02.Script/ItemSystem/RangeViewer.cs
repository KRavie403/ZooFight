using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0 ������ 1 ���� 2 �ڽ� ...
// TypeCount = ��ü ����Ȯ��
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

    // Ÿ�ݹ����� �����ִ� ������Ʈ ǥ�ÿ뵵
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
