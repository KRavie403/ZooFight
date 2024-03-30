using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���ӸŴ����� �ΰ����� ������Ʈ�� �����
// 1.�����Ӱ����� ������Ʈ
// 2.��������Ʈ ������ ������Ʈ
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


    // ���� �÷������� ĳ������ ����
    public PlayerController currentPlayer;
    public int CharacterID = -1;

    public bool IsGameEnd = false;
    public HitScanner.Team VictoryTeam = HitScanner.Team.NotSetting;

    // ����� <Id,������Ʈ> ���� 
    public int MaxTeamMember = 1;
    public List<int> RedTeamId;
    public Dictionary<int,PlayerController> RedTeamPlayers;
    public List<int> BlueTeamId;
    public Dictionary<int,PlayerController> BlueTeamPlayers;


    public void Awake()
    {
        if (inst == null)
        {
            inst = FindObjectOfType<Gamemanager>();     // ���� ���� �� �ڱ� �ڽ��� ����
            Debug.LogError("GameManager �ν��Ͻ��� �������� �ʽ��ϴ�.");
        }


        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        DontDestroyOnLoad(this.gameObject);         // �� ��ȯ�� ������ ���� �ʰ� ����
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
