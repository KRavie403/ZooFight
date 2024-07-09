using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public List<EffectPlayer> EffectClones;     // 복제된 Effect 저장


    // 주어진 'EffectCode'에 해당하는 'EffectPlayer'을 찾아 오브젝트를 반환
    public GameObject GetClones(EffectCode effectCode)
    {
        EffectPlayer players = EffectSetting.keys[effectCode];

        return EffectClones[EffectClones.IndexOf(players)].gameObject;
    }

    // 주어진 'EffectCode'에 해당하는 'EffectPlayer' 객체를 찾아 복제하고 비활성화 상태로 'EffectClones' 리슴트에 추가
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

    // 주어진 'EffectCode'에 해당하는 효과를 시작 시간과 함께 재생
    public void CloneEffectPlay(EffectCode effectCode,float StartTime)
    {
        EffectPlayer player = EffectSetting.keys[effectCode];
        if (player != null)
        {
            player.EffectPlay(0, StartTime, true);
        }
    }

    // 주어진 게임 오브젝트를 비활성화하고 초기 위치로 되돌린 후, 'EffectClones' 리스트에 추가
    public void ReturnToPool(GameObject effectObject)
    {
        effectObject.SetActive(false);
        effectObject.transform.position = Vector3.zero;  // 위치 초기화
        EffectClones.Add(effectObject.GetComponent<EffectPlayer>());
    }

    // 주어진 'EffectCode'에 해당하는 비활성화된 'EffectPlayer' 객체를 풀에서 찾아 활성화하고 반환
    // 없으면 'null'을 반환
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
