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
    // ���� �г���
    [SerializeField] private TextMeshProUGUI _user1text;
    [SerializeField] private TextMeshProUGUI _user2text;
    [SerializeField] private TextMeshProUGUI _user3text;


    // �غ� ��ư
    public GameObject readyBtn;
    // �غ� �Ϸ�Ǹ� üũ 
    public GameObject checkBox;
    public GameObject check2Box;
    public GameObject check3Box;

    // ī��Ʈ�ٿ�
    [SerializeField] private bool[] _isPlayerReady = new bool[3]; // �÷��̾� �غ� ���� �迭
    [SerializeField] private bool _allPlayersReady = false; // ��� �÷��̾ �غ� ��������
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime = 20f;
    [SerializeField] private float readyTime = 10f;


    private void Start()
    {
        LoadUserName();
        Debug.Log("Test: Start Initialize Ready State");
        InitializeReadyState();
    }

    private void Update()
    {
        // ������ ����(�غ�)�� ������ ��� //���߿� ���� ����
        HandleInput();
        HandleCountdown();
    }

    // ���� �г��� �ҷ�����
    private void LoadUserName()
    {
        // ����1�� �г���
        //_user1text.text = ;
        // ����2�� �г���
        //_user2text.text = ;
        // ����3�� �г���
        //_user3text.text = ;
    }

    // �ʱ� �غ� ���� ���� ��Ȱ��ȭ
    private void InitializeReadyState()
    {
        checkBox.SetActive(false);
        check2Box.SetActive(false);
        check3Box.SetActive(false);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Test: Enter������");
            ClickReady();
        }
    }

    private void HandleCountdown()
    {
        // ��� �÷��̾ �غ�Ǹ� 10�� ī��Ʈ�ٿ� ����
        if (CheckIfAllPlayersReady() && !_allPlayersReady)
        {
            StartCountdown();
        }
        // ī��Ʈ�ٿ� ����
        if (remainingTime > 0)
        {
            timerText.color = !_allPlayersReady ? Color.white 
                : timerText.color = new Color(233 / 255f, 190 / 255f, 85 / 255f, 1);

            remainingTime -= Time.deltaTime;
        }
        else if (!_allPlayersReady)
        {
            Debug.Log("Test: ī��Ʈ�ٿ� �� StartCountdown() ���� ");
            remainingTime = 0;
            StartCountdown();
        }
        else
        {
            OnLoadScene().Forget();
        }
        timerText.text = $"{Mathf.CeilToInt(remainingTime):00}";
    }

    private void StartCountdown()
    {
        // ��� �÷��̾ �غ� ������ ��
        _allPlayersReady = true;
        // ī��Ʈ�ٿ� 10�ʷ� ����
        remainingTime = readyTime;
        checkBox.SetActive(true);
        check2Box.SetActive(true);
        check3Box.SetActive(true);
    }

    public void ClickReady()
    {
        Debug.Log("Test: ClickReady");
        //int n = 0; //�ӽ�
        for(int i = 0; i < _isPlayerReady.Length; i++)
        {
            _isPlayerReady[i] = true;
        }
        // Ŭ�� ���� �ش� ������ �̹����� Ȱ��ȭ�� ��
        checkBox.SetActive(true);
        check2Box.SetActive(true);
        check3Box.SetActive(true);
    }

    // �÷��̾� �غ� ���� üũ
    private bool CheckIfAllPlayersReady()
    {
        foreach(bool isReady in _isPlayerReady)
        {
            if (!isReady) return false; // �ϳ��� �غ� ���°� �ƴ϶�� false
        }
        return true;
    }

    // �ε������� ��ȯ
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
