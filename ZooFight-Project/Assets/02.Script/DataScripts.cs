using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 사용자의 아이덴티티
/// 유저의 이름 , ip  등 고유정보를 가짐
/// </summary>
struct PlayerInfo
{

    public string PlayerName;
    public int PlayerId;

    public string PlayerIP;
    public bool isSeverConnect;


}


/// <summary>
/// 게임 외적으로 서버와 교환할 정보
///  
/// </summary>
struct SeverData
{
    public PlayerInfo myPlayer;



}

/// <summary>
/// 게임 내부에서 호스트와 교환할 정보
/// 
/// </summary>
struct CharacterData
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
    public bool IsMoving;
    public bool isSuperArmor;
    public bool IsRunning;
    public bool isJump;
    public bool isAbleMove;
    public bool isCrashed;
    public bool isKeyReverse;
    public bool isGrab;
    public bool isDenial;

    // 달리기 가속 비율
    public float curSpeedRate;

    public ItemCode curItem;


    // 캐릭터의 목표지점
    public Vector3 dirPos;
    public bool isDynamic;

    // 캐릭터의 목표회전값
    public quaternion dirRot;


}
