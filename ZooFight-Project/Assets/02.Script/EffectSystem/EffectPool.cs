using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{

    public List<EffectPlayer> EffectClones;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    }
    public void CreateClones(EffectCode effectCode)
    {

    }

    public void CloneEffectPlay(EffectCode effectCode,float StartTime)
    {
        
    }


}
