using BackEnd.Tcp;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

#region 게임 

/// <summary>
/// 생성된 게임 룸의 정보
/// </summary>
public struct GameInfo
{

    public int GameId;

    public CharacterData[] PlayerInfo;
    public int myPlayerNum;


    public bool isHost;
    public float PlayTIme;

    public SessionId SessionId;


}


/// <summary>
/// 사용자의 아이덴티티
/// 유저의 이름 , ip  등 고유정보를 가짐
/// </summary>
public struct PlayerInfo
{

    public string PlayerName;
    public int PlayerId;

    public string PlayerIP;
    public bool isSeverConnect;

    /// <summary>
    /// 생성된 게임 내의 플레이어 번호
    /// -1 = 게임밖 , 0 = 호스트 , 1 ~ N = 플레이어 넘버
    /// </summary>
    public int PlayerNum;

}


/// <summary>
/// 게임 외적으로 서버와 교환할 정보
/// 호스트 -> 서버 전송
/// </summary>
public struct SeverData 
{
    //public PlayerInfo myPlayer;

    public PlayerInfo[] GamePlayerList;
  
}




/// <summary>
/// 게임 내부에서 호스트와 교환할 정보
/// 
/// </summary>
public struct CharacterData 
{ 
    public PlayerInfo myPlayer;

    

    public int ModelId;

    public PlayerController myController;
    // 캐릭터의 상태변화값
    public PlayerController.pState dirState;

    public float curHp;

    public float curStamina;

    public float CurSp;
    public bool isShield;


    public bool isGameStart;
    public bool isSuperArmor;
    public bool isAbleMove;
    public bool isCrashed;
    public bool isKeyReverse;
    public bool isGrab;
    public bool isDenial;

    public ItemCode curItem;

    public bool isMoving;
    public bool isRunning;
    public bool isJump;


    
    // 캐릭터의 목표지점
    public Vector3 dirPos;
    public bool isDynamic;
    // 캐릭터의 목표회전값
    public quaternion dirRot;


}

public struct ItemData
{
    public ItemCode itemCode;

    /// <summary>
    /// 아이템의 목적지
    /// Zero = 비 이동형 아이템
    /// </summary>
    public Vector3 dirPos;
    public HitScanner.Team curTeam;

}



#region 블럭 데이터

public struct TeamBlockData
{
    public NormalBlockdata curData;    

    public HitScanner.Team curTeam;

}

public struct NormalBlockdata
{
    public Vector3 curPos;

    public bool isMoving;
    public Vector3 dirPos;
}

#endregion


#endregion
