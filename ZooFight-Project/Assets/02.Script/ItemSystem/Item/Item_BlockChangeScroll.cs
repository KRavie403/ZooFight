using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템명 : 블럭교환스크롤
/// Value 1 
/// Value 2 이펙트 지속시간
/// Value 3 ???
/// </summary>

public class Item_BlockChangeScroll : Items
{
    public Item_BlockChangeScroll(PlayerController player) : base(player)
    {

    }

    [SerializeField]
    EffectPlayer myEffect;
    bool isEffectPlay = false;
    [SerializeField]
    SoundSpeaker mySound;
    bool isSoundPlay = false;   

    //public GameObject myBlockObj;
    public BlockObject myBlock;
    //public GameObject enemyBlockObj;
    public BlockObject enemyBlock;

    Vector3 effectDirBase = new Vector3(-90, 0, 0);


    protected override void Awake()
    {
        base.Awake();

        myCode = ItemCode.BlockChangeScroll;
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();

    }

    public override void ItemUse()
    {
        base.ItemUse();



        // 코루틴의 동작을 본체로 가져오기
        isItemUse = true;

    }
    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();

        float duringTime = 0;
        // 아군 , 상대편 블럭정보 가져오기
        if (myPlayer.myTeam == HitScanner.Team.RedTeam)
        {
            myBlock = Gamemanager.Inst.RedTeamBlock;
            enemyBlock = Gamemanager.Inst.BlueTeamBlock;
        }
        else if (myPlayer.myTeam == HitScanner.Team.BlueTeam)
        {
            myBlock = Gamemanager.Inst.BlueTeamBlock;
            enemyBlock = Gamemanager.Inst.RedTeamBlock;
        }

        // 잡혀있는 상태 해제
        myBlock.isChangeActive = true;
        enemyBlock.isChangeActive = true;
        myBlock.ForceDeGrab();
        enemyBlock.ForceDeGrab();
        // 블럭 교환
        myBlock.ChangeBlockTeam();
        enemyBlock.ChangeBlockTeam();


        // 블럭교환 이펙트 출력준비
        ChangeEffect();
        // 블럭교환 사운드 출력준비


        myBlock.isChangeActive = false;
        enemyBlock.isChangeActive = false;

        myPlayer.ItemUseEnd();

        // 블럭교환 실행
        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;


            if (isItemUse)
            {
                yield return null;
            }
            else
            {
                yield return base.ItemActions();
            }

            yield return null;
        }

        Debug.Log($"{this} ActiveEnd");
        // 아이템 반환
        ReturnItem();
    }

    public override void ReturnItem()
    {
        myBlock = null;
        enemyBlock = null;
        myEffect.gameObject.SetActive(false);
        myEffect.transform.SetParent(transform, false);
        base.ReturnItem();
    }

    public void ChangeEffect()
    {
        myEffect.gameObject.SetActive(true);

        //Transform pos = 
        myEffect.transform.SetParent(null);

        myEffect.EffectPlayAll(0, 0, myBlock.transform,Quaternion.Euler(effectDirBase));
        myEffect.EffectPlayAll(1, 0, enemyBlock.transform,Quaternion.Euler(effectDirBase));
        
        
    }

    public IEnumerator ScrollActive()
    {
        float duringTime = 0;
        // 아군 , 상대편 블럭정보 가져오기
        if(myPlayer.myTeam == HitScanner.Team.RedTeam)
        {
            myBlock = Gamemanager.Inst.RedTeamBlock;
            enemyBlock = Gamemanager.Inst.BlueTeamBlock;
        }
        else if (myPlayer.myTeam == HitScanner.Team.BlueTeam)
        {
            myBlock = Gamemanager.Inst.BlueTeamBlock;
            enemyBlock = Gamemanager.Inst.RedTeamBlock;
        }


        // 블럭교환 이펙트 출력준비

        // 블럭교환 사운드 출력준비


        // 블럭교환 실행
        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;

            yield return null;
        }

    }



}
