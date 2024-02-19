using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SoundCode
{
    MainBgm =0,


    CodeCount
}

public static class SoundSettings
{
    public static Dictionary<SoundCode, SoundSpeaker> keys = new();
}

public class Soundmanager : Singletone<Soundmanager>
{

    public SoundSpeaker[] SoundSpeakers = null;

    public SoundPool SoundPool;

    private void Awake()
    {
        for (int i = 0; i < (int)SoundCode.CodeCount; i++)
        {
            if (SoundSpeakers[i] != null)
            {
                SoundSettings.keys.Add((SoundCode)i, SoundSpeakers[i]);
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
