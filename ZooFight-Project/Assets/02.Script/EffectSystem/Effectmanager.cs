using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ����Ʈ �ڵ� ����
/// 0 ~ 99 ĳ���� ���� ����Ʈ
/// 100 ~ 200 ������ ���� ����Ʈ
/// 200 ~ 300 UI ���� ����Ʈ
/// 
/// </summary>
public enum EffectCode
{
    CharacterWalk = 0, CharacterRun = 1, CharacterJump = 2 , CharacterAttack = 3 , CharacterDamaged = 4,
    GuardDrink = 100, StaminaDrink = 101 , PowerDrink = 102,
    BananaTrap = 103,
    Bomb = 104, SpiderBomb = 105, InkBomb = 106,
    ToyHammer = 107, WhippingMachine = 108, MinimalRazer = 109,
    CurseScroll = 110, BlockChangeScroll = 111,
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

public class Effectmanager : Singleton<Effectmanager>
{
    public EffectPool effectPool;

    public List<EffectPlayer> effectPlayers;
    public List<GameObject> effectObj;
    public List<GameObject> activeEffectObjects;    // ����� ������Ʈ

    private void Awake()
    {
        effectPlayers = new List<EffectPlayer>();  // �ʱ�ȭ
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
        activeEffectObjects.Add(obj);   // ������ ��ü ����Ʈ�� �߰�
    }

    // Ư�� ��Ȳ���� ������Ʈ ����
    public void RemoveEffectObj(GameObject effectObject)
    {
        if (activeEffectObjects.Contains(effectObject))
        {
            activeEffectObjects.Remove(effectObject);
            Destroy(effectObject);      // ���� ������Ʈ ����
        }
    }

    // ������� �ʴ� ����Ʈ�� �ڵ����� ��Ȱ��ȭ �� �ʿ��� �� ��Ȱ��ȭ�ϴ� ���
    public void ManageEffectPool()
    {
        foreach (var effect in activeEffectObjects)
        {
            if (!effect.activeInHierarchy)      // ��Ȱ��ȭ�� ����Ʈ ã��
            {
                effectPool.ReturnToPool(effect);    // Ǯ�� ��ȯ
            }
        }
    }
}
