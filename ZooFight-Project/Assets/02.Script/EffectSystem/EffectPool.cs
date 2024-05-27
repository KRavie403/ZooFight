using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public List<EffectPlayer> EffectClones;


    public GameObject GetClones(EffectCode effectCode)
    {
        EffectPlayer players = EffectSetting.keys[effectCode];

        return EffectClones[EffectClones.IndexOf(players)].gameObject;
    }

    //public EffectPlayer GetClones(EffectCode effectCode)
    //{
    //    EffectPlayer players = EffectSetting.keys[effectCode];

    //    return EffectClones[EffectClones.IndexOf(players)];
    //}

    public void CreateClones(GameObject gameObject)
    {
        GameObject clone = Instantiate(gameObject);
        EffectClones.Add(clone.GetComponent<EffectPlayer>());
    }
    public void CreateClones(EffectCode effectCode)
    {
        if (EffectSetting.keys.TryGetValue(effectCode, out EffectPlayer player))
        {
            GameObject clone = Instantiate(player.gameObject);
            clone.SetActive(false);  // 복제된 오브젝트를 비활성화
            EffectClones.Add(clone.GetComponent<EffectPlayer>());  // 풀에 추가
        }
        else
        {
            Debug.LogError($"EffectCode: {effectCode}  찾을 수 없음");
        }
    }

    public void CloneEffectPlay(EffectCode effectCode,float StartTime)
    {
        EffectPlayer player = EffectSetting.keys[effectCode];
        if (player != null)
        {
            player.EffectPlay(0, StartTime, player.transform, true);
        }
    }

    public void ReturnToPool(GameObject effectObject)
    {
        effectObject.SetActive(false);
        effectObject.transform.position = Vector3.zero;  // 위치 초기화
        EffectClones.Add(effectObject.GetComponent<EffectPlayer>());
    }

    public GameObject GetFromPool(EffectCode effectCode)
    {
        foreach (var player in EffectClones)
        {
            if (((IEffect)player).EffectCode == effectCode && !player.gameObject.activeInHierarchy)
            {
                player.gameObject.SetActive(true);
                return player.gameObject;
            }
        }
        return null;
    }
}
