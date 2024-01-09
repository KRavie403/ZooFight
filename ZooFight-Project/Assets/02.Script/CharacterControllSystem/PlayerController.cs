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

    public void CharacterMove(float AxisX,float AxisY,bool denial)
    {
        Vector3 vector3 = new Vector3(AxisX,0,AxisY);
        vector3 = Vector3.Normalize(vector3);
        if(!denial )
        {
            // 월드기준 이동이라 캐릭터 전방기준으로 변경필요
            //transform.position = transform.position + vector3 * Time.deltaTime * MoveSpeed;
            transform.Translate(vector3 * Time.deltaTime * MoveSpeed, Space.Self);
        }
        else
        {

        }

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
