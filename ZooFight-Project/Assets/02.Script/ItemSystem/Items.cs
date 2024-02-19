using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


interface IItems
{

}

public class Items : ItemProperty
{
    PlayerController myPlayer = null;

    public IEnumerator ItemAction;

    public int InputCount = 0;

    public event Action ItemReady = delegate { };

    protected Vector3 dir = Vector3.zero;

    protected virtual void Awake()
    {
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myPlayer = Gamemanager.Inst.currentPlayer;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    
    public virtual void Initate(List<float> Values,PlayerController player)
    {
        if (Values == null) return;
        if (Values.Count < 5) return;
        myPlayer = player;

    }


    public virtual void Standby(ItemSystem.ItemType itemType)
    {
        switch (itemType)
        {
            case ItemSystem.ItemType.PointSelect:
                ItemReady = () => ItemUse(myPlayer);
                CallRangeUI(myItemType);
                break;
            case ItemSystem.ItemType.Channeling:
                break;
            case ItemSystem.ItemType.SelfActive:
                break;
            case ItemSystem.ItemType.EnemyActive:
                break;
            case ItemSystem.ItemType.BlockActive:
                break;
            
            // 미동작 부분
            case ItemSystem.ItemType.TypeCount:
            default:
                break;
        }
    }

    public virtual void ItemUse(PlayerController player)
    {
        
        StartCoroutine(ItemAction);

    }


    public virtual void CallRangeUI(ItemSystem.ItemType itemType)
    {
        switch (itemType)
        {
            case ItemSystem.ItemType.PointSelect:
                break;
            case ItemSystem.ItemType.Channeling:
                break;
            case ItemSystem.ItemType.SelfActive:
                break;
            case ItemSystem.ItemType.EnemyActive:
                break;
            case ItemSystem.ItemType.BlockActive:
                break;
            case ItemSystem.ItemType.TypeCount:
                break;
            default:
                break;
        }
    }


    private void OnDestroy()
    {
        if (myPlayer.IsDestroyed())
        {
            return;
        }
        if(myPlayer != null)
        {
            myPlayer.curItems = null;
        }
    }

    public PlayerController GetPlayer()
    {
        return myPlayer;
    }

}

