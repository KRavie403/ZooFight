using BackEnd.Tcp;
using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    static public WorldManager instance;

    const int START_COUNT = 5;

    private SessionId myPlayerIndex = SessionId.None;

    #region �÷��̾�
    public GameObject playerPool;
    public GameObject playerPrefeb;
    public int numOfPlayer = 0;
    public GameObject particle;
    private const int MAXPLAYER = 6;
    public int alivePlayer { get; set; }
    private Dictionary<SessionId, PlayerController> players;
    public GameObject startPointObject;
    private List<Vector4> statringPoints;

    private Stack<SessionId> gameRecord;
    public delegate void PlayerDie(SessionId index);
    public PlayerDie dieEvent;
    #endregion
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InitializeGame();
        var matchInstance = BackEndMatchManager.GetInstance();
        if (matchInstance == null)
        {
            return;
        }
        if (matchInstance.isReconnectProcess)
        {
            //InGameUiManager.GetInstance().SetStartCount(0, false);
            //InGameUiManager.GetInstance().SetReconnectBoard(BackEndServerManager.GetInstance().myNickName);
        }
    }

    /*
	 * �÷��̾� ����
	 * ���� ���� �Լ� ����
	 */
    public bool InitializeGame()
    {
        if (!playerPool)
        {
            Debug.Log("Player Pool Not Exist!");
            return false;
        }
        Debug.Log("���� �ʱ�ȭ ����");
        gameRecord = new Stack<SessionId>();
        //GameManager.OnGameOver += OnGameOver;
        //GameManager.OnGameResult += OnGameResult;
        myPlayerIndex = SessionId.None;
        SetPlayerAttribute();
        OnGameStart();
        return true;
    }

    public void SetPlayerAttribute()
    {
        // ������
        statringPoints = new List<Vector4>();

        int num = startPointObject.transform.childCount;
        for (int i = 0; i < num; ++i)
        {
            var child = startPointObject.transform.GetChild(i);
            Vector4 point = child.transform.position;
            point.w = child.transform.rotation.eulerAngles.y;
            statringPoints.Add(point);
        }

        dieEvent += PlayerDieEvent;
    }

    private void PlayerDieEvent(SessionId index) // ĳ���� ���� �̺�Ʈ�� ���� �̺�Ʈ�� �ٲ���ϳ�? <<
    {
        alivePlayer -= 1;
        //players[index].gameObject.SetActive(false);

        //��ƼŬ
        //var expObject = Instantiate(particle, players[index].GetPosition(), Quaternion.identity);
        //Destroy(expObject, 5);

        //InGameUiManager.GetInstance().SetScoreBoard(alivePlayer);
        gameRecord.Push(index);

        //Debug.Log(string.Format("Player Die : " + players[index].GetNickName()));

        // ȣ��Ʈ�� �ƴϸ� �ٷ� ����
        if (!BackEndMatchManager.GetInstance().IsHost())
        {
            return;
        }

        if (BackEndMatchManager.GetInstance().nowModeType == MatchModeType.TeamOnTeam)
        {
            //if (alivePlayer == 2)
            //{
            //    int remainTeamNumber = -1;
            //    SessionId remainSession = SessionId.None;
            //    foreach (var player in players)
            //    {
            //        if (player.Value.GetIsLive() == false)
            //        {
            //            continue;
            //        }
            //        if (remainTeamNumber == -1)
            //        {
            //            remainTeamNumber = BackEndMatchManager.GetInstance().GetTeamInfo(player.Key);
            //            remainSession = player.Key;
            //        }
            //        else if (remainTeamNumber == BackEndMatchManager.GetInstance().GetTeamInfo(player.Key))
            //        {
            //            //���� �÷��̾���� �������̸� �״�� �������� �޽����� ����
            //            gameRecord.Push(remainSession);
            //            gameRecord.Push(player.Key);
            //            SendGameEndOrder();
            //            return;
            //        }
            //    }
            //}
        }
        // 1�� ���Ϸ� �÷��̾ ������ �ٷ� ���� üũ
        if (alivePlayer <= 1)
        {
            SendGameEndOrder();
        }
    }

    private void SendGameEndOrder()
    {
        // ���� ���� ��ȯ �޽����� ȣ��Ʈ������ ����
        Debug.Log("Make GameResult & Send Game End Order");
        foreach (SessionId session in BackEndMatchManager.GetInstance().sessionIdList)
        {
            if (!gameRecord.Contains(session))
            {
                gameRecord.Push(session);
            }
        }
        GameEndMessage message = new GameEndMessage(gameRecord);
        BackEndMatchManager.GetInstance().SendDataToInGame<GameEndMessage>(message);
    }

    public SessionId GetMyPlayerIndex()
    {
        return myPlayerIndex;
    }

    public void SetPlayerInfo()
    {
        if (BackEndMatchManager.GetInstance().sessionIdList == null)
        {
            // ���� ����ID ����Ʈ�� �������� ������, 0.5�� �� �ٽ� ����
            Invoke("SetPlayerInfo", 0.5f);
            return;
        }
        var gamers = BackEndMatchManager.GetInstance().sessionIdList;
        int size = gamers.Count;
        if (size <= 0)
        {
            Debug.Log("No Player Exist!");
            return;
        }
        if (size > MAXPLAYER)
        {
            Debug.Log("Player Pool Exceed!");
            return;
        }

        players = new Dictionary<SessionId, PlayerController>();
        BackEndMatchManager.GetInstance().SetPlayerSessionList(gamers);

        int index = 0;
        foreach (var sessionId in gamers)
        {
            GameObject player = Instantiate(playerPrefeb, new Vector3(statringPoints[index].x, statringPoints[index].y, statringPoints[index].z), Quaternion.identity, playerPool.transform);
            players.Add(sessionId, player.GetComponent<PlayerController>());

            if (BackEndMatchManager.GetInstance().IsMySessionId(sessionId))
            {
                myPlayerIndex = sessionId;
                //players[sessionId].Initialize(true, myPlayerIndex, BackEndMatchManager.GetInstance().GetNickNameBySessionId(sessionId), statringPoints[index].w);
            }
            else
            {
                //players[sessionId].Initialize(false, sessionId, BackEndMatchManager.GetInstance().GetNickNameBySessionId(sessionId), statringPoints[index].w);
            }
            index += 1;
        }
        Debug.Log("Num Of Current Player : " + size);

        // ���ھ� ���� ����
        alivePlayer = size;
        //InGameUiManager.GetInstance().SetScoreBoard(alivePlayer);

        if (BackEndMatchManager.GetInstance().IsHost())
        {
            StartCoroutine("StartCount");
        }
    }

    public void OnGameStart()
    {
        if (BackEndMatchManager.GetInstance() == null)
        {
            // ī��Ʈ �ٿ� : ����
            //InGameUiManager.GetInstance().SetStartCount(0, false);
            return;
        }
        if (BackEndMatchManager.GetInstance().IsHost())
        {
            Debug.Log("�÷��̾� �������� Ȯ��");

            if (BackEndMatchManager.GetInstance().IsSessionListNull())
            {
                Debug.Log("Player Index Not Exist!");
                // ȣ��Ʈ ���� ���ǵ����Ͱ� ������ ������ �ٷ� �����Ѵ�.
                foreach (var session in BackEndMatchManager.GetInstance().sessionIdList)
                {
                    // ���� ������� ���ÿ� �߰�
                    gameRecord.Push(session);
                }
                GameEndMessage gameEndMessage = new GameEndMessage(gameRecord);
                BackEndMatchManager.GetInstance().SendDataToInGame<GameEndMessage>(gameEndMessage);
                return;
            }
        }
        SetPlayerInfo();
    }

    IEnumerator StartCount()
    {
        StartCountMessage msg = new StartCountMessage(START_COUNT);

        // ī��Ʈ �ٿ�
        for (int i = 0; i < START_COUNT + 1; ++i)
        {
            msg.time = START_COUNT - i;
            BackEndMatchManager.GetInstance().SendDataToInGame<StartCountMessage>(msg);
            yield return new WaitForSeconds(1); //1�� ����
        }

        // ���� ���� �޽����� ����
        GameStartMessage gameStartMessage = new GameStartMessage();
        BackEndMatchManager.GetInstance().SendDataToInGame<GameStartMessage>(gameStartMessage);
    }

    public void PreInGame()
    {
        foreach (var player in players)
        {
           // player.Value.SetMoveVector(Vector3.zero);
        }
    }

    public void OnGameOver()
    {
        Debug.Log("Game End");
        if (BackEndMatchManager.GetInstance() == null)
        {
            Debug.LogError("��ġ�Ŵ����� null �Դϴ�.");
            return;
        }
        BackEndMatchManager.GetInstance().MatchGameOver(gameRecord);
    }

    public void OnGameResult()
    {
        Debug.Log("Game Result");

        //if (GameManager.GetInstance().IsLobbyScene())
        //{
        //    GameManager.GetInstance().ChangeState(GameManager.GameState.MatchLobby);
        //}
    }

    public void OnRecieve(MatchRelayEventArgs args)
    {
        if (args.BinaryUserData == null)
        {
            Debug.LogWarning(string.Format("�� �����Ͱ� ��ε�ĳ���� �Ǿ����ϴ�.\n{0} - {1}", args.From, args.ErrInfo));
            // �����Ͱ� ������ �׳� ����
            return;
        }
        Message msg = DataParser.ReadJsonData<Message>(args.BinaryUserData);
        if (msg == null)
        {
            return;
        }
        if (BackEndMatchManager.GetInstance().IsHost() != true && args.From.SessionId == myPlayerIndex)
        {
            return;
        }
        if (players == null)
        {
            Debug.LogError("Players ������ �������� �ʽ��ϴ�.");
            return;
        }
        switch (msg.type)
        {
            case Protocol.Type.StartCount:
                StartCountMessage startCount = DataParser.ReadJsonData<StartCountMessage>(args.BinaryUserData);
                Debug.Log("wait second : " + (startCount.time));
                //InGameUiManager.GetInstance().SetStartCount(startCount.time);
                break;
            case Protocol.Type.GameStart:
                //InGameUiManager.GetInstance().SetStartCount(0, false);
                //GameManager.GetInstance().ChangeState(GameManager.GameState.InGame);
                break;
            case Protocol.Type.GameEnd:
                GameEndMessage endMessage = DataParser.ReadJsonData<GameEndMessage>(args.BinaryUserData);
                SetGameRecord(endMessage.count, endMessage.sessionList);
                //GameManager.GetInstance().ChangeState(GameManager.GameState.Over);
                break;

            case Protocol.Type.Key:
                KeyMessage keyMessage = DataParser.ReadJsonData<KeyMessage>(args.BinaryUserData);
                ProcessKeyEvent(args.From.SessionId, keyMessage);
                break;
            case Protocol.Type.PlayerMove:
                PlayerMoveMessage moveMessage = DataParser.ReadJsonData<PlayerMoveMessage>(args.BinaryUserData);
                ProcessPlayerData(moveMessage);
                break;
            case Protocol.Type.PlayerAttack:
                PlayerAttackMessage attackMessage = DataParser.ReadJsonData<PlayerAttackMessage>(args.BinaryUserData);
                ProcessPlayerData(attackMessage);
                break;
            case Protocol.Type.PlayerDamaged:
                PlayerDamegedMessage damegedMessage = DataParser.ReadJsonData<PlayerDamegedMessage>(args.BinaryUserData);
                ProcessPlayerData(damegedMessage);
                break;
            case Protocol.Type.PlayerNoMove:
                PlayerNoMoveMessage noMoveMessage = DataParser.ReadJsonData<PlayerNoMoveMessage>(args.BinaryUserData);
                ProcessPlayerData(noMoveMessage);
                break;
            case Protocol.Type.GameSync:
                GameSyncMessage syncMessage = DataParser.ReadJsonData<GameSyncMessage>(args.BinaryUserData);
                ProcessSyncData(syncMessage);
                break;
            default:
                Debug.Log("Unknown protocol type");
                return;
        }
    }

    public void OnRecieveForLocal(KeyMessage keyMessage)
    {
        ProcessKeyEvent(myPlayerIndex, keyMessage);
    }

    public void OnRecieveForLocal(PlayerNoMoveMessage message)
    {
        ProcessPlayerData(message);
    }

    private void ProcessKeyEvent(SessionId index, KeyMessage keyMessage)
    {
        if (BackEndMatchManager.GetInstance().IsHost() == false)
        {
            //ȣ��Ʈ�� ����
            return;
        }
        bool isMove = false;
        bool isAttack = false;
        bool isNoMove = false;

        int keyData = keyMessage.keyData;

        Vector3 moveVector = Vector3.zero;
        Vector3 attackPos = Vector3.zero;
        //Vector3 playerPos = players[index].GetPosition();
        //if ((keyData & KeyEventCode.MOVE) == KeyEventCode.MOVE)
        //{
        //    moveVector = new Vector3(keyMessage.x, keyMessage.y, keyMessage.z);
        //    moveVector = Vector3.Normalize(moveVector);
        //    isMove = true;
        //}
        //if ((keyData & KeyEventCode.ATTACK) == KeyEventCode.ATTACK)
        //{
        //    attackPos = new Vector3(keyMessage.x, keyMessage.y, keyMessage.z);
        //    players[index].Attack(attackPos);
        //    isAttack = true;
        //}

        //if ((keyData & KeyEventCode.NO_MOVE) == KeyEventCode.NO_MOVE)
        //{
        //    isNoMove = true;
        //}

        if (isMove)
        {
            //players[index].SetMoveVector(moveVector);
            //PlayerMoveMessage msg = new PlayerMoveMessage(index, playerPos, moveVector);
            //BackEndMatchManager.GetInstance().SendDataToInGame<PlayerMoveMessage>(msg);
        }
        if (isNoMove)
        {
            //PlayerNoMoveMessage msg = new PlayerNoMoveMessage(index, playerPos);
            //BackEndMatchManager.GetInstance().SendDataToInGame<PlayerNoMoveMessage>(msg);
        }
        if (isAttack)
        {
            PlayerAttackMessage msg = new PlayerAttackMessage(index, attackPos);
            BackEndMatchManager.GetInstance().SendDataToInGame<PlayerAttackMessage>(msg);
        }
    }

    private void ProcessAttackKeyData(SessionId session, Vector3 pos)
    {
        //players[session].Attack(pos);
        PlayerAttackMessage msg = new PlayerAttackMessage(session, pos);
        BackEndMatchManager.GetInstance().SendDataToInGame<PlayerAttackMessage>(msg);
    }

    private void ProcessPlayerData(PlayerMoveMessage data)
    {
        if (BackEndMatchManager.GetInstance().IsHost() == true)
        {
            //ȣ��Ʈ�� ����
            return;
        }
        Vector3 moveVector = new Vector3(data.xDir, data.yDir, data.zDir);
        //moveVector�� ������ ���� & �̵��� �����Ƿ� ���� ���� ����
        //if (!moveVector.Equals(players[data.playerSession].moveVector))
        //{
        //    players[data.playerSession].SetPosition(data.xPos, data.yPos, data.zPos);
        //    players[data.playerSession].SetMoveVector(moveVector);
        //}
    }
    private void ProcessPlayerData(PlayerNoMoveMessage data)
    {
        //players[data.playerSession].SetPosition(data.xPos, data.yPos, data.zPos);
        //players[data.playerSession].SetMoveVector(Vector3.zero);
    }
    private void ProcessPlayerData(PlayerAttackMessage data)
    {
        if (BackEndMatchManager.GetInstance().IsHost() == true)
        {
            //ȣ��Ʈ�� ����
            return;
        }
        //players[data.playerSession].Attack(new Vector3(data.dir_x, data.dir_y, data.dir_z));
    }
    private void ProcessPlayerData(PlayerDamegedMessage data)
    {
        //players[data.playerSession].Damaged();
        //EffectManager.instance.EnableEffect(data.hit_x, data.hit_y, data.hit_z);
    }

    private void ProcessSyncData(GameSyncMessage syncMessage)
    {
        // �÷��̾� ������ ����ȭ
        int index = 0;
        //if (players == null)
        //{
        //    Debug.LogError("Player Poll is null!");
        //    return;
        //}
        //foreach (var player in players)
        //{
        //    var y = player.Value.GetPosition().y;
        //    player.Value.SetPosition(new Vector3(syncMessage.xPos[index], y, syncMessage.zPos[index]));
        //    player.Value.SetHP(syncMessage.hpValue[index]);
        //    index++;
        //}
        BackEndMatchManager.GetInstance().SetHostSession(syncMessage.host);
    }

    public bool IsMyPlayerMove()
    {
        //return players[myPlayerIndex].isMove;
        return true;
    }

    public bool IsMyPlayerRotate()
    {
        //return players[myPlayerIndex].isRotate;
        return true;
    }

    private void SetGameRecord(int count, int[] arr)
    {
        gameRecord = new Stack<SessionId>();
        // ���ÿ� �־�� �ϹǷ� ���� �ڿ��� ���� ���ÿ� push
        for (int i = count - 1; i >= 0; --i)
        {
            gameRecord.Push((SessionId)arr[i]);
        }
    }

    public GameSyncMessage GetNowGameState(SessionId hostSession)
    {
        //int numOfClient = players.Count;

        //float[] xPos = new float[numOfClient];
        //float[] zPos = new float[numOfClient];
        //int[] hp = new int[numOfClient];
        //bool[] online = new bool[numOfClient];
        //int index = 0;
        //foreach (var player in players)
        //{
        //    xPos[index] = player.Value.GetPosition().x;
        //    zPos[index] = player.Value.GetPosition().z;
        //    hp[index] = player.Value.hp;
        //    index++;
        //}
        //return new GameSyncMessage(hostSession, numOfClient, xPos, zPos, hp, online);
        return null;
    }

    public Vector3 GetMyPlayerPos()
    {
        //return players[myPlayerIndex].GetPosition();
        return Vector3.zero;
    }
}
