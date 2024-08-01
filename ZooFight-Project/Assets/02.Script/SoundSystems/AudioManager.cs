using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip[] bgm;
    public AudioClip[] sfxBasic;
    public AudioClip[] sfxItemUse;
    public AudioClip[] sfxUI;
}
