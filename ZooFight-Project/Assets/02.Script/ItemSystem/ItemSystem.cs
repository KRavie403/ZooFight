using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    CurseScroll,
    SpiderBomb,
    InkBomb,


    // 300 ~ 399 ���������
    ToyHammer = 300,


}

// �����̻� ������ �������� & ����� �������� ��������
public enum StatusCode
{
    // 0 = �⺻���� 1 ~ 9 �̵����ذ迭
    Normal = 0,
    Slow,       // �̵��ӵ� ����
    //Minimal,    // ������ ���� (�̼�,����,������ ����)
    //Cripple,    // ���ݼӵ� ����
    // 10 ~ 19  �ϱ� ���� �̻�
    Blind = 10, // �þ� ����
    Bind,       // �ӹ�
    //Weak,       // �� ��� �Ұ�
    //Silence,    // ������ ��� �Ұ�
    // 20 ~ 29  �߱� ���� �̻�
    Stun,       // ���� 


    // 30 ~ 39  ��� ���� �̻�
    AirBone
}

public class ItemSystem : Singleton<ItemSystem> 
{

    public RangeViewer RangeViewer;

    public GameObject ViewerObj;


    public Dictionary<ItemCode, Items> ItemKeys = new Dictionary<ItemCode, Items>();
    // ��������
    public List<GameObject> ItemList = new List<GameObject>();


    // ������ �������� ����
    public List<Items> ItemBase = new List<Items>();


    public UnityEvent<Items> ItemOrignin;

    Vector3 tmpPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        
        //for(int i = 0; i < ItemList.Count; i++)
        //{
        //    GameObject obj = Instantiate(ItemList[i],transform);
        //    Items item = obj.GetComponent<Items>();
        //    ItemKeys.Add(item.myCode, item);
        //}
        for (int i = 0; i < ItemBase.Count; i++)
        {
            ItemKeys.Add(ItemBase[i].myCode,ItemBase[i]);
        }
        ItemKeys[ItemCode.BananaTrap].Value1 = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem(Items item,PlayerController players)
    {
        //item.ItemUse(players);
        players.curItems.ItemUse();
    }

    public void AddRequest(PlayerController player)
    {
    }
    public void ResetRequest()
    {
        ItemOrignin.RemoveAllListeners();
    }

    public void CallItem()
    {
        ItemOrignin.Invoke(ItemBase[Random.Range(0, ItemList.Count)]);
    }

    public Items RandomItemSelect()
    {
        // �׽�Ʈ�� �������
        return ItemBase[0];// ��������
        return ItemBase[Random.Range(0, ItemList.Count)];

    }

    // ���� �������� ������Ʈ Ǯ���� �����ͼ� �Է¹��� ĳ���Ϳ��� �־��ֱ�
    public Items GiveItem(PlayerController player,Items item)
    {
        if (player.curItems != null) return null;
        //Items item = RandomItemSelect();

        return ObjectPoolingManager.instance.GetObject<Items>(item.name, Vector3.zero, Quaternion.identity, player.ItemPoint,false);
    }

    public Items GetItemOrigin(Items item)
    {

        return null;
    }

}

