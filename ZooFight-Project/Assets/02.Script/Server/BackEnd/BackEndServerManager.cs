using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;
using static BackEnd.SendQueue;

public class BackEndServerManager : MonoBehaviour
{
    private static BackEndServerManager instance;   // �ν��Ͻ�
    public bool isLogin { get; private set; }   // �α��� ����

    private string tempNickName;                        // ������ �г��� (id�� ����)
    public string myNickName { get; private set; } = string.Empty;  // �α����� ������ �г���
    public string myIndate { get; private set; } = string.Empty;    // �α����� ������ inDate
    private Action<bool, string> loginSuccessFunc = null;

    private const string BackendError = "statusCode : {0}\nErrorCode : {1}\nMessage : {2}";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
        BackendSetup();
    }

    public static BackEndServerManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("BackEndServerManager �ν��Ͻ��� �������� �ʽ��ϴ�.");
            return null;
        }

        return instance;
    }

    void Start()
    {
        isLogin = false;

        var bro = Backend.Initialize(true);

        if (bro.IsSuccess())
        {
        }
        else
        {
            Debug.LogError("�ڳ� �ʱ�ȭ ���� : " + bro);
        }

    }

    private void Update()
    {
        //if (Backend.IsInitialized)
        //{
        //    Backend.AsyncPoll();
        //}
        Backend.AsyncPoll();
    }

    private void BackendSetup()
    {
        var bro = Backend.Initialize(true);

        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro);
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ����: " + bro);
        }

    }


    // ���� ���� �ҷ����� �����۾�
    private void OnPrevBackendAuthorized()
    {
        isLogin = true;

        OnBackendAuthorized();
    }

    // ���� ���� ���� �ҷ�����
    private void OnBackendAuthorized()
    {
        Enqueue(Backend.BMember.GetUserInfo, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError("���� ���� �ҷ����� ����\n" + callback);
                loginSuccessFunc(false, string.Format(BackendError,
                callback.GetStatusCode(), callback.GetErrorCode(), callback.GetMessage()));
                return;
            }
            Debug.Log("��������\n" + callback);

            var info = callback.GetReturnValuetoJSON()["row"];
            if (info["nickname"] == null)
            {
                //LoginUI.GetInstance().ActiveNickNameObject();
                return;
            }
            myNickName = info["nickname"].ToString();
            myIndate = info["inDate"].ToString();

            if (loginSuccessFunc != null)
            {
                BackEndMatchManager.GetInstance().GetMatchList(loginSuccessFunc);
            }
        });
    }


}
