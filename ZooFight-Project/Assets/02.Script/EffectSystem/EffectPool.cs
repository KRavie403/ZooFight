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
            clone.SetActive(false);  // ������ ������Ʈ�� ��Ȱ��ȭ
            EffectClones.Add(clone.GetComponent<EffectPlayer>());  // Ǯ�� �߰�
        }
        else
        {
            Debug.LogError($"EffectCode: {effectCode}  ã�� �� ����");
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
        effectObject.transform.position = Vector3.zero;  // ��ġ �ʱ�ȭ
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
