using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour
{
    // SoundSpeaker 객체들 관리
    public List<SoundSpeaker> clones = new List<SoundSpeaker>();

    // 클론 추가
    public void AddClone(SoundSpeaker clone)
    {
        if (!clones.Contains(clone)) clones.Add(clone);
    }

    // 특정 AudioClip을 가진 클론을 검색
    public SoundSpeaker FindClone(AudioClip clip)
    {
        return clones.Find(speaker => speaker.myClip == clip);
    }

    // 모든 클론을 제거
    public void ClearClones()
    {
        foreach (var clone in clones)
        {
            Destroy(clone.gameObject);
        }
        clones.Clear();
    }
}
