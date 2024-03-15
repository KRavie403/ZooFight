using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer mixer;
    public AudioSource BGM;
    public AudioClip[] BGMList;

    private void Start()
    {
        BGMPlay(BGMList[0]);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public GameObject SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();
        //if(sfxName != "")
        return go;
    }

    public void BGMPlay(AudioClip clip)
    {
        BGM.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        BGM.clip = clip;
        BGM.loop = true;
        BGM.volume = 0.1f;
        BGM.Play();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < BGMList.Length; i++)
        {
            if(scene.name == BGMList[i].name)
                BGMPlay(BGMList[i]);
        }
    }
}
