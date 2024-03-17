using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 이펙트의 실행 , 시작 시간 , 루프 , 출력방향 , 출력위치 를 결정하는 클래스 
/// </summary>
public class EffectPlayer : MonoBehaviour , IEffect
{

    [SerializeField]
    EffectCode myEffectCode;

    public GameObject myObj;

    public List<ParticleSystem> myEffect;

    public List<Transform> triggerTarget;

    EffectCode IEffect.EffectCode => myEffectCode;

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

    #region 이펙트 실행
    // 인덱스 실행
    public void EffectPlay(int index)
    {
        myEffect[index].Play();
    }
    // 인덱스 실행 + 시작지점 지정
    public void EffectPlay(int index,float playTime)
    {
        myEffect[index].time = playTime;
        myEffect[index].Play();
    }
    public void EffectPlay(int index, Transform StartPoint)
    {
        myEffect[index].transform.position = StartPoint.position;
        myEffect[index].Play();
    }
    // 인덱스 실행 , 반복출력
    public void EffectPlay(int index,float playTime,bool isLoop)
    {
        var main = myEffect[index].main;
        main.loop = isLoop;
        myEffect[index].time = playTime; 
        myEffect[index].Play();
    }
    // 인덱스 실행 , 시작위치 , 바라볼 방향
    public void EffectPlay(int index,float playTime,Transform StartPoint,Vector3 dir)
    {
        myEffect[index].time = playTime;
        myEffect[index].transform.position = StartPoint.position;
        myEffect[index].transform.LookAt(dir);
        myEffect[index].Play();

    }
    // 인덱스 실행 , 시작위치 , 회전
    public void EffectPlay(int index, float playTime,Transform StartPoint,Quaternion rot)
    {
        myEffect[index].time = playTime;
        myEffect[index].transform.position = StartPoint.position;
        myEffect[index].transform.rotation = rot;
        myEffect[index].Play();
    }

    #endregion

    public void Test1(int index)
    {
        
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

    EffectCode IEffect.GetEffectCode()
    {
        return myEffectCode;
    }


}
