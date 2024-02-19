using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public enum EffectCode
{
    Item1=0, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11,
    CharacterWalk, CharacterRun , CharacterJump , CharacterAttack , CharacterDamaged,
    ButtonClick , 
    MinimalRazer, Boom,
    CodeCount
}

public static class EffectSetting
{
    public static Dictionary<EffectCode,EffectPlayer> keys = new();
}

public class Effectmanager : Singletone<Effectmanager>
{

    public List<EffectPlayer> effectPlayers;
    public List<GameObject> effectObj;

    private void Awake()
    {
        for (int i = 0; i < effectObj.Count; i++)
        {
            if (effectObj[i] != null)
                effectPlayers.Add(effectObj[i].GetComponent<EffectPlayer>());
        }

        for (int i = 0; i < (int)EffectCode.CodeCount; i++)
        {
            if (i < effectObj.Count)
            {
                if (effectPlayers[i] != null)
                {
                    EffectSetting.keys.Add((EffectCode)i, effectPlayers[i]); 
                }
            }
        }
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
