using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoginSoundSpeaker : MonoBehaviour
{
    public Button[] buttons;

    private static readonly System.Random _random = new System.Random();

    private void Start()
    {
        foreach(var btn in buttons)
        {
            btn.onClick.AddListener(OnSettingsButtonClick);
        }
    }

    private void OnSettingsButtonClick()
    {
        if (AudioManager.Inst != null)
        {
            AudioManager.Inst.PlayUIEffect(GetRandomNumber());
        }
#if DEBUG
        else
        {
            Debug.LogWarning("SettingsController 인스턴스를 찾을 수 없습니다.");
        }
#endif
    }

    public static int GetRandomNumber()
    {
        return _random.Next(0, 12); // 0부터 11까지의 숫자를 반환
    }
}
