using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd.Tcp;
using Battlehub.Dispatcher;
using BackEnd;
using TMPro;
using System;

public class MatchingTest : MonoBehaviour
{
    private static MatchingTest instance;
    private int numOfClient = -1;
    [SerializeField]
    private List<TextMeshProUGUI> matchingNickname;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public static MatchingTest GetInstance()
    {
        if (instance == null)
        {
            return null;
        }
        return instance;
    }

    public void RequestInfo()
    {
        var matchInstance = BackEndMatchManager.GetInstance();
        if (matchInstance == null)
        {
            return;
        }

        numOfClient = matchInstance.gameRecords.Count;
        Debug.Log($"numOfClient = {numOfClient}");

        byte index = 0;
        foreach (var record in matchInstance.gameRecords.OrderByDescending(x => x.Key))
        {
            var name = record.Value.m_nickname;

            matchingNickname[index++].text = name;
        }
    }

    public void RequestMatch()
    {
        BackEndMatchManager.GetInstance().RequestMatchMaking();
    }

    public void LeaveMatchRoom()
    {
        BackEndMatchManager.GetInstance().CancelRegistMatchMaking();
    }

    public void CreateRoom()
    {
        if (BackEndMatchManager.GetInstance().CreateMatchRoom() == true)
        {
            Debug.Log("���� ��������ϴ�.");
        }
    }

    public void CreateRoomResult(bool isSuccess, List<MatchMakingUserInfo> userList = null)
    {
        if(isSuccess == true)
        {
            Debug.Log("���� ����������ϴ�.");
        }
        else
        {
            Debug.Log("���� ������ �����߽��ϴ�.\n\n��� �� �ٽ� �õ����ּ���.");
        }
    }
}
