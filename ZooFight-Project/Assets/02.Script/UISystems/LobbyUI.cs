using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    // 유저 닉네임
    [SerializeField] private TextMeshProUGUI _user1text;
    [SerializeField] private TextMeshProUGUI _user2text;
    [SerializeField] private TextMeshProUGUI _user3text;


    // 준비 버튼
    [SerializeField] public GameObject _readyBtn;
    // 준비 완료되면 체크 
    [SerializeField] private GameObject _checkBox;
    [SerializeField] private GameObject _check2Box;
    [SerializeField] private GameObject _check3Box;

    // 카운트다운
    [SerializeField] private bool[] _isPlayerReady; // 플레이어 준비 상태 배열
    [SerializeField] private bool _allPlayersReady = false; // 모든 플레이어가 준비 상태인지
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime = 30f;
    [SerializeField] private float readyTime = 10f;
    [SerializeField] private WaitForSeconds waitsec = new WaitForSeconds(0.5f);


    private void Start()
    {
        LoadUserName();
        //_checkBox.SetActive(false);
        //_check2Box.SetActive(false);
        //_check3Box.SetActive(false);
        for(int i = 0; i < _isPlayerReady.Length; i++)
        {
            _isPlayerReady[i] = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ClickReady();
        }
        // 모든 플레이어가 준비 상태인지 확인
        if (!_allPlayersReady && CheckIfAllPlayersReady())
        {
            // 모든 플레이어가 준비 상태일 때
            _allPlayersReady = true;
            // 카운트다운 10초로 설정
            remainingTime = readyTime;
        }

        // 카운트다운 진행
        if (remainingTime > 0)
        {
            if (remainingTime > 10) { timerText.color = Color.white; }
            remainingTime -= Time.deltaTime;
        }

        // 카운트다운이 0에 도달했을 때
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            if (!_allPlayersReady)
            {
                _allPlayersReady = true;
                for(int i = 0; i < _isPlayerReady.Length; i++)
                {
                    _isPlayerReady[i] = true;
                }
                remainingTime = readyTime;
            }
            OnLoadScene().Forget();
        }

        timerText.text = string.Format("{0:00}", remainingTime);
    }

    // 유저 닉네임 불러오기
    private void LoadUserName()
    {
        // 유저1의 닉네임
        //_user1text.text = ;
        // 유저2의 닉네임
        //_user2text.text = ;
        // 유저3의 닉네임
        //_user3text.text = ;
    }

    public void ClickReady()
    {
        Debug.Log("ClickReady");
        //int n = 0; //임시
        for(int i = 0; i < 3; i++)
        {
            _isPlayerReady[i] = true;
        }
        _checkBox.SetActive(true);
        _check2Box.SetActive(true);
        _check3Box.SetActive(true);
    }

    // 플레이어 준비 상태 체크
    private bool CheckIfAllPlayersReady()
    {
        foreach(bool isReady in _isPlayerReady)
        {
            if (!isReady) return false; // 하나라도 준비 상태가 아니라면 false
        }
        return true;
    }

    // 로딩신으로 전환
    private async UniTaskVoid OnLoadScene()
    {
        await UniTask.Yield();

        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("LoadingScene");
        loadSceneAsync.allowSceneActivation = false;

        while (!loadSceneAsync.isDone)
        {
            await UniTask.Yield();

            loadSceneAsync.allowSceneActivation = true;
        }
    }
}
