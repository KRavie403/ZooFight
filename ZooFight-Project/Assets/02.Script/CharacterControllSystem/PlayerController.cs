using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MovementController
{


    public float AxisX,AxisY = 0;


    bool isUIOpen = false;

    Vector2 acceleration = Vector2.zero;

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



        if(!isUIOpen)
        {

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

    

}
