using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 사운드 코드 기준
/// 0 ~ 99 캐릭터 관련 사운드
/// 100 ~ 300 아이템 관련 사운드
/// 300 ~ 400 UI 관련 사운드
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
    Item11,
    ButtonClick = 200, 


    CodeCount
}

public static class SoundSettings
{
    public static Dictionary<SoundCode, SoundSpeaker> keys = new();
}

public class Soundmanager : Singleton<Soundmanager>
{

    public List<SoundSpeaker> SoundSpeakers;


    public SoundPool SoundPool;

    private void Awake()
    {
        for (int i = 0; i < SoundSpeakers.Count; i++)
        {
            if (SoundSpeakers[i] != null)
            {
                SoundSettings.keys.Add(SoundSpeakers[i].mySoundCode, SoundSpeakers[i]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeClone(SoundSpeaker sound)
    {
        if(!SoundPool.Clones.Contains(sound))
        {
            GameObject obj = Instantiate(sound.gameObject, SoundPool.transform);
        }
        else
        {
            return;
        }
    }

    public SoundSpeaker GetClone(SoundSpeaker sound)
    {
        if (!SoundPool.Clones.Contains(sound))
        {
            MakeClone(sound);
        }
        return SoundPool.Clones[SoundPool.Clones.IndexOf(sound)];
    }


}
