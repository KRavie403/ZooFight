using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MovementController, IHitBox
{

    #region 참조 변수 목록
    public enum pState
    {
        Create = 0,
        Idle,
        Move,
        Jump,
        ItemReady,
        ItemUse,

        Down,
        Recovery,

        S_Idle,
        S_Move,
        S_Jump,
        S_ItemReady,
        S_ItemUse,

        GameReady,
        GameEnd,
        StateCount
    }

    // 캐릭터동작에 필요한 함수들 목록
    public enum pFunc
    {
        Move,
        Jump,
        ItemReady,
        ItemUse,
        ItemRelease,
        BasicAttack,
        EnterSuperArmor,

        AllStop,
        FuncCount
    }

    [SerializeField]
    private pState State;
    protected StateMachine PlayerSM;

    // 기본 모든 상태목록
    public Dictionary<pState, BaseState> p_States = new Dictionary<pState, BaseState>();
    // 슈퍼아머 상태 목록
    public Dictionary<pState, BaseState> S_States = new Dictionary<pState, BaseState>();

    public bool IsDown => PlayerSM.CurrentState != p_States[pState.Down];


    [Range(-1.0f, 1.0f)]
    public float AxisX, AxisY = 0;
    public Vector3 DenialPos = Vector3.zero;

    public CharacterCamera TargetCamera;
    public LayerMask groundMask;
    Vector2 SetNetPos = Vector2.zero;

    public GrabPoint grabPoint;
    public Transform AttackPoint;
    public Transform ItemPoint;

    public Items curItems;

    
    [SerializeField]
    bool IsMoving = false;
    bool isSuperArmor = false;
    bool isUIOpen = false;
    [SerializeField]
    bool IsRunning = false;
    [SerializeField]
    bool isJump = false;
    public bool isKeyReverse = false;
    public bool isGrab = false;
    // 캐릭터 위치 검증여부
    bool isDenial = false;
    Vector2 acceleration = Vector2.zero;

    Component IHitBox.myHitBox
    {
        get => this as Component;
    }

    HitScanner.Team IHitBox.Team
    {
        get => myTeam;
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        AxisX = 0;
        AxisY = 0;
        TargetCamera = GetComponentInChildren<CharacterCamera>();
        grabPoint = GetComponentInChildren<GrabPoint>();

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        // 나중에는 패킷 묶는곳에 던지고 묶인 패킷을 주기적으로 전송하기
        //Gamemanager.OnPollingRate += () => ;
        PlayerSM = new StateMachine();

        p_States.Add(pState.Create, new Character_Create(this, PlayerSM));

        p_States.Add(pState.Idle, new Character_Idle(this, PlayerSM));
        p_States.Add(pState.Move, new Character_Move(this, PlayerSM));
        p_States.Add(pState.Jump, new Character_Jump(this, PlayerSM));

        p_States.Add(pState.ItemReady, new Characater_ItemReady(this, PlayerSM));
        p_States.Add(pState.ItemUse, new Character_ItemUse(this, PlayerSM));

        p_States.Add(pState.Down, new Character_Down(this, PlayerSM));
        p_States.Add(pState.Recovery, new Character_Recovery(this, PlayerSM));


        p_States.Add(pState.S_Idle,new Character_SIdle(this, PlayerSM));
        p_States.Add(pState.S_Move,new Character_SMove(this, PlayerSM));
        p_States.Add(pState.S_Jump,new Character_Jump(this, PlayerSM));

        p_States.Add(pState.S_ItemReady, new Character_SItemReady(this, PlayerSM));
        p_States.Add(pState.S_ItemUse,new Character_SItemUse(this, PlayerSM));

        // 테스트
        S_States.Add(pState.S_Idle, new Character_SIdle(this, PlayerSM));
        S_States.Add(pState.S_Move, new Character_SMove(this, PlayerSM));
        S_States.Add(pState.S_Jump, new Character_Jump(this, PlayerSM));

        S_States.Add(pState.S_ItemReady, new Character_SItemReady(this, PlayerSM));
        S_States.Add(pState.S_ItemUse, new Character_SItemUse(this, PlayerSM));


        //CharacterInitalize(myTeam, SessionId, CharacterID);

        StateInitiate();

        PlayerSM.Initalize(p_States[pState.Create]);

        CharacterInitate();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //MoveStateCheck();
        PlayerSM.CurrentState.LogicUpdate();
        //CharacterMove(AxisX, AxisY,isDenial);

    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        //PlayerSM.CurrentState.PhysicsUpdate();


    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    public void StateInitiate()
    {
        for (int i = 0; i < (int)pState.StateCount; i++)
        {
            if (p_States.ContainsKey((pState)i))
            {
                p_States[(pState)i].Initate();
            }
        }
    }

    #region 정보 폴링


    // 현재 이 캐릭터의 상태정보 전송
    public void StatusPolling()
    {

    }

    public void StatusRenewal()
    {

    }

    // 플레이어 정보주입 - 세션id , 팀 , 플레이어 id 
    public void CharacterInitalize(HitScanner.Team PlayerTeam,int SessionID,int PlayerID)
    {
        myTeam = PlayerTeam;
        SessionId = SessionID;
        CharacterID = PlayerID;
        Gamemanager.Inst.GetTeam(PlayerTeam).Add(CharacterID, this);
        Gamemanager.Inst.GetTeamId(PlayerTeam).Add(CharacterID);
    }

    public void CharacterInitate()
    {
        PlayerSM.ChangeState(p_States[pState.Idle]);
    }
    public void StateChanged()
    {
        //switch (switch_on)
        //{
        //    default:
        //}
    }

    #endregion

    #region 캐릭터 무브먼트

    #region 캐릭터 이동

    public void SetIsMoving(bool isMoving)
    {
        IsMoving = isMoving;
    }
    public bool GetIsmoving()
    {
        return IsMoving;
    }

    public void SetRunning(bool isRunning)
    {
        IsRunning = isRunning;
    }
    public bool GetIsRunning()
    {
        return IsRunning;
    }

    public void MoveStateCheck()
    {
        
        if(AxisX == 0 && AxisY == 0)
        {
            if (isShield)
            {
                //if(PlayerSM.CurrentState == p_States[p])
                if(PlayerSM.CurrentState != p_States[pState.S_Idle])
                {
                    PlayerSM.ChangeState(p_States[pState.S_Idle]);
                    SetIsMoving(false);
                }
            }
            else
            {
                if (PlayerSM.CurrentState != p_States[pState.Idle])
                {
                    PlayerSM.ChangeState(p_States[pState.Idle]);
                    SetIsMoving(false);
                }
            }
        }
        else
        {
            if (isShield)
            {
                if (PlayerSM.CurrentState != p_States[pState.S_Move])
                    PlayerSM.ChangeState(p_States[pState.S_Move]);
                SetIsMoving(true);
            }
            else
            {
                if(PlayerSM.CurrentState != p_States[pState.Move])
                    PlayerSM.ChangeState(p_States[pState.Move]);
                SetIsMoving(true);
            }
        }


        //if (AxisY != 0)
        //{
        //    if (PlayerSM.CurrentState != p_States[pState.Move])
        //        PlayerSM.ChangeState(p_States[pState.Move]);
        //}

    }

    public void CurAxisMove()
    {
        //CharacterMove(AxisX, AxisY, isDenial);
        CharacterMove(isDenial, AxisX, AxisY);
    }

    public void Move(float AxisX , float AxisY)
    {


        // 입력 이동값이 0일때 아무것도안하기
        if (AxisX == 0 && AxisY == 0)
        {
            myAnim.SetBool("IsMoving", false);
            myAnim.SetBool("IsRunning", false); 
            myAnim.SetFloat("MoveAxisX", 0);
            myAnim.SetFloat("MoveAxisY", 0);
            return;
        }

        Vector3 Direction = Vector3.Normalize(new Vector3 (AxisX,0,AxisY));

        float Speed = IsRunning ? MoveSpeed * RunSpeedRate : MoveSpeed;

        // 프로토콜 전송용 벡터
        Vector3 Dir = MakeDir(AxisX, AxisY);
        //transform.Translate(MoveSpeed * Time.deltaTime * Direction, Space.Self);
        transform.position += MakeDir(AxisX, AxisY) * Speed * Time.deltaTime;

        if (isGrab)
        {
            Vector2 BlockDir = Vector2.zero;
            BlockDir = grabPoint.curGrabBlock.DistSelect(Direction,transform.forward);
            grabPoint.curGrabBlock.SetcurDir(BlockDir, transform.forward);
        }

        myAnim.SetFloat("MoveAxisX", Mathf.Clamp(AxisX * MotionSpeed, -1.0f, 1.0f));
        myAnim.SetFloat("MoveAxisY", Mathf.Clamp(AxisY * MotionSpeed, -1.0f, 1.0f));

        myAnim.SetBool("IsMoving", true);
        if(IsRunning)
        {
            myAnim.SetBool("IsRunning", true);
        }
        else
        {
            myAnim.SetBool("IsRunning", false);
        }

    }
    public Vector3 MakeDir(float AxisX,float AxisY)
    {
        Vector3 dir = Vector3.Normalize(new Vector3(AxisX,0,AxisY));

        return transform.rotation * dir;
    }

    public void CharacterMove(bool denial , float AxisX = 0,float AxisY = 0)
    {
        if (!denial)
        {
            Move(AxisX, AxisY);
        }
        else
        {
            transform.position = DenialPos;
            myAnim.SetBool("IsMoving", false);
            myAnim.SetBool("IsRunning", false);
            DenialPos = Vector3.zero;
        }
    }

    public void Slide(Vector3 dir, float dist, float Speed, UnityAction e = null)
    {
        
        StartCoroutine(CharacterSlide(dir, dist, Speed, e));    
    }

    public IEnumerator CharacterSlide(Vector3 dir, float Dist,float Speed, UnityAction e = null)
    {
        float duringTime = 0.0f;

        myAnim.SetBool("IsSlide", true);
        myAnim.SetTrigger("Sliding");
        while (duringTime < Dist/Speed)
        {
            duringTime += Time.deltaTime;
            CharacterMove(false, dir.x, dir.z);
            
            yield return null;
        }

        myAnim.SetBool("isSlide", false);

        e?.Invoke();
    }

    // 패킷 데이터용 이동 함수 -- 목표 좌표를 인자로받음
    public void MoveToPos(Vector3 dir)
    {
        // 현재 위치 그대로 이동하면 미동작
        if (dir == transform.position) return;

        Vector3 Axis =  Quaternion.Euler(-transform.rotation.eulerAngles) * dir;

        float Speed = IsRunning ? MoveSpeed * RunSpeedRate : MoveSpeed;

        float curSpeed = Mathf.Sqrt(Axis.x * Axis.x + Axis.z * Axis.z);

        transform.position = dir;
        // 30 틱 이상의격차가 나면 이동모션없이 이동시키기
        if (curSpeed > 30 * Speed / Gamemanager.Inst.PollingRate) return;

        myAnim.SetFloat("MoveAxisX", Mathf.Clamp(Axis.x * MotionSpeed, -1.0f, 1.0f));
        myAnim.SetFloat("MoveAxisY", Mathf.Clamp(Axis.y * MotionSpeed, -1.0f, 1.0f));
        myAnim.SetBool("IsMoving", true);
        if (IsRunning)
        {
            myAnim.SetBool("IsRunning", true);
        }
        else
        {
            myAnim.SetBool("IsRunning", false);
        }
    }



    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
 

    #endregion

    #region 점프

    public void Jump()
    {
        if(PlayerSM.CurrentState != p_States[pState.Jump])
            PlayerSM.ChangeState(p_States[pState.Jump]);
    }

    public void CharacterJump()
    {
        if (!isJump)
        {

            GetComponent<Rigidbody>().AddForce(Vector3.up*JumpHeight, ForceMode.Impulse);
            isJump = true;
        }
    }
    public void SetisJump(bool IsJump)
    {
        isJump = IsJump;
    }
    public bool GetisJump()
    {
        return isJump;
    }


    #endregion

    #region 블럭잡기

    public void Grab()
    {
        if(grabPoint.GrabableBlock != null)
        {
            BlockGrab(grabPoint.GrabableBlock.transform);
        }
    }
    public void DeGrab()
    {
        if (grabPoint.curGrabBlock != null)
        {
            isGrab = false;
            grabPoint.curGrabBlock.DeGrab(this);
        }
    }

    public void BlockGrab(Transform targat)
    {
        targat.GetComponent<BlockObject>().Grab(this);
    }

    public void BlockFree(Transform targat)
    {
        targat.GetComponent<BlockObject>().DeGrab(this);
    }

    #endregion

    #endregion

    #region 아이템

    // 아이템 꺼내기 or 집어넣기
    public void ItemRelease()
    {

        if(PlayerSM.CurrentState == p_States[pState.ItemUse])
        {
            PlayerSM.ChangeState(p_States[pState.Idle]);
            MoveStateCheck();
        }
        else if (PlayerSM.CurrentState == p_States[pState.ItemReady])
        {
            MoveStateCheck();
        }

    }

    // 동작 불가능상태 = 아이템 사용중 , 상태이상, 기상, 점프
    public void ItemReady()
    {
        if (curItems == null) return;
        if (PlayerSM.CurrentState == p_States[pState.ItemUse]) return;
        if (PlayerSM.CurrentState == p_States[pState.Down]) return;
        if (PlayerSM.CurrentState == p_States[pState.Recovery]) return;
        if (PlayerSM.CurrentState == p_States[pState.Jump]) return;
        if (PlayerSM.CurrentState == p_States[pState.S_Jump]) return;

        if (PlayerSM.CurrentState == p_States[pState.ItemReady])
        {
            ItemRelease();
        }
        else
        {
            PlayerSM.ChangeState(p_States[pState.ItemReady]);
        }
    }

    // 현재 보유중인 아이템 사용
    public void ItemUse()
    {
        if(curItems != null)
        {
            if(PlayerSM.CurrentState == p_States[pState.ItemReady])
            {
                PlayerSM.ChangeState(p_States[pState.ItemUse]);

                //curItems.ItemUse();
            }
        }
    }
    public void ItemUseEnd()
    {
        if(PlayerSM.CurrentState == p_States[pState.ItemUse])
        {
            PlayerSM.ChangeState(p_States[pState.Idle]);
            MoveStateCheck();
        }
    }

    // 아이템 지급받기
    public void GetItem()
    {

        Items items = ItemSystem.Inst.GiveItem(this, ItemSystem.Inst.RandomItemSelect());
        if( items == null) return;  
        if(curItems == null)
        {
            curItems = items;
        }
        else
        {
            items.ReturnItem();
        }
    }

    #endregion

    #region 판정관련
    [SerializeField] float RecoveryTime = 2.0f;

    // 타격 판정 발생
    void IHitBox.HitAction(Component comp)
    {
        //comp.GetComponent<myHitScanner>().MyDamage

        GetDamaged(comp.GetComponent<HitScanner>().MyDamage);
    }

    // 단타 데미지를 받는 함수
    public void GetDamaged(float Damage)
    {
        Debug.Log("Damaged");
        if(isShield)
        {
            CurShield -= Damage;
        }
        else
        {
            CurHP -= Damage;
        }

    }
    

    public void GetDotDamaged(float Damage,float time)
    {
        StartCoroutine(DotDamaged(Damage,time));

    }

    // 실드가 없을때 실드획득
    public void GetShield(float ShieldValue)
    {
        if (ShieldValue < 0) return;
        if( ShieldValue > MaxShield) 
        { 

        }
        else
        {
            isShield = true;
            CurShield = ShieldValue;
        }

    }

    public IEnumerator StaminaWork()
    {

        // 항상 동작하게 하기
        while(true)
        {
            // 스테 감소
            if (IsRunning)
            {
                CurSP -= Time.deltaTime * SPRecovery;
            }
            // 스테 증가
            else
            {
                CurSP += Time.deltaTime * SPRecovery;
            }
            yield return null;
        }
    }

    // 상태이상을 받는 함수
    public void GetCrowdControl(StatusCode code, float Time, float Power=0)
    {

        switch (code)
        {
            case StatusCode.Normal:
                break;
            case StatusCode.Slow:
                break;
            case StatusCode.Blind:
                break;
            case StatusCode.Bind:
                break;
            case StatusCode.Stun:
                break;
            case StatusCode.AirBone:
                break;
            default:
                break;
        }
    }

    public void DownAction()
    {
        PlayerSM.ChangeState(p_States[pState.Down]);
    }

    public void CharacterRecovery()
    {
        StartCoroutine(HpRecovery());
    }

    IEnumerator DotDamaged(float Damage,float time,UnityAction e = null)
    {
        float duringTime = 0;
        while (duringTime < time)
        {
            duringTime += Time.deltaTime;
            GetDamaged(Damage / (time * Time.deltaTime));
            yield return null;
        }
        e?.Invoke();
    }

    IEnumerator HpRecovery()
    {
        float Times = 0.0f;
        myAnim.SetTrigger("Recovery");
        while (Times < RecoveryTime)
        {
            Times += Time.deltaTime;
            SetHp(MaxHP * Times / RecoveryTime, true);
            yield return null;
        }
        PlayerSM.ChangeState(p_States[pState.Idle]);
    }

    // 공격 판정 함수
    public void PlayerAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.3f, groundMask);

        foreach (Collider collider in colliders)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player != null) 
            {
                //if(player.myTeam == )
            }
        }
    }

    // 플레이어의 크기를 입력받은 사이즈로 변경, 변경완료후 입력 받은 명령이 있다면 처리
    public void PlayerSizeChange(float ChangeRate, UnityAction e = null)
    {
        // 
        gameObject.transform.localScale = Vector3.one * ChangeRate * 0.8f;

        e?.Invoke();
    }


    // 해머 공격시 발동
    public void HammerSmash(float Time)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.3f, groundMask);

        foreach (Collider collider in colliders)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GetCrowdControl(StatusCode.Stun, Time);
            }
        }
        myAnim.SetTrigger("HammerSmash");


    }

    // 실드가 파괴 될 경우 발동하는 함수 = 기본값 true
    public void ShieldCrashed(bool isCrash = true)
    {
        // 실드 종료 & 스턴
        isShield = false;

        // 지속시간 종료로 인한 소멸시
        if (!isCrash)
        {
            CurShield = 0.0f;
        }

        DownAction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log($"{collision.gameObject.layer} , {groundMask.value}");
        
        if ( (1 << collision.gameObject.layer) == groundMask)
        {
            Debug.Log("Ground");
            if(PlayerSM.CurrentState == p_States[pState.Jump])
            {
                MoveStateCheck();
                isJump = false;
            }
        }
    }



    private void OnCollisionExit(Collision collision)
    {
        //
        //if (PlayerSM.CurrentState == p_States[pState.Jump])
        //{
        //    if (collision.gameObject.layer == groundMask)
        //    {
        //        //PlayerSM.ChangeState(p_States[pState.Jump]);
        //        isJump = true;
        //    }
        //}

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == groundMask)
        {
            isJump = false;
        }
        if (PlayerSM.CurrentState == p_States[pState.Jump])
        {
        }
    }

    #endregion

    #region 데이터인출&수정

    public HitScanner.Team GetEnemyTeam()
    {
        return (HitScanner.Team)((int)myTeam * -1);
    }
    // isSet True = 해당값으로 설정 , False = 해당값만큼 증가
    public void SetHp(float Value,bool isSet)
    {
        if (Value > MaxHP)
        {
            CurHP = MaxHP;
            return;
        }

        CurHP = isSet ? Value : CurHP + Value ;
        
    }

    public void SetState(pState state)
    {
        State = state;
    }

    #endregion

    #region 승패동작

    public void WinAction()
    {
        myAnim.SetTrigger("Win");
        myAnim.SetBool("IsGameEnd",true);
    }

    public void LoseAction()
    {
        myAnim.SetTrigger("Lose");
        myAnim.SetBool("IsGameEnd", true);
    }

    public void ActionAllStop()
    {
        grabPoint.curGrabBlock.DeGrab(this);
        myAnim.enabled = false;
        this.enabled = false;
    }

    #endregion



}
