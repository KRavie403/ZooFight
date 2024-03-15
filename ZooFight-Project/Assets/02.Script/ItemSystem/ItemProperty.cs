using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemProperty : MonoBehaviour
{

    
    public float Value1; // 사거리
    public float Value2; // 동작시간
    public float Value3; // 동작범위
    public float Value4; // 투사체속도    
    public float Value5; // 데미지 or 회복량

    public ItemSystem.ItemType myItemType;

    public EffectCode effectCode;

    public SoundCode soundCode;

    public List<GameObject> Targets;

}
