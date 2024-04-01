using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


interface IItems
{
    public void UseItems();
}

public class Items : ItemProperty , IItems , IEffect
{
    protected PlayerController myPlayer = null;

    public IEnumerator ItemAction;

    public int InputCount = 0;

    public event Action ItemReady = delegate { };

    protected Vector3 dir = Vector3.zero;

    [SerializeField]
    EffectCode ItemEffect;
    EffectCode IEffect.EffectCode => ItemEffect;

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


    public virtual void Standby(Items item,ItemSystem.ActiveType itemType)
    {
        switch (itemType)
        {
            case ItemSystem.ActiveType.PointSelect:
                ItemReady = () => ItemUse(myPlayer);
                //CallRangeUI(myItemType);
                break;
            case ItemSystem.ActiveType.SelfActive:
                break;
            case ItemSystem.ActiveType.EnemyActive:
                break;
            case ItemSystem.ActiveType.BlockActive:
                break;
            
            // 미동작 부분
            case ItemSystem.ActiveType.TypeCount:
            default:
                break;
        }
    }

    public virtual void ItemUse(PlayerController player)
    {
        if(ItemAction != null)
        {
            StartCoroutine(ItemAction);
        }

    }


    public virtual void CallRangeUI(ItemSystem.ActiveType ActiveType)
    {
        switch (ActiveType)
        {
            case ItemSystem.ActiveType.PointSelect:
                break;
            case ItemSystem.ActiveType.SelfActive:
                break;
            case ItemSystem.ActiveType.EnemyActive:
                break;
            case ItemSystem.ActiveType.BlockActive:
                break;
            case ItemSystem.ActiveType.TypeCount:
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

    EffectCode IEffect.GetEffectCode()
    {
        return ItemEffect;
    }

    public virtual void UseItems()
    {
        throw new NotImplementedException();
    }
}

