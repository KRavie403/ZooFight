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
    public void InsertPlayerInfo()
    {

    }
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

#region 캐릭터 데이터

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

    public BlockObject myBlock;
    public bool isGrab;

    public bool isGameStart;
    public bool isSuperArmor;
    public bool isAbleMove;
    public bool isCrashed;
    public bool isKeyReverse;
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


public struct CharacterStatus
{
    public float curHp;

    public float curStamina;

    public float CurSp;
    public bool isShield;
}

public struct CharacterMovement
{
    // 캐릭터의 목표지점
    public Vector3 dirPos;
    public bool isDynamic;
    // 캐릭터의 목표회전값
    public quaternion dirRot;
}

#endregion

public struct ItemData
{
    public ItemCode itemCode;

    /// <summary>
    /// 아이템의 목적지
    /// Zero = 비 이동형 아이템
    /// </summary>
    public Vector3 dirPos;
    public HitScanner.Team curTeam;
    public PlayerController ItemOwner;

}



    #region 블럭 데이터

    public struct TeamBlockData
    {
        public NormalBlockdata curData;    
        

    }

    public struct NormalBlockdata
    {
        public HitScanner.Team curTeam;

        public Vector3 curPos;

        public bool isMoving;
        public bool isGrab;
        public Vector3 dirPos;
    }

#endregion


#endregion


#region 데이터 클래스버전

public enum DataTypes
{
    CharacterData=0,
    GameData,
    ItemData,
    BlockData,
    PlayerData,
    Types
}

public class PlayerInfo_Class
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
    public void InsertPlayerInfo()
    {

    }
}

/// <summary>
/// 데이터의 형식을 담는 클래스
/// </summary>
public class BasicData
{
    public DataTypes type; 

    public BasicData(DataTypes type)
    {
        this.type = type;
    }
}


public class GameData_Class : BasicData
{
    public PlayerInfo playerInfo;

    public SessionId SessionId;
    public GameData_Class(SessionId SessionId) : base(DataTypes.GameData)
    {
        this.SessionId = SessionId;
    }

    public int GameId;

    public CharacterData[] PlayerInfo;
    public int myPlayerNum;


    public bool isHost;
    public float PlayTIme;



}


public class PlayerData_Class : BasicData
{
    public PlayerInfo myPlayer;

    public PlayerData_Class(PlayerInfo playerInfo) : base(DataTypes.PlayerData)
    {
        this.myPlayer = playerInfo;
    }
}

/// <summary>
/// 게임 내부에서 호스트와 교환할 정보
/// 
/// </summary>
public class CharacterData_Class : BasicData
{
    public CharacterData_Class(PlayerInfo playerinfo): base(DataTypes.CharacterData)
    {
        this.myPlayer = playerinfo;
    }

    public PlayerInfo myPlayer;

    public int ModelId;

    public PlayerController myController;
    // 캐릭터의 상태변화값
    public PlayerController.pState dirState;

    float curHp;
    public float SetHp(float hp)
    {
        curHp = hp;
        return curHp;
    }

    float curStamina;
    public float SetStamina(float stamina) 
    {
        curStamina = stamina;
        return curStamina;
    }

    public float CurSp;
    public bool isShield;

    public BlockObject myBlock;
    public bool isGrab;

    public bool isGameStart;
    public bool isSuperArmor;
    public bool isAbleMove;
    public bool isCrashed;
    public bool isKeyReverse;
    public bool isDenial;

    public ItemCode curItem;

    public bool isMoving;
    public bool isRunning;
    public bool isJump;



    // 캐릭터의 목표지점
    public Vector3 dirPos;
    public bool isStatic;
    // 캐릭터의 목표회전값
    public Vector3 dirRot;

    public void SetDir(Vector3 pos,Vector3 rot,bool isStatic= true)
    {
        this.dirPos = pos;
        this.dirRot = rot;
    }

}

public class ItemData_Class : BasicData
{
    public ItemCode itemCode;

    public ItemData_Class(ItemCode itemCode) : base(DataTypes.ItemData)
    {
        this.itemCode = itemCode;
    }

    /// <summary>
    /// 아이템의 목적지
    /// Zero = 비 이동형 아이템
    /// </summary>
    public Vector3 dirPos;
    public HitScanner.Team curTeam;
    public PlayerController ItemOwner;

}

public class BlockData_Class : BasicData
{
    public HitScanner.Team curTeam;

    public BlockData_Class(HitScanner.Team curTeam) : base(DataTypes.BlockData) 
    {
        this.curTeam = curTeam;
    }

    public Vector3 curPos;

    public bool isMoving;
    public bool isGrab;
    public Vector3 dirPos;
}

#endregion