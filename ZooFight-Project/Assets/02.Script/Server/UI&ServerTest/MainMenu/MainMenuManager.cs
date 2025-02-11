using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

using Battlehub.Dispatcher;
using BackEnd;
using static BackEnd.SendQueue;

public partial class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager instance;

    // 플레이
    public GameObject playBtn;

    // 설정
    public Button settingsBtn;

    // 게임 종료
    public GameObject exitBtn;
    private bool _isESC = true;

    // 캐릭터 선택
    public GameObject switchBtn;
    public GameObject selectBtn;
    public GameObject[] arrawBtns;
    private bool _isSelect = true;

    // 매칭 유저
    public ToggleGroup tabObject;
    private TabUI[] matchInfotabList;

    // 매칭 이미지
    [SerializeField] private GameObject _matchingUI;
    [SerializeField] private Animator _anim;
    [SerializeField] private RawImage _uiRawImage;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool _isMatchmaking;
    private CancellationTokenSource _cts;

    private Action<bool, string> loginSuccessFunc = null;
    private const string BackendError = "statusCode : {0}\nErrorCode : {1}\nMessage : {2}";

    public static MainMenuManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("LobbyUI 인스턴스가 존재하지 않습니다.");
            return null;
        }

        return instance;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;

        // 재접속 로직 제외
        BackEndMatchManager.GetInstance().IsMatchGameActivate();
    }

    private void Start()
    {
        _matchingUI.SetActive(false);
        switchBtn.SetActive(true);
        selectBtn.SetActive(false);
        foreach (var btn in arrawBtns) btn.SetActive(false);
        settingsBtn.onClick.AddListener(OnSettingsButtonClick);

        matchInfotabList = tabObject.GetComponentsInChildren<TabUI>();
        //matchRecordTabList = recordObject.GetComponentsInChildren<TabUI>();
        int index = 0;
        foreach (var info in BackEndMatchManager.GetInstance().matchInfos)
        {
            matchInfotabList[index].SetTabText(info.title);
            matchInfotabList[index].index = index;
            //matchRecordTabList[index].SetTabText(info.title);
            //matchRecordTabList[index].index = index;
            index += 1;
        }

        for (int i = BackEndMatchManager.GetInstance().matchInfos.Count; i < matchInfotabList.Length; ++i)
        {
            matchInfotabList[i].gameObject.SetActive(false);
            //matchRecordTabList[i].gameObject.SetActive(false);
        }
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

    /// <summary>
    /// 게임 플레이
    /// </summary>
    public void ClickPlay()
    {
        // 매치 서버에 대기방 생성 요청
        if (BackEndMatchManager.GetInstance().CreateMatchRoom() == true)
        {
            SetLoadingObjectActive(true);
        }

        var matchManager = BackEndMatchManager.GetInstance();
        if (matchManager == null)
        {
            Debug.LogError("BackEndMatchManager instance is null.");
            return;
        }

        Enqueue(Backend.BMember.GetUserInfo, callback =>
        {

            if (!callback.IsSuccess())
            {
                Debug.LogError("유저 정보 불러오기 실패\n" + callback);
                loginSuccessFunc(false, string.Format(BackendError,
                callback.GetStatusCode(), callback.GetErrorCode(), callback.GetMessage()));
                return;
            }
            Debug.Log("유저정보\n" + callback);

            var info = callback.GetReturnValuetoJSON()["row"];
            if (loginSuccessFunc == null)
            {
                Debug.Log("loginSuccess is null");
            }

            if (loginSuccessFunc != null)
            {
                BackEndMatchManager.GetInstance().GetMatchList(loginSuccessFunc);
            }
        });
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    public void ClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                                Application.Quit();     // 어플리케이션 종료
#endif
    }

    /// <summary>
    /// 캐릭터 변경
    /// </summary>
    public void ClickSwitch()
    {
        switchBtn.SetActive(false);
        selectBtn.SetActive(true);
        foreach (var btn in arrawBtns) btn.SetActive(true);
    }

    /// <summary>
    /// 캐릭터 적용
    /// </summary>
    public void ClickSelect()
    {
        switchBtn.SetActive(true);
        selectBtn.SetActive(false);
        foreach (var btn in arrawBtns) btn.SetActive(false);
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
                Debug.Log($"초기 elapsedTime 값: {elapsedTime}");

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

    public void SetLoadingObjectActive(bool isActive)
    {
        _matchingUI.SetActive(isActive);
        LoadLoadingImg();       // 매칭 큐
        StartMatchmakingTimer().Forget();
    }

    // 원형 로딩 이미지
    public void LoadLoadingImg()
    {
        _anim.SetBool("IsRotating", true);
    }

    public void OpenGameRecord()
    {
        //if (loadingObject.activeSelf || errorObject.activeSelf || requestProgressObject.activeSelf || matchDoneObject.activeSelf || reconnectObject.activeSelf)
        //{
        //    return;
        //}
        GetGameRecord();
    }

    public void GetGameRecord()
    {
        //loadingObject.SetActive(true);

        //int index = -1;
        //foreach (var tab in matchRecordTabList)
        //{
        //    if (tab.IsOn() == true)
        //    {
        //        index = tab.index;
        //        break;
        //    }
        //}

        //if (index < 0)
        //{
        //    Debug.Log("활성화된 탭이 없습니다.");
        //    return;
        //}

        //BackEndMatchManager.GetInstance().GetMyMatchRecord(index, (MatchRecord record, bool isSuccess) =>
        //{
        //    Dispatcher.Current.BeginInvoke(() =>
        //    {
                //loadingObject.SetActive(false);
                //recordObject.SetActive(true);

                //if (fillWinChart != null)
                //{
                //    StopCoroutine(fillWinChart);
                //}
                //if (fillLoseChart != null)
                //{
                //    StopCoroutine(fillLoseChart);
                //}
                //if (countingWinRate != null)
                //{
                //    StopCoroutine(countingWinRate);
                //}

                //recordContent[0].text = record.matchTitle;
                //recordContent[2].text = record.matchType.ToString();
                //recordContent[3].text = record.modeType.ToString();

                //recordContent[1].text = "0%";
                //recordContent[4].text = "-";
                //recordContent[5].text = "0";
                //recordContent[6].text = "0";

                //recordChart[0].fillAmount = (float)0;
                //recordChart[1].fillAmount = 0;

                //if (isSuccess == false)
                //{
                //    // 조회 실패
                //    SetErrorObject("매칭 기록 조회에 실패하였습니다.\n\n잠시 후 다시 시도해주세요.");
                //    return;
                //}

                //if (record.win == -1)
                //{
                //    // 매칭 기록이 없음
                //    SetErrorObject("매칭 기록이 존재하지 않습니다.\n\n해당 매칭을 먼저 시도해주세요.");
                //    return;
                //}

                //recordContent[4].text = record.score;
                //recordContent[5].text = record.win.ToString();
                //recordContent[6].text = (record.numOfMatch - record.win).ToString();
                //float winRate = (float)record.win / record.numOfMatch;
                //fillWinChart = StartCoroutine(FillPieChart(recordChart[0], winRate));
                //fillLoseChart = StartCoroutine(FillPieChart(recordChart[1], 1 - winRate));
                //countingWinRate = StartCoroutine(FillWinRate((int)record.winRate));
        //    });
        //});
    }

    public void SetErrorObject(string error)
    {
        //errorObject.SetActive(true);
        //errorText.text = error;
    }

    public void EnableReconnectObject()
    {
        Dispatcher.Current.BeginInvoke(() =>
        {
            //loadingObject.SetActive(true);
            Invoke("SetReconnectObject", 1.0f);
        });
    }

    private void SetReconnectObject()
    {
        //loadingObject.SetActive(false);
        //reconnectObject.SetActive(true);
    }

    public void ReconnectInGameProcess()
    {
        //var tmp = matchDoneObject.GetComponentInChildren<Text>();
        //tmp.text = "재접속 중...";
        //modelObject.SetActive(false);
        //MatchDoneCallback();

        BackEndMatchManager.GetInstance().ProcessReconnect();
    }

    public void JoinMatchProcess()
    {
        BackEndMatchManager.GetInstance().JoinMatchServer();
    }

    public void ChangeTab()
    {
        int index = 0;
        foreach (var tab in matchInfotabList)
        {
            if (tab.IsOn() == true)
            {
                break;
            }
            index += 1;
        }
        var matchInfo = BackEndMatchManager.GetInstance().matchInfos[index];
        //matchInfoText.text = string.Format(matchInfoStr, matchInfo.headCount, matchInfo.isSandBoxEnable.Equals(true) ? "활성화" : "비활성화",
            //matchInfo.matchType, matchInfo.matchModeType);
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

    //public void CreateRoomResult(bool isSuccess, List<MatchMakingUserInfo> userList = null)
    //{
    //    // 대기 방 생성에 성공 시 대기방 UI를 활성화 시키고,
    //    // 친구목록을 조회
    //    if (isSuccess == true)
    //    {
    //        readyRoomObject.SetActive(true);
    //        SetFriendList();
    //        if (userList == null)
    //        {
    //            SetReadyUserList(BackEndServerManager.GetInstance().myNickName);
    //        }
    //        else
    //        {
    //            SetReadyUserList(userList);
    //        }
    //    }
    //    // 대기 방 생성에 실패 시 에러를 띄움
    //    else
    //    {
    //        SetLoadingObjectActive(false);
    //        SetErrorObject("대기방 생성에 실패했습니다.\n\n잠시 후 다시 시도해주세요.");
    //    }
    //}

    //public void LeaveReadyRoom()
    //{
    //    BackEndMatchManager.GetInstance().LeaveMatchLoom();
    //    // readyRoomObject.SetActive(false);
    //}

    //public void CloseRoomUIOnly()
    //{
    //    readyRoomObject.SetActive(false);
    //}

    public void RequestMatch(int index)
    {
        //if (loadingObject.activeSelf || recordObject.activeSelf || errorObject.activeSelf || requestProgressObject.activeSelf || matchDoneObject.activeSelf)
        //{
        //    return;
        //}
        
        //// 현재 매칭 인원 확인 (예: 그룹 매칭 필요)
        //int requiredPlayers = int.Parse(matchInfos[index].headCount);
        //int currentPlayers = GetCurrentMatchPlayers(matchInfos[index].matchType);

        //if (currentPlayers < requiredPlayers)
        //{
        //    Debug.Log("매칭을 시작하기에 필요한 인원이 부족합니다.");
        //    return;
        //}

        // 매칭 요청 보내기
        foreach (var tab in matchInfotabList)
        {
            if (tab.IsOn() == true)
            {
                BackEndMatchManager.GetInstance().RequestMatchMaking(tab.index);
                return;
            }
        }

        Debug.Log("활성화된 탭이 존재하지 않습니다.");

        // 로딩 씬을 비동기적으로 로드하고, 이후 게임 씬을 로드
        LoadLoadingScene().Forget();
    }

    //private void ClearReadyUserList()
    //{
    //    readyUserList = new List<string>();
    //    var parent = readyUserListParent.transform;

    //    while (parent.childCount > 0)
    //    {
    //        var child = parent.GetChild(0);
    //        GameObject.DestroyImmediate(child.gameObject);
    //    }
    //}
}
