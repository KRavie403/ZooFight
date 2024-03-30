using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ����Ʈ�� ���� , ���� �ð� , ���� , ��¹��� , �����ġ �� �����ϴ� Ŭ���� 
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

    #region ����Ʈ ����
    // �ε��� ����
    public void EffectPlay(int index)
    {
        myEffect[index].Play();
    }
    // �ε��� ���� + �������� ����
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
    // �ε��� ���� , �ݺ����
    public void EffectPlay(int index,float playTime,bool isLoop)
    {
        var main = myEffect[index].main;
        main.loop = isLoop;
        myEffect[index].time = playTime; 
        myEffect[index].Play();
    }
    // �ε��� ���� , ������ġ , �ٶ� ����
    public void EffectPlay(int index,float playTime,Transform StartPoint,Vector3 dir)
    {
        myEffect[index].time = playTime;
        myEffect[index].transform.position = StartPoint.position;
        myEffect[index].transform.LookAt(dir);
        myEffect[index].Play();

    }
    // �ε��� ���� , ������ġ , ȸ��
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