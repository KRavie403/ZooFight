using BackEnd.Tcp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 생성된 게임 룸의 정보
/// </summary>
struct GameInfo
{

    public int GameId;

    public PlayerInfo[] PlayerInfo;
    public bool isHost;
    public float PlayTIme;

    public SessionId SessionId;

    /// <summary>
    /// Key = PlayerKey , PlayerInfo = Info
    /// 게임 내 플레이어들의 키값과 컨트롤러를 연결
    /// </summary>
    public Dictionary<int, PlayerInfo> PlayerInfos;

}


// 게임매니저가 두가지의 업데이트를 담당함
// 1.프레임간격의 업데이트
// 2.폴링레이트 간격의 업데이트
public class Gamemanager : MonoBehaviour
{

    private static Gamemanager inst;
    public static Gamemanager Inst => inst;

    #region Scene
    private const string Connect = "0. Connect"; 
    private const string LOBBY = "1. MatchLobby";
    private const string READY = "2. LoadRoom";
    private const string INGAME = "3. InGame";
    #endregion

    #region Actions-Events
    //public static event Action OnRobby = delegate { };
    public static event Action OnGameReady = delegate { };
    //public static event Action OnGameStart = delegate { };
    public static event Action InGame = delegate { };
    public static event Action AfterInGame = delegate { };
    public static event Action OnGameOver = delegate { };
    public static event Action OnGameResult = delegate { };
    public static event Action OnGameReconnect = delegate { };
    public static event Action OnPollingRate = delegate { };

    private string asyncSceneName = string.Empty;
    private IEnumerator InGameUpdateCoroutine;

    private IEnumerator ClientUpdateCoroutine;

    public float PollingRate = 30.0f;

    public enum GameState { Connect, MatchLobby, Ready, Start, InGame, Over, Result, Reconnect };
    private GameState gameState;
    #endregion

    #region 월드매니저로 이관할 정보
    // 현재 플레이중인 캐릭터의 정보
    public PlayerController currentPlayer;
    public int CharacterID = -1;

    public bool IsGameEnd = false;
    public HitScanner.Team VictoryTeam = HitScanner.Team.NotSetting;

    // 팀멤버 <Id,컴포넌트> 조합 
    public int MaxTeamMember = 1;
    public List<int> RedTeamId;
    public Dictionary<int,PlayerController> RedTeamPlayers;
    public List<int> BlueTeamId;
    public Dictionary<int,PlayerController> BlueTeamPlayers;

    public BlockObject RedTeamBlock;
    public BlockObject BlueTeamBlock;

    #endregion
    //public WaitForSeconds BasicPollingRate

    public void Awake()
    {
        if (inst == null)
        {
            inst = FindObjectOfType<Gamemanager>();     // 게임 시작 시 자기 자신을 담음
            Debug.LogError("GameManager 인스턴스가 존재하지 않습니다.");
        }
        if(inst != this)
        {
            Destroy(this.gameObject);
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        DontDestroyOnLoad(this.gameObject);         // 씬 전환에 영향을 받지 않게 만듬
    }



    // Start is called before the first frame update
    void Start()
    {
        
        gameState = GameState.Connect;
        ClientUpdateCoroutine = PollingRateUpdate();
        StartCoroutine(ClientUpdateCoroutine);

        currentPlayer = FindObjectOfType<PlayerController>();
    }

    float Times = 0;
    // Update is called once per frame
    void Update()
    {
        Times += Time.deltaTime;
        
    }
    

    private void FixedUpdate()
    {
        
    }

    public IEnumerator PollingRateUpdate()
    {
        float duringTime = 0;
        WaitForSeconds ws = new WaitForSeconds(1 / PollingRate);
        while (true)
        {
            //duringTime += Time.
            //Debug.Log(Times);
            OnPollingRate();
            yield return ws;
        }
    }


    #region  정보인출

    // 각 팀원 목록
    public Dictionary<int,PlayerController> GetTeam(HitScanner.Team team)
    {
        switch (team)
        {
            case HitScanner.Team.RedTeam:
                return RedTeamPlayers;
            case HitScanner.Team.BlueTeam:
                return BlueTeamPlayers;
            case HitScanner.Team.NotSetting:
                return null;
            default:
                return null;
        }
    }
    public Dictionary<int,PlayerController> GetEnemyTeam(HitScanner.Team team)
    {
        switch (team)
        {
            case HitScanner.Team.RedTeam:
                return BlueTeamPlayers;
            case HitScanner.Team.BlueTeam:
                return RedTeamPlayers;
            case HitScanner.Team.NotSetting:
                    return null;
                default: return null;
        }
    }

    // 승패팀 팀원 id값
    public List<int> GetWinnerTeamId()
    {
        switch (VictoryTeam)
        {
            case HitScanner.Team.RedTeam:
                return RedTeamId;
            case HitScanner.Team.NotSetting:
                return null;
            case HitScanner.Team.BlueTeam:
                return BlueTeamId;
            default:
                return null;
        }
    }
    public List<int> GetLoserTeamId()
    {
        switch (VictoryTeam)
        {
            case HitScanner.Team.RedTeam:
                return BlueTeamId;
            case HitScanner.Team.NotSetting:
                return null;
            case HitScanner.Team.BlueTeam:
                return RedTeamId;
            default:
                return null;
        }
    }
    // 지정팀 팀원 id값
    public List<int> GetTeamId(HitScanner.Team team)
    {
        switch (team)
        {
            case HitScanner.Team.RedTeam:
                return RedTeamId;
            case HitScanner.Team.NotSetting:
                return null;
            case HitScanner.Team.BlueTeam:
                return BlueTeamId;
            default:
                return null;
        }
    }

    public PlayerController PlayerIdToGetPlayerController(int playerId)
    {
        if (BlueTeamPlayers.ContainsKey(playerId))
        {
            return BlueTeamPlayers[playerId];
        }
        else if(RedTeamPlayers.ContainsKey(playerId)) 
        { 
            return RedTeamPlayers[playerId];
        }
        else
        {
            return null;
        }
    }

    // 지정 팀 블럭
    public BlockObject GetTeamBlock(HitScanner.Team team)
    {
        switch (team)
        {
            case HitScanner.Team.RedTeam:
                if(RedTeamBlock != null) return RedTeamBlock;
                else return null;
            case HitScanner.Team.NotSetting:
                return null;
            case HitScanner.Team.BlueTeam:
                if (BlueTeamBlock != null) return BlueTeamBlock;
                else return null;
            case HitScanner.Team.AllTarget:
                return null;
            default:
                return null;
        }
    }
    public BlockObject GetEnemyBlock(HitScanner.Team team)
    {
        switch (team)
        {
            case HitScanner.Team.RedTeam:
                return BlueTeamBlock;
            case HitScanner.Team.NotSetting:
                return null;
            case HitScanner.Team.BlueTeam:
                return RedTeamBlock;
            case HitScanner.Team.AllTarget:
                return null;
            default:
                return null;
        }
    }
    public void AddBlockObj(BlockObject obj)
    {
        switch (obj.myTeam)
        {
            case HitScanner.Team.RedTeam:
                if(RedTeamBlock != null)
                {
                    RedTeamBlock = obj;
                }
                return;
            case HitScanner.Team.NotSetting:
                return;
            case HitScanner.Team.BlueTeam:
                if(BlueTeamBlock != null)
                {
                    BlueTeamBlock = obj;
                }
                return;
            case HitScanner.Team.AllTarget:
                return;
            default:
                return;
        }
    }
    #endregion



}
