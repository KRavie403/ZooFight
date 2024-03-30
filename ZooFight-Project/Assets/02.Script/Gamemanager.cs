using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void Awake()
    {
        if (inst == null)
        {
            inst = FindObjectOfType<Gamemanager>();     // 게임 시작 시 자기 자신을 담음
            Debug.LogError("GameManager 인스턴스가 존재하지 않습니다.");
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
        WaitForSeconds ws = new WaitForSeconds(1 / PollingRate);
        while (true)
        {

            //Debug.Log(Times);
            OnPollingRate();
            yield return ws;
        }
    }


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


}
