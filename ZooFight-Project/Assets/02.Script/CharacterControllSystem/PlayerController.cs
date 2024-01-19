using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;


public class PlayerController : MovementController ,IHitBox
{

    [Range(-1.0f, 1.0f)]
    public float AxisX,AxisY = 0;

    public Transform TargetCamera;

    Vector2 SetNetPos = Vector2.zero;


    bool isUIOpen = false;
    bool IsRunning = false;
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

        
    }

    // Start is called before the first frame update
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        
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
            }
        }
        // �������� �ʾ�����
        else
        {
            myAnim.SetBool("IsMoving", false);
            myAnim.SetBool("IsRunning", false);



        }

    }

    public void BlockGrab(Transform targat)
    {
        targat.GetComponent<BlockObject>().Grab();

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


}
