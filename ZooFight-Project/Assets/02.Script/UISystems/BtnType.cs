using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnType : Singleton<BtnType>
{
    public GameObject Settings;
    public GameObject Set;
    public CanvasGroup SettingGroup;
    public CanvasGroup DisplayGroup;
    public CanvasGroup AudioGroup;
    public CanvasGroup ControlGroup;
    public CanvasGroup SetGroup;

    private bool isESC = true;
    private bool isSetESC = true;

    void Start()
    {
        Settings.SetActive(false);
        Set.SetActive(false);
        CanvasGroupOff(SetGroup);
        CanvasGroupOff(SettingGroup);
    }

    private void Update()
    {
        ESC();
    }

    private void Awake()
    {
        base.Initialize();
        DontDestroyOnLoad(gameObject);
    }

    public void ClickSetting()
    {
        //Pause(true);                                                    // 일시정지 추가
        Settings.SetActive(true);
        isESC = false;
        CanvasGroupOn(SettingGroup);
        CanvasGroupOn(DisplayGroup);
        CanvasGroupOff(AudioGroup);
        CanvasGroupOff(ControlGroup);
    }
    public void ClickContinue()
    {
        //Pause(false);
        CanvasGroupOff(SettingGroup);
    }
    public void ClickStart()
    {
        LoadingManager.LoadSceneHandle("MultiplayerLobbyScene", 0);
    }
    public void ClickDisplay()
    {
        CanvasGroupOn(DisplayGroup);
        CanvasGroupOff(AudioGroup);
        CanvasGroupOff(ControlGroup);
    }
    public void ClickAudio()
    {
        CanvasGroupOff(DisplayGroup);
        CanvasGroupOn(AudioGroup);
        CanvasGroupOff(ControlGroup);
    }
    public void ClickControl()
    {
        CanvasGroupOff(DisplayGroup);
        CanvasGroupOff(AudioGroup);
        CanvasGroupOn(ControlGroup);
    }

    public void ClickQuit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ClickExit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                                Application.Quit();     // 어플리케이션 종료
        #endif
    }

    public void ClickESC()
    {
        isESC = true;
        CanvasGroupOff(SettingGroup);
    }

    public void ESC()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isESC && isSetESC) 
            {
                isSetESC = false;
                Set.SetActive(true);
                CanvasGroupOn(SetGroup); 
            }
            else if(isESC && !isSetESC) 
            {
                isSetESC = true;
                CanvasGroupOff(SetGroup);
            }
            else if (!isESC)
            {
                ClickESC();
            }
        }
    }

    //void Pause(bool isPause)
    //{
    //    if (true == isPause)     // 일시정지 상태
    //    {
    //        Time.timeScale = 0;
    //    }
    //    else                            // 일시정지 해제
    //    {
    //        Time.timeScale = 1;
    //    }
    //}
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
