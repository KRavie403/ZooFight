using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MovementController, IHitBox
{

    #region ���� ���� ���
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

    // �⺻ ��� ���¸��
    public Dictionary<pState, BaseState> p_States = new Dictionary<pState, BaseState>();
    // ���۾Ƹ� ���� ���
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
        // ���߿��� ��Ŷ ���°��� ������ ���� ��Ŷ�� �ֱ������� �����ϱ�
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

        // �׽�Ʈ
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

        // �������� ���ۿ� ����
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

    // ��Ŷ �����Ϳ� �̵� �Լ� -- ��ǥ ��ǥ�� ���ڷι���
    public void MoveToPos(Vector3 dir)
    {
        // ���� ��ġ �״�� �̵��ϸ� �̵���
        if (dir == transform.position) return;

        Vector3 Axis =  Quaternion.Euler(-transform.rotation.eulerAngles) * dir;

        float Speed = IsRunning ? MoveSpeed * RunSpeedRate : MoveSpeed;

        float curSpeed = Mathf.Sqrt(Axis.x * Axis.x + Axis.z * Axis.z);

        transform.position = dir;
        // 30 ƽ �̻��ǰ����� ���� �̵���Ǿ��� �̵���Ű��
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

    // ������ ������ or ����ֱ�
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

    // ���� �Ұ��ɻ��� = ������ ����� , �����̻�, ���, ����
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

    // ���� �������� ������ ���
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

    // ������ ���޹ޱ�
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

    #region ��������
    [SerializeField] float RecoveryTime = 2.0f;

    // Ÿ�� ���� �߻�
    void IHitBox.HitAction(Component comp)
    {
        //comp.GetComponent<myHitScanner>().MyDamage

        GetDamaged(comp.GetComponent<HitScanner>().MyDamage);
    }

    // ��Ÿ �������� �޴� �Լ�
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

    // �ǵ尡 ������ �ǵ�ȹ��
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

        // �׻� �����ϰ� �ϱ�
        while(true)
        {
            // ���� ����
            if (IsRunning)
            {
                CurSP -= Time.deltaTime * SPRecovery;
            }
            // ���� ����
            else
            {
                CurSP += Time.deltaTime * SPRecovery;
            }
            yield return null;
        }
    }

    // �����̻��� �޴� �Լ�
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

    // ���� ���� �Լ�
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

    // �÷��̾��� ũ�⸦ �Է¹��� ������� ����, ����Ϸ��� �Է� ���� ����� �ִٸ� ó��
    public void PlayerSizeChange(float ChangeRate, UnityAction e = null)
    {
        // 
        gameObject.transform.localScale = Vector3.one * ChangeRate * 0.8f;

        e?.Invoke();
    }


    // �ظ� ���ݽ� �ߵ�
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

    // �ǵ尡 �ı� �� ��� �ߵ��ϴ� �Լ� = �⺻�� true
    public void ShieldCrashed(bool isCrash = true)
    {
        // �ǵ� ���� & ����
        isShield = false;

        // ���ӽð� ����� ���� �Ҹ��
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

    public void SetState(pState state)
    {
        State = state;
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
        myAnim.enabled = false;
        this.enabled = false;
    }

    #endregion



}
