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
    CharacterWalk = 0, CharacterRun, CharacterJump, CharacterAttack, CharacterDamaged,
    GuardDrink = 100, StaminaDrink, PowerDrink, 
    BananaTrap,
    Bomb, SpiderBomb, InkBomb,
    ToyHammer, WhippingMachine, MinimalRazer,
    CurseScroll, BlockChangeScroll,
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
    public List<GameObject> activeEffectObjects;    // 실행된 오브젝트

    private void Awake()
    {
        effectPlayers = new List<EffectPlayer>();  // 초기화
        for (int i = 0; i < effectObj.Count; i++)
        {
            if (effectObj[i] != null)
            {
                //effectPlayers.Add(effectObj[i].GetComponent<EffectPlayer>());
                EffectPlayer effectPlayer = effectObj[i].GetComponent<EffectPlayer>();
                effectPlayers.Add(effectPlayer);
                EffectSetting.keys.Add(effectPlayers[i].GetComponent<IEffect>().EffectCode, effectPlayers[i]);
                Debug.Log(effectPlayers[i].GetComponent<IEffect>().EffectCode);
            }
            //if (effectObj[i] != null)
            //{
            //    effectPlayers.Add(effectObj[i].GetComponent<EffectPlayer>());
            //    EffectSetting.keys.Add(effectPlayers[i].GetComponent<IEffect>().GetEffectCode(), effectPlayers[i]);
            //    Debug.Log(effectPlayers[i].GetComponent<IEffect>().GetEffectCode());
            //}
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
        activeEffectObjects.Add(obj);   // 생성된 객체 리스트에 추가
    }

    // 특정 상황에서 오브젝트 제거
    public void RemoveEffectObj(GameObject effectObject)
    {
        if (activeEffectObjects.Contains(effectObject))
        {
            activeEffectObjects.Remove(effectObject);
            Destroy(effectObject);      // 게임 오브젝트 제거
        }
    }

    // 사용하지 않는 이펙트를 자동으로 비활성화 및 필요할 때 재활성화하는 기능
    public void ManageEffectPool()
    {
        for (int i = activeEffectObjects.Count - 1; i >= 0; i--)
        {
            var effect = activeEffectObjects[i];
            if (!effect.activeInHierarchy)      // 비활성화된 이펙트 찾기
            {
                effectPool.ReturnToPool(effect);    // 풀로 반환
                activeEffectObjects.RemoveAt(i);   // 리스트에서 제거
            }
        }
    }
}
