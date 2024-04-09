using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemCode
{
    // 0   ~  99 전방발사형
    Bomb = 0,
    BananaTrap,

    // 100 ~ 199 자체버프형
    GuardDrink = 100,
    PowerDrink,


    // 200 ~ 299 적대작용형
    BlockChangeScroll = 200,
    SpiderBomb,
    InkBomb,


    // 300 ~ 399 착용장비형
    ToyHammer = 300,


}

// 상태이상 강도가 높을수록 & 등급이 높을수록 높은숫자
public enum StatusCode
{
    // 0 = 기본상태
    Normal = 0,
    // 10 ~ 19 하
    Blind,
    Bind,

    // 20 ~ 29 중

    Stun

    // 30 ~ 39 상
}
public class ItemSystem : Singleton<ItemSystem> 
{

    public RangeViewer RangeViewer;

    public GameObject ViewerObj;


    public enum ItemType
    {
        Immediate = 0,      // 즉시 발동형 아이템
        Projectile,         // 지점 지정형 아이템
        Delayed,            // 지연 시간형 아이템


        TypeCount           // 아이템 종류의 갯수
    }

    public enum ActiveType
    {

        PointSelect = 0, // 사용 지점을 클릭으로 설정하는 아이템
        SelfActive,      // 자신에게 즉시 적용되는 아이템
        EnemyActive,     // 상대방에게 즉시 작용되는 아이템
        BlockActive,     // 메인블럭에 적용되는 아이템

        TypeCount        // 동작 종류의 갯수
    }



    Vector3 tmpPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem(Items item,PlayerController players)
    {
        item.ItemUse(players);
    }


}

