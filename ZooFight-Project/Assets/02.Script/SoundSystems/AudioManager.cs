using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    private void Start()
    {
        // 초기화 작업, 기본 BGM을 설정하는 등
        PlayBackgroundMusic(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic(scene.name);
    }

    public void PlayBackgroundMusic(string sceneName)
    {
        // BGM을 설정하는 로직
        AudioClip clip = GetClipForScene(sceneName, bgm);
        if (clip != null)
        {
            BGMSource.clip = clip;
            BGMSource.Play();
        }
        else
        {
            Debug.LogWarning("BGM clip not found for scene: " + sceneName);
        }
    }

    public void PlayUIEffect(int index)
    {
        // UI 사운드를 설정
        if (index >= 0 && index < sfxUI.Length)
        {
            SFXSource.PlayOneShot(sfxUI[index]);
        }
        else
        {
            Debug.LogWarning("UI 사운드 이펙트가 범위를 벗어남: " + index);
        }
    }

    private AudioClip GetClipForScene(string sceneName, AudioClip[] clips)
    {
        // 씬 이름에 따라 적절한 오디오 클립을 반환
        foreach (var clip in clips)
        {
            if (clip.name == sceneName)
            {
                return clip;
            }
        }
        return null;
    }
}
