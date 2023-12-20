using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.IO;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Events;
using System.Linq;

public class KeyInputMapper : MonoBehaviour 
{

    public UnityAction Forward;
    public UnityAction Backward;
    public UnityAction Leftmove;
    public UnityAction Rightmove;


    public UnityAction Jump;
    public UnityAction Attack;
    public UnityAction UsingSkill;
    public UnityAction SelectSkill;

    public string JumpCode = "Jump";


    // Start is called before the first frame update
    void Start()
    {
        JumpCode = "Jump";
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis($"{JumpCode}") > 0)
        {
            Debug.Log("A");
            if(Input.GetAxis($"{JumpCode}") < 1)
            {
                Debug.Log(Input.GetAxis($"{JumpCode}"));
            }
        }
        else if (Input.GetAxis($"{JumpCode}") < 0)
        {
            Debug.Log("B");
            if (Input.GetAxis($"{JumpCode}") > -1)
            {
                Debug.Log(Input.GetAxis($"{JumpCode}"));
            }
        }


        if (Input.anyKeyDown)
        {
            
            

        }


    }

    public void KeycodeToInt(string keycode)
    {
       


    }



}
