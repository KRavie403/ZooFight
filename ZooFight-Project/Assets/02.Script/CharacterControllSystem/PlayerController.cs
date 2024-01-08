using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MovementController
{


    KeyInputCode currentSelect = KeyInputCode.None;

    KeyInputCode NowSelect = KeyInputCode.None;

    bool isUIOpen = false;


    public void WeaponSwap()
    {
        if (currentSelect != NowSelect)
        {
            WeaponRelease();
            WeaponSelect();
        }
        
    }

    public void WeaponRelease()
    {

    }

    public void WeaponSelect()
    {

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

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(currentSelect != KeyInputCode.Key1)
                {
                    NowSelect = KeyInputCode.Key1;
                    WeaponSwap();
                }
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                if(currentSelect != KeyInputCode.Key2)
                {
                    NowSelect = KeyInputCode.Key2;
                    WeaponSwap();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if(currentSelect == KeyInputCode.Key3)
                {
                    NowSelect = KeyInputCode.Key3;
                    WeaponSwap();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (currentSelect == KeyInputCode.Key4)
                {
                    NowSelect = KeyInputCode.Key4;
                    WeaponSwap();

                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (currentSelect == KeyInputCode.Key5)
                {
                    NowSelect = KeyInputCode.Key5;
                    WeaponSwap();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                if (currentSelect == KeyInputCode.Key6)
                {
                    NowSelect = KeyInputCode.Key6;
                    WeaponSwap();
                }
            }

        }
        else
        {

        }
        
    }

    public enum KeyInputCode
    {
        None, Key1, Key2, Key3, Key4, Key5, Key6

    }

    

}
