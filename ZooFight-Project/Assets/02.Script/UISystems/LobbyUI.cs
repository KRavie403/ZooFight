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
    [SerializeField] public GameObject _readyBtn;
    // �غ� �Ϸ�Ǹ� üũ 
    [SerializeField] private GameObject _checkBox;
    [SerializeField] private GameObject _check2Box;
    [SerializeField] private GameObject _check3Box;

    // ī��Ʈ�ٿ�
    [SerializeField] private bool[] _isPlayerReady; // �÷��̾� �غ� ���� �迭
    [SerializeField] private bool _allPlayersReady = false; // ��� �÷��̾ �غ� ��������
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
        // ��� �÷��̾ �غ� �������� Ȯ��
        if (!_allPlayersReady && CheckIfAllPlayersReady())
        {
            // ��� �÷��̾ �غ� ������ ��
            _allPlayersReady = true;
            // ī��Ʈ�ٿ� 10�ʷ� ����
            remainingTime = readyTime;
        }

        // ī��Ʈ�ٿ� ����
        if (remainingTime > 0)
        {
            if (remainingTime > 10) { timerText.color = Color.white; }
            remainingTime -= Time.deltaTime;
        }

        // ī��Ʈ�ٿ��� 0�� �������� ��
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

    public void ClickReady()
    {
        Debug.Log("ClickReady");
        //int n = 0; //�ӽ�
        for(int i = 0; i < 3; i++)
        {
            _isPlayerReady[i] = true;
        }
        _checkBox.SetActive(true);
        _check2Box.SetActive(true);
        _check3Box.SetActive(true);
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
