using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


interface IItems
{
    public void ItemUse();
}


public class Items : ItemProperty , IItems , IEffect
{
    public Items(PlayerController curPlayer)
    {
        this.myPlayer = curPlayer;
        Debug.Log($"{this} Load");
    }

    [SerializeField]
    protected PlayerController myPlayer = null;

    public IEnumerator ItemAction;

    public int InputCount = 0;
    protected bool isItemUse = false;
    protected bool isItemUseEnd = false;
    public event Action ItemReady = delegate { };

    [SerializeField]
    protected Vector3 dir = Vector3.zero;

    [SerializeField]
    EffectCode ItemEffect;
    EffectCode IEffect.EffectCode => ItemEffect;

    protected virtual void Awake()
    {
        //if(transform.parent == ObjectPoolingManager.instance.gameObject)
        //{

        //}
        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //curPlayer = Gamemanager.Inst.currentPlayer;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnEnable()
    {
        StopAllCoroutines();
        if (myPlayer != null)
        {
            ItemAction = ItemActions();
            if(ItemAction != null)
            {
                StartCoroutine(ItemActions());
            }

        }
    }

    protected virtual void OnDisable()
    {
        if(ItemAction != null)
        {
            StopAllCoroutines();
        }
    }

    

    // ���������� �ʿ��Ҷ�
    public virtual void Initate(List<float> Values,PlayerController player)
    {
        if (Values == null) return;
        if (Values.Count > 5) return;

        Value1 = Values[0];
        Value2 = Values[1];
        Value3 = Values[2];
        Value4 = Values[3];
        Value5 = Values[4];
        
        myPlayer = player;
        foreach(var value in Values)
        {
            Debug.Log(value);
        }
        transform.localPosition = Vector3.zero;
        //Debug.Log(Values);
    }


    // ����� ����
    public virtual void ItemUse()
    {
        // ������Ʈ�� �����մٸ�
        if (!gameObject.activeSelf)
        {

        }

        // �ܿ� �۾��� �ִ��� Ȯ�� �� ����
        if (ItemAction != null)
        {
            StopCoroutine(ItemAction);
            ItemAction = ItemActions();
        }
        isItemUse = true;
    }

    public virtual void ItemHitAction()
    {
        isTarget = true;
    }

    // �������� ������Ʈ Ǯ�� �ݳ��Ҷ�
    public void ReturnItem()
    {
        //myPlayer = null;
        //GetComponent<myHitScanner>()?.SetMyTeam(myHitScanner.Team.NotSetting);
        ItemEnd();
        ObjectPoolingManager.instance.ReturnObject(gameObject);
    }

    // �������� ������ ����ɶ�
    public virtual void ItemEnd()
    {
        isItemUseEnd = true;
        StopCoroutine(ItemAction);
        myPlayer.ItemUseEnd();
        myPlayer.curItems = null;
        myPlayer = null;
        ItemAction = null;
        StopAllCoroutines();
        dir = Vector3.zero;
        GetComponent<HitScanner>()?.Reset();
    }

    protected virtual IEnumerator ItemActions()
    {

        // ��� ������ ��������
        while (!isItemUse)
        {
            // � �������̵� �������� ĳ������ ����
            SetDir(myPlayer.transform.forward);


            yield return null;
        }
        // ��ӹ޴� �������� ����� ���� �ۼ�
        // 
    }

    public List<float> GetValues()
    {
        List<float> Values = new List<float>
        {
            Value1,
            Value2,
            Value3,
            Value4,
            Value5
        };
        return Values;
    }

    protected void OnDestroy()
    {
        if (myPlayer != null) 
        {
            myPlayer.curItems = null;
        }
    }


    public PlayerController GetPlayer()
    {
        return myPlayer;
    }

    public void SetDir(Vector3 Dir)
    {
        dir = Dir;
    }



}

