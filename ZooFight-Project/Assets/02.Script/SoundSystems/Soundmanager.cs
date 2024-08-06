using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 사운드 코드 기준
/// 0 ~ 99 캐릭터 관련 사운드
/// 100 ~ 199 아이템 관련 사운드
/// 200 ~ 299 UI 관련 사운드
/// 300 ~ 399 BGM 관련 사운드
/// 추후 확장가능
/// </summary>
/// 
public enum SoundCode
{
    MainBgm =0,CharacterMove,CharacterJump,CharacterDameged , CharacterAttack,
    BananaTrap = 100,
    GuardDrink, StaminaDrink, PowerDrink,
    Bomb, SpiderBomb, InkBomb,
    ToyHammer, WhippingMachine, MinimalRazer,
    CurseScroll, BlockChangeScroll,
    ButtonClick = 200, GameStart,
    LoginScene = 300, MainMenuScene, LobbyScene, LoadingScene, GameScene, GameResultScene, CreditScene,
    CodeCount
}

public static class SoundSettings
{
    public static Dictionary<SoundCode, SoundSpeaker> keys = new();
}

public class SoundManager : Singleton<SoundManager>
{

    public List<SoundSpeaker> SoundSpeakers;    // 에디터에서 할당된 사운드 스피커 리스트
    public SoundPool SoundPool;                           // 사운드 풀 객체

    protected override void Awake()
    {
        base.Awake();
        InitializeSoundSettings();      // 초기화 메소드 호출
    }

    private void InitializeSoundSettings()
    {
        // 사운드 스피커 리스트를 딕셔너리에 추가하는 초기화 메소드
        for (int i = 0; i < (int)SoundCode.CodeCount; i++)
        {
            if (SoundSpeakers[i] != null)
            {
                //SoundSettings.keys.Add(SoundSpeakers[i].mySoundCode, SoundSpeakers[i]);
            }
        }
    }

    private SoundSpeaker CreateClone(SoundSpeaker sound)
    {
        // 특정 사운드 스피커의 클론을 생성하는 메소드
        GameObject obj = Instantiate(sound.gameObject, SoundPool.transform);
        SoundSpeaker clone = obj.GetComponent<SoundSpeaker>();
        SoundPool.AddClone(clone);
        return clone;
    }

    public SoundSpeaker GetClone(SoundSpeaker sound)
    {
        // 특정 사운드 스피커의 클론을 반환하는 메소드
        SoundSpeaker clone = SoundPool.FindClone(sound.myClip);
        if (clone == null)
        {
            clone = CreateClone(sound);
        }
        return clone;
    }

    public void PlaySound(SoundCode code)
    {
        // 사운드 코드를 입력받아 해당 사운드를 재생하는 메소드
        if (SoundSettings.keys.TryGetValue(code, out SoundSpeaker speaker))
        {
            SoundSpeaker clone = GetClone(speaker);
            clone.SoundPlay();
        }
    }
}
