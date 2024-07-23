using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSpeaker : MonoBehaviour
{
    public AudioClip myClip;    // 사운드 클립

    public SoundCode mySoundCode;

    private void Start()
    {
        SoundPlay();
    }
    // 사운드 재생
    public void SoundPlay()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = myClip;
        audioSource.Play();
    }
}
