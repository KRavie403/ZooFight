using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemProperty : MonoBehaviour
{

    
    public float Value1; // ��Ÿ�
    public float Value2; // ���۽ð�
    public float Value3; // ���۹���
    public float Value4; // ����ü�ӵ�    
    public float Value5; // ������ or ȸ����

    public ItemSystem.ItemType myItemType;

    public EffectCode effectCode;

    public SoundCode soundCode;

    public List<GameObject> Targets;

}
