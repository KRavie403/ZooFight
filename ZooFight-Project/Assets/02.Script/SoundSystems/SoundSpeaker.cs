using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSpeaker : MonoBehaviour
{
    public AudioSource Speaker;

    public AudioClip myClip;

    public SoundCode mySoundCode;


    private void Awake()
    {
        Speaker.clip = myClip;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Àç»ý
    public void SoundPlay()
    {
        Speaker.Play();
    }
    public void SoundPlay(bool isLoop)
    {
        Speaker.loop = isLoop;
        Speaker.Play();
    }
    public void SoundPlay(ulong Delay ,bool isLoop = false)
    {
        Speaker.loop = isLoop;
        Speaker.Play(Delay);
    }

    public void SoundStop()
    {

    }
    public void SoundStop(float WaitTime)
    {

    }

    #endregion

    IEnumerator StopTimer(float WaitTime)
    {
        float T = 0;
        while (T < WaitTime)
        {
            T += Time.deltaTime;
            yield return null;
        }
        Speaker.Play();
    }



}
