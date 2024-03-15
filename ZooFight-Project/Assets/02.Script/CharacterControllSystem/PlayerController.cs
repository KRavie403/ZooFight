using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MovementController ,IHitBox
{
    public enum pState
    {
        Create=0,
        Idle,
        Jump,
        SkillReady,
        SKillUse,

        Down,
        Recovery,

        S_Idle,
        S_Jump,
        S_SkillReady,
        S_SKillUse

    }

    protected StateMachine PlayerSM;

    public Dictionary<pState,BaseState> p_States = new Dictionary<pState,BaseState>();

    public bool IsDown => PlayerSM.CurrentState != p_States[pState.Down];


    [Range(-1.0f, 1.0f)]
    public float AxisX,AxisY = 0;

    public CharacterCamera TargetCamera;

    Vector2 SetNetPos = Vector2.zero;

    public GrabPoint grabPoint;

    public Items curItems;

    bool isUIOpen = false;
    bool IsRunning = false;
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
        //Gamemanager.OnPollingRate += () => Debug.Log(transform.position);

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        CharacterMove(AxisX, AxisY,isDenial);

        if(!isUIOpen)
        {

        }
        else
        {

        }
        
    }

    public void SetRunning(bool isRunning)
    {
        IsRunning = isRunning;
    }

    // ĳ���� �̵� �Լ� 
    public void CharacterMove(float AxisX,float AxisY,bool denial)
    {
        Vector3 vector3 = new Vector3(AxisX,0,AxisY);
        vector3 = Vector3.Normalize(vector3);
        Vector2 BlockDir = Vector2.zero;

        
        if (isGrab)
        { 
            Vector3 tmp = new(transform.position.x,0,transform.position.z);
            //Vector3 tmp2 = vector3 * transform.forward;
            Debug.Log(Quaternion.LookRotation(transform.forward, Vector3.up));
            BlockDir = grabPoint.curGrabBlock.DistSelect(vector3,transform.forward);
        }

        // ��������ġ�ϰ��
        if(!denial )
        {
            // ������� �̵��̶� ĳ���� ����������� �����ʿ� - �ذ� �Ϸ�
            //transform.position = transform.position + vector3 * Time.deltaTime * MoveSpeed;
            transform.Translate(vector3 * Time.deltaTime * MoveSpeed, Space.Self);

            //
            //myAnim.GetFloat("MoveAxisX");
            myAnim.SetFloat("MoveAxisX", Mathf.Clamp(AxisX * MotionSpeed, -1.0f, 1.0f));
            myAnim.SetFloat("MoveAxisY", Mathf.Clamp(AxisY * MotionSpeed, -1.0f, 1.0f));

            // �������� ������
            if(myAnim.GetFloat("MoveAxisX") == 0 && myAnim.GetFloat("MoveAxisY") ==0)
            {
                myAnim.SetBool("IsMoving", false);
                myAnim.SetBool("IsRunning", false);
                if (isGrab)
                {
                    grabPoint.curGrabBlock.SetcurDir(Vector3.zero,transform.forward);
                }
            }
            else // �������� ������
            {
                // �ٴ���
                if(IsRunning == false)
                {
                    myAnim.SetBool("IsMoving", true);
                    myAnim.SetBool("IsRunning", false);
                }
                else // �޸�����
                {
                    myAnim.SetBool("IsMoving", false);
                    myAnim.SetBool("IsRunning", true);
                }
                if (isGrab)
                {
                    grabPoint.curGrabBlock.SetcurDir(BlockDir,transform.forward);
                }
            }
        }
        // �������� �ʾ�����
        else
        {
            myAnim.SetBool("IsMoving", false);
            myAnim.SetBool("IsRunning", false);
        }
    }

    public void CharacterMove(Vector3 pos, Vector3 dir, bool denial, UnityAction e = null)
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

    public void Grab()
    {
        BlockGrab(grabPoint.GrabableBlock.transform);
    }
    public void DeGrab()
    {
        isGrab = false;
        grabPoint.curGrabBlock.DeGrab(this);
    }

    public void BlockGrab(Transform targat)
    {
        targat.GetComponent<BlockObject>().Grab(this);
    }

    public void DeBlockGrab(Transform targat)
    {
        targat.GetComponent<BlockObject>().DeGrab(this);
    }

    public void WeaponSwap()
    {

            WeaponRelease();
            WeaponSelect();
        
    }

    public void WeaponRelease()
    {

    }

    public void WeaponSelect()
    {

    }

    void IHitBox.HitAction(Component comp)
    {
        Debug.Log("Damaged");
        //throw new System.NotImplementedException();
    }

    // 
    public void SetHp(float Value,bool isSet)
    {
        if (Value > MaxHP) return;
        if(isSet)
        {
            CurHP += Value;
        }
        else
        {
            CurHP = Value;
        }
    }

    public HitScanner.Team GetEnemyTeam()
    {
        return (HitScanner.Team)((int)myTeam * -1);
    }


}
