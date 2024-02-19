using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectPlayer : MonoBehaviour
{

    public GameObject myObj;

    public List<ParticleSystem> myEffect;

    public List<Transform> triggerTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            EffectPlay(0);
            EffectPlay(1);
        }
    }

    // ¿Œµ¶Ω∫ Ω««‡
    public void EffectPlay(int index)
    {
        myEffect[index].Play();
    }
    // ¿Œµ¶Ω∫ 
    public void EffectPlay(int index,float playTime)
    {
        myEffect[index].time = playTime;
        myEffect[index].Play();
    }

    public void EffectPlay(int index,float playTime,bool isLoop)
    {
        var main = myEffect[index].main;
        main.loop = isLoop;
        myEffect[index].time = playTime; 
        myEffect[index].Play();
    }
    
    public void Test1(int index)
    {
        //myEffect[index].SetTriggerParticles(ParticleSystemTriggerEventType.Inside, triggerTarget);
    }

    private void OnParticleTrigger()
    {
        foreach (var v in triggerTarget)
        {
            Debug.Log(v.name);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (triggerTarget.Contains(other.transform)) return;
        triggerTarget.Add(other.transform);
        Debug.Log(other.name);
    }

    public void EffectEnd(int index,UnityAction e = null)
    {
        if (myEffect[index].isStopped)
        {
            e?.Invoke();
        }
    }

}
