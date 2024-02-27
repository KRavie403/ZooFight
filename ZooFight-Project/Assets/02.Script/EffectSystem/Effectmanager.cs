using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 이펙트 코드 기준
/// 0 ~ 99 캐릭터 관련 이펙트
/// 100 ~ 200 아이템 관련 이펙트
/// 200 ~ 300 UI 관련 이펙트
/// 
/// </summary>
public enum EffectCode
{
    CharacterWalk = 0, CharacterRun , CharacterJump , CharacterAttack , CharacterDamaged,
    ABCD,
    BananaTrap = 100,
    GuardDrink, StaminaDrink, PowerDrink,
    Bomb, SpiderBomb, InkBomb,
    ToyHammer, WhippingMachine, MinimalRazer,
    CurseScroll, BlockChangeScroll,
    Item11,
    ButtonClick = 200,
    CodeCount
}

public static class EffectSetting
{
    public static Dictionary<EffectCode,EffectPlayer> keys = new();
}

interface IEffect
{
    public EffectCode EffectCode { get; }
    public EffectCode GetEffectCode();
}

public class Effectmanager : Singletone<Effectmanager>
{
    public EffectPool effectPool;

    public List<EffectPlayer> effectPlayers;
    public List<GameObject> effectObj;

    private void Awake()
    {
        for (int i = 0; i < effectObj.Count; i++)
        {
            if (effectObj[i] != null)
            {
                effectPlayers.Add(effectObj[i].GetComponent<EffectPlayer>());
                EffectSetting.keys.Add(effectPlayers[i].GetComponent<IEffect>().GetEffectCode(), effectPlayers[i]);
                Debug.Log(effectPlayers[i].GetComponent<IEffect>().GetEffectCode());
            }
        }

        effectPool = GetComponent<EffectPool>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetEffectObj(EffectPlayer player)
    {
        if(!effectPlayers.Contains(player))
        {
            return null;
        }
        return effectPlayers[effectPlayers.IndexOf(player)].gameObject;
    }

    public void CreateEffectObj(EffectPlayer player,Transform target)
    {
        if (effectPlayers.Contains(player))
        {
            return;
        }
        GameObject obj = Instantiate(player.myObj,target);
    }




}
