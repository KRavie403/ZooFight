using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �����۸� : ???
/// Value 1  
/// Value 2 
/// Value 3
/// Value 4
/// Value 5
/// </summary>

public class ItemProperty : MonoBehaviour
{

    // �⺻ ����
    public float Value1; // ������ or ȸ����
    public float Value2; // ���۽ð�
    public float Value3; // ���۹���
    public float Value4; // ����ü�ӵ�    
    public float Value5; // ��Ÿ�

    public ItemCode myCode;


    public EffectCode effectCode;

    public SoundCode soundCode;

    public GameObject myPrefab;


    protected List<GameObject> _targets = new();
    public List<GameObject> Targets = new();

    public bool isTarget = false;





}
