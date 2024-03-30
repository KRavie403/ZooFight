using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;

public class PlayerController : MovementController, IHitBox
{
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
        FuncCount
    }

    [SerializeField]
    private pState State;
    protected StateMachine PlayerSM;


    public Dictionary<pState, BaseState> p_States = new Dictionary<pState, BaseState>();

    public bool IsDown => PlayerSM.CurrentState != p_States[pState.Down];


    [Range(-1.0f, 1.0f)]
    public float AxisX, AxisY = 0;
    public Vector3 DenialPos = Vector3.zero;

    public CharacterCamera TargetCamera;
    public LayerMask groundMask;
    Vector2 SetNetPos = Vector2.zero;

    public GrabPoint grabPoint;

    public Items curItems;

    bool isUIOpen = false;
    [SerializeField]
    bool IsMoving = false;
    [SerializeField]
    bool IsRunning = false;
    [SerializeField]
    bool isJump = false;
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
        p_States.Add(pState.ItemReady, new Charcater_SkillReady(this, PlayerSM));
        p_States.Add(pState.ItemUse, new Character_SKillUse(this, PlayerSM));

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
            if (PlayerSM.CurrentState != p_States[pState.Idle])
            {
                PlayerSM.ChangeState(p_States[pState.Idle]);
                SetIsMoving(false);
            }
        }
        else
        {
            if(PlayerSM.CurrentState != p_States[pState.Move])
                PlayerSM.ChangeState(p_States[pState.Move]);
            SetIsMoving(true);
        }


        //if (AxisY != 0)
        //{
        //    if (PlayerSM.CurrentState != p_States[pState.Move])
        //        PlayerSM.ChangeState(p_States[pState.Move]);
        //}

    }

    public void CurAxisMove()
    {
        CharacterMove(AxisX, AxisY, isDenial);
    }

    // 캐릭터 이동 함수 
    public void CharacterMove(float AxisX,float AxisY,bool denial)
    {
        Vector3 vector3 = new Vector3(AxisX,0,AxisY);
        vector3 = Vector3.Normalize(vector3);
        Vector2 BlockDir = Vector2.zero;

        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, transform.forward);

        Vector3 vector32 = rot * vector3;

        Vector2 dir = new Vector2(vector3.x, vector3.z);

        // 물체를 잡고 움직일때
        if (isGrab)
        { 
            Vector3 tmp = new(transform.position.x,0,transform.position.z);
            //Vector3 tmp2 = vector3 * transform.forward;
            //Debug.Log(Quaternion.LookRotation(transform.forward, Vector3.up));
            BlockDir = grabPoint.curGrabBlock.DistSelect(vector3,transform.forward);
        }

        // 검증된위치일경우
        if(!denial )
        {
            // 월드기준 이동이라 캐릭터 전방기준으로 변경필요 - 해결 완료
            //transform.position = transform.position + vector3 * Time.deltaTime * MoveSpeed;
            transform.Translate(vector3 * Time.deltaTime * MoveSpeed, Space.Self);

            //
            //myAnim.GetFloat("MoveAxisX");
            myAnim.SetFloat("MoveAxisX", Mathf.Clamp(AxisX * MotionSpeed, -1.0f, 1.0f));
            myAnim.SetFloat("MoveAxisY", Mathf.Clamp(AxisY * MotionSpeed, -1.0f, 1.0f));

            // 움직임이 없을때
            if(AxisX == 0 && AxisY == 0)
            //if(myAnim.GetFloat("MoveAxisX") == 0 && myAnim.GetFloat("MoveAxisY") ==0)
            {
                myAnim.SetBool("IsMoving", false);
                myAnim.SetBool("IsRunning", false);
                if (isGrab)
                {
                    grabPoint.curGrabBlock.SetcurDir(Vector3.zero,transform.forward);
                }
            }
            else // 움직임이 있을때
            {
                myAnim.SetBool("IsMoving", true);
                // 뛰는중
                if(IsRunning == false)
                {
                    myAnim.SetBool("IsRunning", false);
                }
                else // 달리는중
                {
                    myAnim.SetBool("IsRunning", true);
                }
                if (isGrab)
                {
                    grabPoint.curGrabBlock.SetcurDir(BlockDir,transform.forward);
                }
            }
        }
        // 검증되지 않았을때
        else
        {
            myAnim.SetBool("IsMoving", false);
            myAnim.SetBool("IsRunning", false);
        }
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

        Vector2 BlockDir = Vector2.zero;

        transform.Translate(MoveSpeed * Time.deltaTime * Direction, Space.Self);
        if (isGrab)
        {
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

    public void CharacterMove2(bool denial , float AxisX = 0,float AxisY = 0)
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

    public IEnumerator BasicMove(Vector3 pos, Vector3 dir, UnityAction e = null)
    {

        while (true)
        {
            yield return null;
        }

        e?.Invoke();
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
        if (isJump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up*JumpHeight, ForceMode.Impulse);
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

    public void ItemSelect()
    {
        PlayerSM.ChangeState(p_States[pState.ItemReady]);
    }



    #endregion

    #region 판정관련
    [SerializeField] float RecoveryTime = 2.0f;

    void IHitBox.HitAction(Component comp)
    {
        Debug.Log("Damaged");
        //comp.GetComponent<HitScanner>().MyDamage
        //throw new System.NotImplementedException();
    }


    void DownAction()
    {
        PlayerSM.ChangeState(p_States[pState.Down]);
    }

    public void CharacterRecovery()
    {
        StartCoroutine(HpRecovery());
    }
    IEnumerator HpRecovery()
    {
        float Times = 0.0f;
        while (Times < RecoveryTime)
        {
            Times += Time.deltaTime;
            SetHp(MaxHP * Times / RecoveryTime, true);
            yield return null;
        }
        PlayerSM.ChangeState(p_States[pState.Idle]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(PlayerSM.CurrentState == p_States[pState.Jump])
        {
            if (collision.gameObject.layer == groundMask)
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

    #endregion

    #region 승패동작

    public void WinAction()
    {
        myAnim.SetTrigger("Win");
    }

    public void LoseAction()
    {
        myAnim.SetTrigger("Lose");
    }

    #endregion



}
