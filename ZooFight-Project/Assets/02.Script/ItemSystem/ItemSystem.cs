using System.Collections;
using System.Collections.Generic;
using UnityEngine;




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

