using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ItemSystem : Singleton<ItemSystem> 
{

    public RangeViewer RangeViewer;

    public GameObject ViewerObj;


    public enum ItemType
    {
        Immediate = 0,      // ��� �ߵ��� ������
        Projectile,         // ���� ������ ������
        Delayed,            // ���� �ð��� ������


        TypeCount           // ������ ������ ����
    }

    public enum ActiveType
    {

        PointSelect = 0, // ��� ������ Ŭ������ �����ϴ� ������
        SelfActive,      // �ڽſ��� ��� ����Ǵ� ������
        EnemyActive,     // ���濡�� ��� �ۿ�Ǵ� ������
        BlockActive,     // ���κ��� ����Ǵ� ������

        TypeCount        // ���� ������ ����
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

