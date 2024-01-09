using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    private static Gamemanager inst;
    public static Gamemanager Inst => inst;

    // 현재 플레이중인 캐릭터의 정보
    public PlayerController currentPlayer;
    public int CharacterID = -1;


    public void Awake()
    {
        if (inst == null)
        {
            inst = FindObjectOfType<Gamemanager>();     // 게임 시작 시 자기 자신을 담음
            DontDestroyOnLoad(this.gameObject);         // 씬 전환에 영향을 받지 않게 만듬

        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
