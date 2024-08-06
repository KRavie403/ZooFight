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
    public GameObject settingsBtn;


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
        SceneManager.LoadScene("LobbyScene");
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
}
