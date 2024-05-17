using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using BackEnd.Tcp;
using static UnityEngine.InputSystem.InputControlScheme;
using System.Runtime.CompilerServices;

/*
 * ��ġ�Ŵ��� (�ΰ��� ���� ���)
 * BackEndInGame.cs���� ���ǵ� ��ɵ�
 * �ΰ��ӿ� �ʿ��� ������
 * �ΰ��Ӽ��� ���ӷ� �����ϱ� (�ΰ��Ӽ��� ������ BackEndMatch.cs���� ����)
 * �ΰ��Ӽ��� ��������
 * ���ӽ���
 * ���Ӱ���� ����
 * ���Ӱ���� ���� (1:1 / ������ / ����)
 * ������ ������ ��Ŷ ����
 */

public partial class BackEndMatchManager : MonoBehaviour
{
    private bool isSetHost = false;                 // ȣ��Ʈ ���� �����ߴ��� ����

    private MatchGameResult matchGameResult;

    // ���� �α�
    private string FAIL_ACCESS_INGAME = "�ΰ��� ���� ���� : {0} - {1}";
    private string SUCCESS_ACCESS_INGAME = "���� �ΰ��� ���� ���� : {0}";
    private string NUM_INGAME_SESSION = "�ΰ��� �� ���� ���� : {0}";

    // ���� ���� ������ �� ȣ���
    public void OnGameReady()
    {
        if (isSetHost == false)
        {
            // ȣ��Ʈ�� �������� ���� �����̸� ȣ��Ʈ ����
            isSetHost = SetHostSession();
        }
        Debug.Log("ȣ��Ʈ ���� �Ϸ�");

        if (isSandBoxGame == true && IsHost() == true)
        {
            SetAIPlayer();
        }

        if (IsHost() == true)
        {
            // 0.5�� �� ReadyToLoadRoom �Լ� ȣ��
            Invoke("ReadyToLoadRoom", 0.5f);
        }
    }

    private void OnGameReconnect()
    {
        isHost = false;
        localQueue = null;
        Debug.Log("������ ���μ��� ������... ȣ��Ʈ �� ���� ť ���� �Ϸ�");
    }

    // ���� �뿡 ������ ���ǵ��� ����
    // ���� �뿡 �������� �� 1ȸ ���ŵ�
    // ������ ���� ���� 1ȸ ���ŵ�
    private void ProcessMatchInGameSessionList(MatchInGameSessionListEventArgs args)
    {
        sessionIdList = new List<SessionId>();
        gameRecords = new Dictionary<SessionId, MatchUserGameRecord>();

        foreach (var record in args.GameRecords)
        {
            sessionIdList.Add(record.m_sessionId);
            gameRecords.Add(record.m_sessionId, record);
        }
        sessionIdList.Sort();
    }

    // Ŭ���̾�Ʈ ���� ���� �� ���ӿ� ���� ���ϰ�
    // Ŭ���̾�Ʈ�� ���� �뿡 ������ ������ ȣ���
    // ������ ���� ���� ���ŵ��� ����
    private void ProcessMatchInGameAccess(MatchInGameSessionEventArgs args)
    {
        if (isReconnectProcess)
        {
            // ������ ���μ��� �� ���
            // �� �޽����� ���ŵ��� �ʰ�, ���� ���ŵǾ ������
            Debug.Log("������ ���μ��� ������... ������ ���μ��������� ProcessMatchInGameAccess �޽����� ���ŵ��� �ʽ��ϴ�.\n" + args.ErrInfo);
            return;
        }

        Debug.Log(string.Format(SUCCESS_ACCESS_INGAME, args.ErrInfo));

        if (args.ErrInfo != ErrorCode.Success)
        {
            // ���� �� ���� ����
            var errorLog = string.Format(FAIL_ACCESS_INGAME, args.ErrInfo, args.Reason);
            Debug.Log(errorLog);
            LeaveInGameRoom();
            return;
        }

        // ���� �� ���� ����
        // ���ڰ��� ��� ������ Ŭ���̾�Ʈ(����)�� ����ID�� ��Ī ����� ����ִ�.
        // ���� ������ �����Ǿ� ����ֱ� ������ �̹� ������ �����̸� �ǳʶڴ�.

        var record = args.GameRecord;
        Debug.Log(string.Format(string.Format("�ΰ��� ���� ���� ���� [{0}] : {1}", args.GameRecord.m_sessionId, args.GameRecord.m_nickname)));
        if (!sessionIdList.Contains(args.GameRecord.m_sessionId))
        {
            // ���� ����, ���� ��� ���� ����
            sessionIdList.Add(record.m_sessionId);
            gameRecords.Add(record.m_sessionId, record);

            Debug.Log(string.Format(NUM_INGAME_SESSION, sessionIdList.Count));
        }
    }

    // �ΰ��� �� ����
    private void AccessInGameRoom(string roomToken)
    {
        Backend.Match.JoinGameRoom(roomToken);
    }

    // �ΰ��� ���� ���� ����
    public void LeaveInGameRoom()
    {
        isConnectInGameServer = false;
        Backend.Match.LeaveGameServer();
    }

    // �������� ���� ���� ��Ŷ�� ������ �� ȣ��
    // ��� ������ ���� �뿡 ���� �� "�ֿܼ��� ������ �ð�" �Ŀ� ���� ���� ��Ŷ�� �������� �´�
    private void GameSetup()
    {
        Debug.Log("���� ���� �޽��� ����. ���� ���� ����");
        // ���� ���� �޽����� ���� ������ ���� ���·� ����
        //if (GameManager.GetInstance().GetGameState() != GameManager.GameState.Ready)
        //{
        //    isHost = false;
        //    isSetHost = false;
        //    OnGameReady();
        //}
    }


    private void ReadyToLoadRoom()
    {
        if (BackEndMatchManager.GetInstance().isSandBoxGame == true)
        {
            Debug.Log("����ڽ� ��� Ȱ��ȭ. AI ���� �۽�");
            // ����ڽ� ���� ai ���� �۽�
            foreach (var tmp in gameRecords)
            {
                if ((int)tmp.Key > (int)SessionId.Reserve)
                {
                    continue;
                }
                Debug.Log("ai���� �۽� : " + (int)tmp.Key);
                SendDataToInGame(new Protocol.AIPlayerInfo(tmp.Value));
            }
        }

        Debug.Log("1�� �� �� �� ��ȯ �޽��� �۽�");
        Invoke("SendChangeRoomScene", 1f);
    }

    private void SendChangeRoomScene()
    {
        Debug.Log("�� �� ��ȯ �޽��� �۽�");
        SendDataToInGame(new Protocol.LoadRoomSceneMessage());
    }

    private void SendChangeGameScene()
    {
        Debug.Log("���� �� ��ȯ �޽��� �۽�");
        SendDataToInGame(new Protocol.LoadGameSceneMessage());
    }

    // ������ ���� ��� ����
    // �������� �� Ŭ���̾�Ʈ�� ���� ����� ����
    public void MatchGameOver(Stack<SessionId> record)
    {
        if (nowModeType == MatchModeType.OneOnOne)
        {
            matchGameResult = OneOnOneRecord(record);
        }
        else if (nowModeType == MatchModeType.Melee)
        {
            matchGameResult = MeleeRecord(record);
        }
        else if (nowModeType == MatchModeType.TeamOnTeam)
        {
            matchGameResult = TeamRecord(record);
        }
        else
        {
            Debug.LogError("���� ��� ���� ���� - �˼����� ��ġ���Ÿ���Դϴ�.\n" + nowModeType);
            return;
        }

        //MatchResultUI.GetInstance().SetGameResult(matchGameResult);
        RemoveAISessionInGameResult();
        Backend.Match.MatchEnd(matchGameResult);
    }

    private void RemoveAISessionInGameResult()
    {
        string str = string.Empty;
        List<SessionId> aiSession = new List<SessionId>();
        if (matchGameResult.m_winners != null)
        {
            str += "���� : ";
            foreach (var tmp in matchGameResult.m_winners)
            {
                if ((int)tmp < (int)SessionId.Reserve)
                {
                    aiSession.Add(tmp);
                }
                else
                {
                    str += tmp + " : ";
                }
            }
            str += "\n";
            matchGameResult.m_winners.RemoveAll(aiSession.Contains);
        }

        aiSession.Clear();
        if (matchGameResult.m_losers != null)
        {
            str += "���� : ";
            foreach (var tmp in matchGameResult.m_losers)
            {
                if ((int)tmp < (int)SessionId.Reserve)
                {
                    aiSession.Add(tmp);
                }
                else
                {
                    str += tmp + " : ";
                }
            }
            str += "\n";
            matchGameResult.m_losers.RemoveAll(aiSession.Contains);
        }
        Debug.Log(str);
    }


    // 1:1 ���� ���
    private MatchGameResult OneOnOneRecord(Stack<SessionId> record)
    {
        MatchGameResult nowGameResult = new MatchGameResult();

        nowGameResult.m_winners = new List<SessionId>();
        nowGameResult.m_winners.Add(record.Pop());

        nowGameResult.m_losers = new List<SessionId>();
        nowGameResult.m_losers.Add(record.Pop());

        nowGameResult.m_draws = null;

        return nowGameResult;
    }

    // ������ ���� ���
    private MatchGameResult MeleeRecord(Stack<SessionId> record)
    {
        MatchGameResult nowGameResult = new MatchGameResult();
        nowGameResult.m_draws = null;
        nowGameResult.m_losers = null;
        nowGameResult.m_winners = new List<SessionId>();
        int size = record.Count;
        for (int i = 0; i < size; ++i)
        {
            nowGameResult.m_winners.Add(record.Pop());
        }

        return nowGameResult;
    }

    // ���� ���� ���
    private MatchGameResult TeamRecord(Stack<SessionId> record)
    {
        var winnerSession = record.Pop();
        var teamNumber = GetTeamInfo(winnerSession);

        MatchGameResult nowGameResult = new MatchGameResult();
        nowGameResult.m_draws = null;
        nowGameResult.m_losers = new List<SessionId>();
        nowGameResult.m_winners = new List<SessionId>();

        foreach (var user in gameRecords)
        {
            if (user.Value.m_teamNumber == teamNumber)
            {
                nowGameResult.m_winners.Add(user.Key);
            }
            else
            {
                nowGameResult.m_losers.Add(user.Key);
            }
        }

        return nowGameResult;
    }

    // ȣ��Ʈ���� ���� ���Ǹ���Ʈ�� ����
    public void SetPlayerSessionList(List<SessionId> sessions)
    {
        sessionIdList = sessions;
    }

    // ������ ������ ��Ŷ ����
    // ���������� �� ��Ŷ�� �޾� ��� Ŭ���̾�Ʈ(��Ŷ ���� Ŭ���̾�Ʈ ����)�� ��ε�ĳ���� ���ش�.
    public void SendDataToInGame<T>(T msg)
    {
        var byteArray = DataParser.DataToJsonData<T>(msg);
        Backend.Match.SendDataToInGameRoom(byteArray);
    }

    private void ProcessSessionOffline(SessionId sessionId)
    {
        if (hostSession.Equals(sessionId))
        {
            // ȣ��Ʈ ���� ��⸦ ���
            //InGameUiManager.GetInstance().SetHostWaitBoard();
        }
        else
        {
            // ȣ��Ʈ�� �ƴϸ� �ܼ��� UI �� ����.
        }
    }

    private void ProcessSessionOnline(SessionId sessionId, string nickName)
    {
        //InGameUiManager.GetInstance().SetReconnectBoard(nickName);
        // ȣ��Ʈ�� �ƴϸ� �ƹ� �۾� ���� (ȣ��Ʈ�� ����)
        if (isHost)
        {
            // ������ �� Ŭ���̾�Ʈ�� �ΰ��� ���� �����ϱ� �� ���� �������� ���� �� nullptr ���ܰ� �߻��ϹǷ� ����
            // 2������ ��ٸ� �� ���� ���� �޽����� ����
            Invoke("SendGameSyncMessage", 2.0f);
        }
    }

    // Invoke�� �����
    private void SendGameSyncMessage()
    {
        // ���� ���� ��Ȳ (��ġ, hp ���...)
        var message = WorldManager.instance.GetNowGameState(hostSession);
        SendDataToInGame(message);
    }

    private void SetAIPlayer()
    {
        int aiCount = numOfClient - sessionIdList.Count;
        int numOfTeamOne = 0;
        int numOfTeamTwo = 0;

        Debug.Log("AI �÷��̾� ���� : aiCount : " + aiCount);

        if (nowModeType == MatchModeType.TeamOnTeam)
        {
            foreach (var tmp in gameRecords)
            {
                if (tmp.Value.m_teamNumber == 0)
                {
                    numOfTeamOne += 1;
                }
                else
                {
                    numOfTeamTwo += 1;
                }
            }
        }
        int index = 0;
        for (int i = 0; i < aiCount; ++i)
        {
            MatchUserGameRecord aiRecord = new MatchUserGameRecord();
            aiRecord.m_nickname = "AIPlayer" + index;
            aiRecord.m_sessionId = (SessionId)index;
            aiRecord.m_numberOfMatches = 0;
            aiRecord.m_numberOfWin = 0;
            aiRecord.m_numberOfDefeats = 0;
            aiRecord.m_numberOfDraw = 0;
            if (nowMatchType == MatchType.MMR)
            {
                aiRecord.m_mmr = 1000;
            }
            else if (nowMatchType == MatchType.Point)
            {
                aiRecord.m_points = 1000;
            }

            if (nowModeType == MatchModeType.TeamOnTeam)
            {
                if (numOfTeamOne > numOfTeamTwo)
                {
                    aiRecord.m_teamNumber = 1;
                    numOfTeamTwo += 1;
                }
                else
                {
                    aiRecord.m_teamNumber = 0;
                    numOfTeamOne += 1;
                }
            }
            gameRecords.Add((SessionId)index, aiRecord);
            sessionIdList.Add((SessionId)index);
            index += 1;
        }
    }

    public bool PrevGameMessage(byte[] BinaryUserData)
    {
        Protocol.Message msg = DataParser.ReadJsonData<Protocol.Message>(BinaryUserData);
        if (msg == null)
        {
            return false;
        }

        // ���� ���� ���� �۾� ��Ŷ �˻� 
        switch (msg.type)
        {
            case Protocol.Type.AIPlayerInfo:
                Protocol.AIPlayerInfo aiPlayerInfo = DataParser.ReadJsonData<Protocol.AIPlayerInfo>(BinaryUserData);
                ProcessAIDate(aiPlayerInfo);
                return true;
            case Protocol.Type.LoadRoomScene:
                //LobbyUI.GetInstance().ChangeRoomLoadScene();
                if (IsHost() == true)
                {
                    Debug.Log("5�� �� ���� �� ��ȯ �޽��� �۽�");
                    Invoke("SendChangeGameScene", 5f);
                }
                return true;
            case Protocol.Type.LoadGameScene:
                //GameManager.GetInstance().ChangeState(GameManager.GameState.Start);
                return true;
        }
        return false;
    }

    private void ProcessAIDate(Protocol.AIPlayerInfo aIPlayerInfo)
    {
        MatchInGameSessionEventArgs args = new MatchInGameSessionEventArgs();
        args.GameRecord = aIPlayerInfo.GetMatchRecord();

        ProcessMatchInGameAccess(args);
    }
}
