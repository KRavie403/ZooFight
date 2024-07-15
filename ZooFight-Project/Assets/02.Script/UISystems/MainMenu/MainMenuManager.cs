using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // 플레이
    public GameObject playBtn;

    // 설정
    public GameObject settingsBtn;

    //public GameObject Settings;
    //public CanvasGroup SettingGroup;
    //public CanvasGroup DisplayGroup;
    //public CanvasGroup AudioGroup;
    //public CanvasGroup ControlGroup;

    private bool _isESC = true;

    // 게임 종료
    public GameObject exitBtn;

    private void Start()
    {
        //if (Settings != null)
        //{
        //    Settings.SetActive(false);
        //}
        //CanvasGroupOff(SettingGroup);
    }

    public void ClickPlay()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void ClickSetting()
    {
        //if (Settings != null)
        //{
        //    Settings.SetActive(true);
        //    _isESC = false;
        //    CanvasGroupOn(SettingGroup);
        //    CanvasGroupOn(DisplayGroup);
        //    CanvasGroupOff(AudioGroup);
        //    CanvasGroupOff(ControlGroup);
        //}
    }

    public void ClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                                Application.Quit();     // 어플리케이션 종료
#endif
    }

    //private void CanvasGroupOn(CanvasGroup cg)
    //{
    //    cg.alpha = 1;
    //    cg.interactable = true;
    //    cg.blocksRaycasts = true;
    //}
    //private void CanvasGroupOff(CanvasGroup cg)
    //{
    //    cg.alpha = 0;
    //    cg.interactable = false;
    //    cg.blocksRaycasts = false;
    //}
}
