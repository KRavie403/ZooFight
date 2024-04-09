using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemCode
{
    // 0   ~  99 ����߻���
    Bomb = 0,
    BananaTrap,

    // 100 ~ 199 ��ü������
    GuardDrink = 100,
    PowerDrink,


    // 200 ~ 299 �����ۿ���
    BlockChangeScroll = 200,
    SpiderBomb,
    InkBomb,


    // 300 ~ 399 ���������
    ToyHammer = 300,


}

// �����̻� ������ �������� & ����� �������� ��������
public enum StatusCode
{
    // 0 = �⺻����
    Normal = 0,
    // 10 ~ 19 ��
    Blind,
    Bind,

    // 20 ~ 29 ��

    Stun

    // 30 ~ 39 ��
}
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

