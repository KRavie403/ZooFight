using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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

    // ĳ���͵��ۿ� �ʿ��� �Լ��� ���
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
    // ĳ���� ��ġ ��������
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
        // ���߿��� ��Ŷ ���°��� ������ ���� ��Ŷ�� �ֱ������� �����ϱ�
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

    #region ���� ����

    // ���� �� ĳ������ �������� ����
    public void StatusPolling()
    {

    }

    public void StatusRenewal()
    {

    }

    // �÷��̾� �������� - ����id , �� , �÷��̾� id 
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

    #region ĳ���� �����Ʈ

    #region ĳ���� �̵�

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
        //CharacterMove(AxisX, AxisY, isDenial);
        CharacterMove(isDenial, AxisX, AxisY);
    }

    public void Move(float AxisX , float AxisY)
    {

        // �Է� �̵����� 0�϶� �ƹ��͵����ϱ�
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

    public void MoveToPos(Vector3 pos,Vector3 dir)
    {

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

    #region ����

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

    #region �����

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

    #region ������

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

    public void ItemUse()
    {

    }

    #endregion

    #region ��������
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
        Debug.Log($"{collision.gameObject.layer} , {groundMask.value}");
        
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

    #region ����������&����

    public HitScanner.Team GetEnemyTeam()
    {
        return (HitScanner.Team)((int)myTeam * -1);
    }
    // isSet True = �ش簪���� ���� , False = �ش簪��ŭ ����
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

    #region ���е���

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
        this.enabled = false;
    }

    #endregion



}
