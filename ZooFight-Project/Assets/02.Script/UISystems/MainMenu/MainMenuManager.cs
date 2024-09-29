using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class MainMenuManager : MonoBehaviour
{
    // 플레이
    public GameObject playBtn;

    // 설정
    public Button settingsBtn;

    // 게임 종료
    public GameObject exitBtn;
    private bool _isESC = true;


    // 매칭 이미지
    [SerializeField] private GameObject _matchingUI;
    [SerializeField] private Animator _anim;
    [SerializeField] private RawImage _uiRawImage;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool _isMatchmaking;
    private CancellationTokenSource _cts;

    private void Start()
    {
        _matchingUI.SetActive(false);
        settingsBtn.onClick.AddListener(OnSettingsButtonClick);
    }

    private void OnSettingsButtonClick()
    {
        if (SettingsController.Inst != null)
        {
            SettingsController.Inst.ClickSetting();
        }
#if DEBUG
        else
        {
            Debug.LogWarning("SettingsController 인스턴스를 찾을 수 없습니다.");
        }
#endif
    }

    private void OnDestroy()
    {
        if (_cts != null)
        {
            _cts.Cancel();
        }
    }

    public void ClickPlay()
    {
        //_matchingUI.SetActive(true);
        //StartMatchmakingTimer().Forget();

        // 로딩 씬을 비동기적으로 로드하고, 이후 게임 씬을 로드
        LoadLoadingScene().Forget();
    }

    public void ClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                                Application.Quit();     // 어플리케이션 종료
#endif
    }

    private async UniTaskVoid StartMatchmakingTimer()
    {
        _isMatchmaking = true;
        _cts = new CancellationTokenSource();

        int elapsedTime = 0;

        try
        {
            while (_isMatchmaking)
            {
                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                // 1초 대기
                await UniTask.Delay(1000, cancellationToken: _cts.Token);

                elapsedTime++;
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("매칭 취소됨");
        }
    }

    // 매칭을 종료할 때 호출
    public void StopMatchmaking()
    {
        _isMatchmaking = false;
        _cts.Cancel();
    }

    // 원형 로딩 이미지
    public void LoadLoadingImg()
    {
        _anim.SetBool("IsRotating", true);
    }


    private async UniTask LoadLoadingScene()
    {
        // 현재 로딩 씬이 이미 로드되어 있는 경우 언로드
        if (SceneManager.GetSceneByName("LoadingScene").isLoaded)
        {
            await SceneManager.UnloadSceneAsync("LoadingScene");
        }

        // 로딩 씬을 비동기적으로 로드
        AsyncOperation loadingSceneOp = SceneManager.LoadSceneAsync("LoadingScene");
        loadingSceneOp.allowSceneActivation = true; // 씬 활성화를 제어

        // 로딩 씬이 로드될 때까지 기다림
        while (!loadingSceneOp.isDone)
        {
            await UniTask.Yield(); // 프레임마다 대기하여 로딩 진행 상황을 체크
        }

        // 게임 씬을 비동기적으로 로드
        await LoadGameSceneAsync();
    }

    private async UniTask LoadGameSceneAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("GameScene");
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            await UniTask.Yield();
            if (op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
                return;
            }
        }
    }
}
