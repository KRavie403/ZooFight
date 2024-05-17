using System;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using BackEnd.Tcp;
using Protocol;
using Battlehub.Dispatcher;
using System.Linq;

/*
 * ��ġ�Ŵ���
 * BackEndMatchManager.cs���� ���ǵ� ��ɵ�
 * ��ġ�Ŵ������� �ʿ��� ���� ����
 * GameManager �̺�Ʈ ���
 * ��ġ����ŷ �ڵ鷯 ���
 * �ΰ��� �ڵ鷯 ���
 * ��Ī ���� ����� BackEndMatch.cs�� ����
 * �ΰ��� ���� ����� BackEndInGame.cs�� ����
 */

public partial class BackEndMatchManager : MonoBehaviour
{
    // �ֿܼ��� ������ ��Ī ī�� ����
    public class MatchInfo
    {
        public string title;                // ��Ī ��
        public string inDate;               // ��Ī inDate (UUID)
        public MatchType matchType;         // ��ġ Ÿ��
        public MatchModeType matchModeType; // ��ġ ��� Ÿ��
        public string headCount;            // ��Ī �ο�
        public bool isSandBoxEnable;        // ����ڽ� ��� (AI��Ī)
    }

    private static BackEndMatchManager instance = null; // �ν��Ͻ�

    //public List<MatchInfo> matchInfos { get; private set; } = new List<MatchInfo>();  // �ֿܼ��� ������ ��Ī ī����� ����Ʈ
    public MatchInfo matchInfos { get; private set; }

    public List<SessionId> sessionIdList { get; private set; }  // ��ġ�� �������� �������� ���� ���
    public Dictionary<SessionId, int> teamInfo { get; private set; }    // ��ġ�� �������� �������� �� ���� (MatchModeType�� team�� ��쿡�� ���)
    public Dictionary<SessionId, MatchUserGameRecord> gameRecords { get; private set; } = null;  // ��ġ�� �������� �������� ��Ī ���
    private string inGameRoomToken = string.Empty;  // ���� �� ��ū (�ΰ��� ���� ��ū)
    public SessionId hostSession { get; private set; }  // ȣ��Ʈ ����
    private ServerInfo roomInfo = null;             // ���� �� ����
    public bool isReconnectEnable { get; private set; } = false;

    public bool isConnectMatchServer { get; private set; } = false;
    private bool isConnectInGameServer = false;
    private bool isJoinGameRoom = false;
    public bool isReconnectProcess { get; private set; } = false;
    public bool isSandBoxGame { get; private set; } = false;

    private int numOfClient = 2;                    // ��ġ�� ������ ������ �� ��

    #region Host
    private bool isHost = false;                    // ȣ��Ʈ ���� (�������� ������ SuperGamer ������ ������)
    private Queue<KeyMessage> localQueue = null;    // ȣ��Ʈ���� ���÷� ó���ϴ� ��Ŷ�� �׾Ƶδ� ť (����ó���ϴ� �����ʹ� ������ �߼� ����)
    #endregion

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public static BackEndMatchManager GetInstance()
    {
        if (!instance)
        {
            Debug.LogError("BackEndMatchManager �ν��Ͻ��� �������� �ʽ��ϴ�.");
            return null;
        }

        return instance;
    }

    void OnApplicationQuit()
    {
        if (isConnectMatchServer)
        {
            LeaveMatchServer();
            Debug.Log("ApplicationQuit - LeaveMatchServer");
        }
    }

    void Start()
    {

        //GameManager.OnGameReconnect += OnGameReconnect; �̰� ���ӸŴ����� �߰��ؾ��Ұ�.
        // �ڵ鷯 ����
        MatchMakingHandler();
        GameHandler();
        ExceptionHandler();
    }


    public bool IsHost()
    {
        return isHost;
    }

    public bool IsMySessionId(SessionId session)
    {
        return Backend.Match.GetMySessionId() == session;
    }

    public string GetNickNameBySessionId(SessionId session)
    {
        return gameRecords[session].m_nickname;
    }

    public bool IsSessionListNull()
    {
        return sessionIdList == null || sessionIdList.Count == 0;
    }

    private bool SetHostSession()
    {
        // ȣ��Ʈ ���� ���ϱ�
        // �� Ŭ���̾�Ʈ�� ��� ���� (ȣ��Ʈ ���� ���ϴ� ������ ��� �����Ƿ� ������ Ŭ���̾�Ʈ�� ��� ������ ���������� ������� ����.)

        Debug.Log("ȣ��Ʈ ���� ���� ����");
        // ȣ��Ʈ ���� ���� (�� Ŭ���̾�Ʈ���� ���� ������ �ٸ� �� �ֱ� ������ ����)
        sessionIdList.Sort();
        isHost = false;
        // ���� ȣ��Ʈ ��������
        foreach (var record in gameRecords)
        {
            if (record.Value.m_isSuperGamer == true)
            {
                if (record.Value.m_sessionId.Equals(Backend.Match.GetMySessionId()))
                {
                    isHost = true;
                }
                hostSession = record.Value.m_sessionId;
                break;
            }
        }

        Debug.Log("ȣ��Ʈ ���� : " + isHost);

        // ȣ��Ʈ �����̸� ���ÿ��� ó���ϴ� ��Ŷ�� �����Ƿ� ���� ť�� �������ش�
        if (isHost)
        {
            localQueue = new Queue<KeyMessage>();
        }
        else
        {
            localQueue = null;
        }

        // ȣ��Ʈ �������� ������ ��ġ������ ���� ����
        LeaveMatchServer();
        return true;
    }

    private void SetSubHost(SessionId hostSessionId)
    {
        Debug.Log("���� ȣ��Ʈ ���� ���� ����");
        // ���� ���� ȣ��Ʈ �������� �������� ���� ������ Ȯ��
        // �������� ���� SuperGamer ������ GameRecords�� SuperGamer ���� ����
        foreach (var record in gameRecords)
        {
            if (record.Value.m_sessionId.Equals(hostSessionId))
            {
                record.Value.m_isSuperGamer = true;
            }
            else
            {
                record.Value.m_isSuperGamer = false;
            }
        }
        // ���� ȣ��Ʈ �������� Ȯ��
        if (hostSessionId.Equals(Backend.Match.GetMySessionId()))
        {
            isHost = true;
        }
        else
        {
            isHost = false;
        }

        hostSession = hostSessionId;

        Debug.Log("���� ȣ��Ʈ ���� : " + isHost);
        // ȣ��Ʈ �����̸� ���ÿ��� ó���ϴ� ��Ŷ�� �����Ƿ� ���� ť�� �������ش�
        if (isHost)
        {
            localQueue = new Queue<KeyMessage>();
        }
        else
        {
            localQueue = null;
        }

        Debug.Log("���� ȣ��Ʈ ���� �Ϸ�");
    }

    // ��Ī ���� ���� �̺�Ʈ �ڵ鷯
    private void MatchMakingHandler()
    {
        Backend.Match.OnJoinMatchMakingServer += (args) =>
        {
            Debug.Log("OnJoinMatchMakingServer : " + args.ErrInfo);
            // ��Ī ������ �����ϸ� ȣ��
            ProcessAccessMatchMakingServer(args.ErrInfo);
        };
        Backend.Match.OnMatchMakingResponse += (args) =>
        {
            Debug.Log("OnMatchMakingResponse : " + args.ErrInfo + " : " + args.Reason);
            // ��Ī ��û ���� �۾��� ���� ȣ��
            ProcessMatchMakingResponse(args);
        };

        Backend.Match.OnLeaveMatchMakingServer += (args) =>
        {
            // ��Ī �������� ���� ������ �� ȣ��
            Debug.Log("OnLeaveMatchMakingServer : " + args.ErrInfo);
            isConnectMatchServer = false;

            if (args.ErrInfo.Category.Equals(ErrorCode.DisconnectFromRemote) || args.ErrInfo.Category.Equals(ErrorCode.Exception)
                || args.ErrInfo.Category.Equals(ErrorCode.NetworkTimeout))
            {
                //// �������� ������ ���� ���
                //if (LobbyUI.GetInstance())
                //{
                //    LobbyUI.GetInstance().MatchRequestCallback(false);
                //    LobbyUI.GetInstance().CloseRoomUIOnly();
                //    LobbyUI.GetInstance().SetErrorObject("��Ī������ ������ ���������ϴ�.\n\n" + args.ErrInfo.Reason);
                //}
            }
        };

        // ��� �� ����/���� ����
        Backend.Match.OnMatchMakingRoomCreate += (args) =>
        {
            Debug.Log("OnMatchMakingRoomCreate : " + args.ErrInfo + " : " + args.Reason);
            MatchingTest.GetInstance().CreateRoomResult(args.ErrInfo.Equals(ErrorCode.Success) == true);
            //LobbyUI.GetInstance().CreateRoomResult(args.ErrInfo.Equals(ErrorCode.Success) == true);
        };

        // ���濡 ���� ���� �޽���
        Backend.Match.OnMatchMakingRoomJoin += (args) =>
        {
            Debug.Log(string.Format("OnMatchMakingRoomJoin : {0} : {1}", args.ErrInfo, args.Reason));
            if (args.ErrInfo.Equals(ErrorCode.Success))
            {
                Debug.Log("user join in loom : " + args.UserInfo.m_nickName);
                //LobbyUI.GetInstance().InsertReadyUserPrefab(args.UserInfo.m_nickName);
            }
        };

        // ���濡 ���� ������ �ִ� ���� ����Ʈ �޽���
        Backend.Match.OnMatchMakingRoomUserList += (args) =>
        {
            Debug.Log(string.Format("OnMatchMakingRoomUserList : {0} : {1}", args.ErrInfo, args.Reason));
            List<MatchMakingUserInfo> userList = null;
            if (args.ErrInfo.Equals(ErrorCode.Success))
            {
                userList = args.UserInfos;
                Debug.Log("ready room user count : " + userList.Count);
            }
            //LobbyUI.GetInstance().CreateRoomResult(args.ErrInfo.Equals(ErrorCode.Success) == true, userList);
            MatchingTest.GetInstance().CreateRoomResult(args.ErrInfo.Equals(ErrorCode.Success) == true, userList);
        };

        // ���濡 ���� ���� �޽���
        Backend.Match.OnMatchMakingRoomLeave += (args) =>
        {
            Debug.Log(string.Format("OnMatchMakingRoomLeave : {0} : {1}", args.ErrInfo, args.Reason));
            if (args.ErrInfo.Equals(ErrorCode.Success) || args.ErrInfo.Equals(ErrorCode.Match_Making_KickedByOwner))
            {
                Debug.Log("user leave in loom : " + args.UserInfo.m_nickName);
                if (args.UserInfo.m_nickName.Equals(BackEndServerManager.GetInstance().myNickName))
                {
                    if (args.ErrInfo.Equals(ErrorCode.Match_Making_KickedByOwner))
                    {
                        //LobbyUI.GetInstance().SetErrorObject("������߽��ϴ�.");
                    }
                    Debug.Log("�ڱ��ڽ��� �濡�� �������ϴ�.");
                    //LobbyUI.GetInstance().CloseRoomUIOnly();
                    return;
                }
                //LobbyUI.GetInstance().DeleteReadyUserPrefab(args.UserInfo.m_nickName);
            }
        };

        // ������ ���濡�� ���� ���� �ı� �� �޽���
        Backend.Match.OnMatchMakingRoomDestory += (args) =>
        {
            Debug.Log(string.Format("OnMatchMakingRoomDestory : {0} : {1}", args.ErrInfo, args.Reason));
            //LobbyUI.GetInstance().CloseRoomUIOnly();
            //LobbyUI.GetInstance().SetErrorObject("������ ������ �ı��Ͽ����ϴ�.");
        };

        // ���濡 ���� �ʴ� ����/���� ����. (������ �ʴ� ����/������ �ƴ�.)
        Backend.Match.OnMatchMakingRoomInvite += (args) =>
        {
            Debug.Log(string.Format("OnMatchMakingRoomInvite : {0} : {1}", args.ErrInfo, args.Reason));
            //LobbyUI.GetInstance().SetErrorObject(args.ErrInfo.Equals(ErrorCode.Success) == true ? "�ʴ뿡 �����߽��ϴ�." : "�ʴ뿡 �����߽��ϴ�.\n\n" + args.Reason);
        };

        // �ʴ��� ������ �ʴ� ����/���� ����.
        Backend.Match.OnMatchMakingRoomInviteResponse += (args) =>
        {
            Debug.Log(string.Format("OnMatchMakingRoomInviteResponse : {0} : {1}", args.ErrInfo, args.Reason));
        };

        // ���� ���� ��� �޽���
        Backend.Match.OnMatchMakingRoomKick += (args) =>
        {
            Debug.Log(string.Format("OnMatchMakingRoomKick : {0} : {1}", args.ErrInfo, args.Reason));
            if (args.ErrInfo.Equals(ErrorCode.Success) == false)
            {
                //LobbyUI.GetInstance().SetErrorObject(args.Reason);
            }
        };

        // ������ ���� �ʴ������� ���ϵ�
        Backend.Match.OnMatchMakingRoomSomeoneInvited += (args) =>
        {
            Debug.Log(string.Format("OnMatchMakingRoomSomeoneInvited : {0} : {1}", args.ErrInfo, args.Reason));
            var roomId = args.RoomId;
            var roomToken = args.RoomToken;
            Debug.Log(string.Format("room id : {0} / token : {1}", roomId, roomToken));
            MatchMakingUserInfo userInfo = args.InviteUserInfo;
            //LobbyUI.GetInstance().SetSelectObject(userInfo.m_nickName + " ������ �ʴ��߽��ϴ�. �ʴ븦 �����ұ��?",
            //() =>
            //{
            //    Debug.Log("�ʴ븦 �����մϴ�.");
            //    Backend.Match.AcceptInvitation(roomId, roomToken);
            //},
            //() =>
            //{
            //    Debug.Log("�ʴ븦 �����մϴ�.");
            //    Backend.Match.DeclineInvitation(roomId, roomToken);
            //});
        };
    }

    // �ΰ��� ���� ���� �̺�Ʈ �ڵ鷯
    private void GameHandler()
    {
        Backend.Match.OnSessionJoinInServer += (args) =>
        {
            Debug.Log("OnSessionJoinInServer : " + args.ErrInfo);
            // �ΰ��� ������ �����ϸ� ȣ��
            if (args.ErrInfo != ErrorInfo.Success)
            {
                if (isReconnectProcess)
                {
                    if (args.ErrInfo.Reason.Equals("Reconnect Success"))
                    {
                        //������ ����
                        // GameManager.GetInstance().ChangeState(GameManager.GameState.Reconnect); ���ӸŴ������� ���ľ���.
                        Debug.Log("������ ����");
                    }
                    else if (args.ErrInfo.Reason.Equals("Fail To Reconnect"))
                    {
                        Debug.Log("������ ����");
                        JoinMatchServer();
                        isConnectInGameServer = false;
                    }
                }
                return;
            }
            if (isJoinGameRoom)
            {
                return;
            }
            if (inGameRoomToken == string.Empty)
            {
                Debug.LogError("�ΰ��� ���� ���� ���������� �� ��ū�� �����ϴ�.");
                return;
            }
            Debug.Log("�ΰ��� ���� ���� ����");
            isJoinGameRoom = true;
            AccessInGameRoom(inGameRoomToken);
        };

        Backend.Match.OnSessionListInServer += (args) =>
        {
            // ���� ����Ʈ ȣ�� �� ���� ä���� ȣ���
            // ���� ���� ����(��)�� �������� �÷��̾�� �� ������ ���� �� �濡 ���� �ִ� �÷��̾��� ���� ������ ����ִ�.
            // ������ �ʰ� ���� �÷��̾���� ������ OnMatchInGameAccess ���� ���ŵ�
            Debug.Log("OnSessionListInServer : " + args.ErrInfo);

            ProcessMatchInGameSessionList(args);
        };

        Backend.Match.OnMatchInGameAccess += (args) =>
        {
            Debug.Log("OnMatchInGameAccess : " + args.ErrInfo);
            // ������ �ΰ��� �뿡 ������ ������ ȣ�� (�� Ŭ���̾�Ʈ�� �ΰ��� �뿡 ������ ������ ȣ���)
            ProcessMatchInGameAccess(args);
        };

        Backend.Match.OnMatchInGameStart += () =>
        {
            // �������� ���� ���� ��Ŷ�� ������ ȣ��
            GameSetup();
        };

        Backend.Match.OnMatchResult += (args) =>
        {
            Debug.Log("���� ����� ���ε� ��� : " + string.Format("{0} : {1}", args.ErrInfo, args.Reason));
            // �������� ���� ��� ��Ŷ�� ������ ȣ��
            // ����(Ŭ���̾�Ʈ��) ������ ���� ������� ���������� ������Ʈ �Ǿ����� Ȯ��

            if (args.ErrInfo == BackEnd.Tcp.ErrorCode.Success)
            {         
                // GameManager.GetInstance().ChangeState(GameManager.GameState.Result);
            }
            else if (args.ErrInfo == BackEnd.Tcp.ErrorCode.Match_InGame_Timeout)
            {
                Debug.Log("���� ���� ���� : " + args.ErrInfo);
                // LobbyUI.GetInstance().MatchCancelCallback();
            }
            else
            {
                Debug.Log("���� ��� ���ε� ���� : " + args.ErrInfo);
            }
            // ���Ǹ���Ʈ �ʱ�ȭ
            sessionIdList = null;
        };

        Backend.Match.OnMatchRelay += (args) =>
        {
            // �� Ŭ���̾�Ʈ���� ������ ���� �ְ���� ��Ŷ��
            // ������ �ܼ� ��ε�ĳ���ø� ���� (�������� ��� ���굵 �������� ����)

            // ���� ���� ����
            if (PrevGameMessage(args.BinaryUserData) == true)
            {
                // ���� ���� ������ �����Ͽ����� �ٷ� ����
                return;
            }

            if (WorldManager.instance == null)
            {
                // ���� �Ŵ����� �������� ������ �ٷ� ����
                return;
            }

            WorldManager.instance.OnRecieve(args);
        };

        Backend.Match.OnMatchChat += (args) =>
        {
            // ä�ñ���� Ʃ�丮�� �������� �ʾҽ��ϴ�.
        };

        Backend.Match.OnLeaveInGameServer += (args) =>
        {
            Debug.Log("OnLeaveInGameServer : " + args.ErrInfo + " : " + args.Reason);
            if (args.Reason.Equals("Fail To Reconnect"))
            {
                JoinMatchServer();
            }
            isConnectInGameServer = false;
        };

        Backend.Match.OnSessionOnline += (args) =>
        {
            // �ٸ� ������ ������ ���� �� ȣ��
            var nickName = Backend.Match.GetNickNameBySessionId(args.GameRecord.m_sessionId);
            Debug.Log(string.Format("[{0}] �¶��εǾ����ϴ�. - {1} : {2}", nickName, args.ErrInfo, args.Reason));
            ProcessSessionOnline(args.GameRecord.m_sessionId, nickName);
        };

        Backend.Match.OnSessionOffline += (args) =>
        {
            // �ٸ� ���� Ȥ�� �ڱ��ڽ��� ������ �������� �� ȣ��
            Debug.Log(string.Format("[{0}] �������εǾ����ϴ�. - {1} : {2}", args.GameRecord.m_nickname, args.ErrInfo, args.Reason));
            // ���� ������ �ƴϸ� �������� ���μ��� ����
            if (args.ErrInfo != ErrorCode.AuthenticationFailed)
            {
                ProcessSessionOffline(args.GameRecord.m_sessionId);
            }
            else
            {
                // �߸��� ������ �õ� �� ���������� �߻�
            }
        };

        Backend.Match.OnChangeSuperGamer += (args) =>
        {
            Debug.Log(string.Format("���� ���� : {0} / �� ���� : {1}", args.OldSuperUserRecord.m_nickname, args.NewSuperUserRecord.m_nickname));
            // ȣ��Ʈ �缳��
            SetSubHost(args.NewSuperUserRecord.m_sessionId);
            if (isHost)
            {
                // ���� ����ȣ��Ʈ�� �����Ǹ� �ٸ� ��� Ŭ���̾�Ʈ�� ��ũ�޽��� ����
                Invoke("SendGameSyncMessage", 1.0f);
            }
        };
    }

    private void ExceptionHandler()
    {
        // ���ܰ� �߻����� �� ȣ��
        Backend.Match.OnException += (e) =>
        {
            Debug.Log(e);
        };
    }

    void Update()
    {
        if (isConnectInGameServer || isConnectMatchServer)
        {
            Backend.Match.Poll();

            // ȣ��Ʈ�� ��� ���� ť�� ����
            // ť�� �ִ� ��Ŷ�� ���ÿ��� ó��
            if (localQueue != null)
            {
                while (localQueue.Count > 0)
                {
                    var msg = localQueue.Dequeue();
                    WorldManager.instance.OnRecieveForLocal(msg);
                }
            }
        }
    }

    public void GetMyMatchRecord(int index, Action<MatchRecord, bool> func)
    {
        var inDate = BackEndServerManager.GetInstance().myIndate;

        SendQueue.Enqueue(Backend.Match.GetMatchRecord, inDate, matchInfos.matchType, matchInfos.matchModeType, matchInfos.inDate, callback =>
        {
            MatchRecord record = new MatchRecord();
            record.matchTitle = matchInfos.title;
            record.matchType = matchInfos.matchType;
            record.modeType = matchInfos.matchModeType;

            if (!callback.IsSuccess())
            {
                Debug.LogError("��Ī ��� ��ȸ ����\n" + callback);
                func(record, false);
                return;
            }

            if (callback.Rows().Count <= 0)
            {
                Debug.Log("��Ī ����� �������� �ʽ��ϴ�.\n" + callback);
                func(record, true);
                return;
            }
            var data = callback.Rows()[0];
            var win = Convert.ToInt32(data["victory"]["N"].ToString());
            var draw = Convert.ToInt32(data["draw"]["N"].ToString());
            var defeat = Convert.ToInt32(data["defeat"]["N"].ToString());
            var numOfMatch = win + draw + defeat;
            string point = string.Empty;
            if (matchInfos.matchType == MatchType.MMR)
            {
                point = data["mmr"]["N"].ToString();
            }
            else if (matchInfos.matchType == MatchType.Point)
            {
                point = data["point"]["N"].ToString() + " P";
            }
            else
            {
                point = "-";
            }

            record.win = win;
            record.numOfMatch = numOfMatch;
            record.winRate = Math.Round(((float)win / numOfMatch) * 100 * 100) / 100;
            record.score = point;

            func(record, true);
        });
    }

    public void IsMatchGameActivate()
    {
        roomInfo = null;
        isReconnectEnable = false;

        JoinMatchServer();
    }

    public void GetMatchList(Action<bool, string> func)
    {
        // ��Ī ī�� ���� �ʱ�ȭ
        //matchInfos.Clear();

        Backend.Match.GetMatchList(callback =>
        {
            // ��û �����ϴ� ��� ���û
            if (callback.IsSuccess() == false)
            {
                Debug.Log("��Īī�� ����Ʈ �ҷ����� ����\n" + callback);
                Dispatcher.Current.BeginInvoke(() =>
                {
                    GetMatchList(func);
                });
                return;
            }

            foreach (LitJson.JsonData row in callback.Rows())
            {
                MatchInfo matchInfo = new MatchInfo();
                matchInfo.title = row["matchTitle"]["S"].ToString();
                matchInfo.inDate = row["inDate"]["S"].ToString();
                matchInfo.headCount = row["matchHeadCount"]["N"].ToString();
                matchInfo.isSandBoxEnable = row["enable_sandbox"]["BOOL"].ToString().Equals("True") ? true : false;

                foreach (MatchType type in Enum.GetValues(typeof(MatchType)))
                {
                    if (type.ToString().ToLower().Equals(row["matchType"]["S"].ToString().ToLower()))
                    {
                        matchInfo.matchType = type;
                    }
                }

                foreach (MatchModeType type in Enum.GetValues(typeof(MatchModeType)))
                {
                    if (type.ToString().ToLower().Equals(row["matchModeType"]["S"].ToString().ToLower()))
                    {
                        matchInfo.matchModeType = type;
                    }
                }

                //matchInfos.Add(matchInfo);
            }
            Debug.Log("��Īī�� ����Ʈ �ҷ����� ���� : " + matchInfos.title);
            func(true, string.Empty);
        });
    }

    public void AddMsgToLocalQueue(KeyMessage message)
    {
        // ���� ť�� �޽��� �߰�
        if (isHost == false || localQueue == null)
        {
            return;
        }

        localQueue.Enqueue(message);
    }

    public void SetHostSession(SessionId host)
    {
        hostSession = host;
    }

    public int GetTeamInfo(SessionId sessionId)
    {
        return gameRecords[sessionId].m_teamNumber;
    }

    public MatchInfo GetMatchInfo(string indate)
    {
        //var result = matchInfos.FirstOrDefault(x => x.inDate == indate);
        var result=new MatchInfo();
        if (matchInfos.inDate == indate)
        {
            result = matchInfos;
        }
        if (result.Equals(default(MatchInfo)) == true)
        {
            return null;
        }
        return result;
    }
}

public class ServerInfo
{
    public string host;
    public ushort port;
    public string roomToken;
}

public class MatchRecord
{
    public MatchType matchType;
    public MatchModeType modeType;
    public string matchTitle;
    public string score = "-";
    public int win = -1;
    public int numOfMatch = 0;
    public double winRate = 0;
}