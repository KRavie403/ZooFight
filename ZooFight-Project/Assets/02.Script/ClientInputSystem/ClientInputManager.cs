using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInputManager : Singletone<ClientInputManager>
{

    public KeyInputMapper InputMapper;

    public KeySettingDecoder InputSettingDecoder;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool isKeyDown;

    // Update is called once per frame
    void Update()
    {

        InputKeyDown();
        InputKeyStay();
        InputKeyUp();
        MouseAxis();

    }

    public void MouseAxis()
    {
        Gamemanager.Inst.currentPlayer.GetComponentInChildren<CharacterCamera>().AxisX
            = Input.GetAxis("Mouse X");
        Gamemanager.Inst.currentPlayer.GetComponentInChildren<CharacterCamera>().AxisY
            = Input.GetAxis("Mouse Y");
    }

    public void InputKeyDown()
    {

        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Forward]))
        {
            Debug.Log("Down");
        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Backward]))
        {

        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Left]))
        {

        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Right]))
        {

        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Jump]))
        {

        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Attack]))
        {

        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Selectskill]))
        {

        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Usingskill]))
        {

        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Grab]))
        {

        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Menu]))
        {

        }

    }

    public void InputKeyStay()
    {

        if (Input.GetKey(KeySetting.keys[KeyAction.Forward]))
        {
            Debug.Log("Stay");
            Gamemanager.Inst.currentPlayer.AxisY += Time.deltaTime;
        }
        if (Input.GetKey(KeySetting.keys[KeyAction.Backward]))
        {
            Gamemanager.Inst.currentPlayer.AxisY -= Time.deltaTime;
        }
        if (Input.GetKey(KeySetting.keys[KeyAction.Left]))
        {
            Gamemanager.Inst.currentPlayer.AxisX -= Time.deltaTime;

        }
        if (Input.GetKey(KeySetting.keys[KeyAction.Right]))
        {
            Gamemanager.Inst.currentPlayer.AxisX += Time.deltaTime;

        }
        if (Input.GetKey(KeySetting.keys[KeyAction.Jump]))
        {

        }
        if (Input.GetKey(KeySetting.keys[KeyAction.Attack]))
        {

        }
        if (Input.GetKey(KeySetting.keys[KeyAction.Selectskill]))
        {

        }
        if (Input.GetKey(KeySetting.keys[KeyAction.Usingskill]))
        {

        }
        if (Input.GetKey(KeySetting.keys[KeyAction.Grab]))
        {

        }
        if (Input.GetKey(KeySetting.keys[KeyAction.Menu]))
        {

        }

    }

    public void InputKeyUp()
    {

        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Forward]))
        {
            Debug.Log("Up");
            Gamemanager.Inst.currentPlayer.AxisY = 0;
        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Backward]))
        {

            Gamemanager.Inst.currentPlayer.AxisY = 0;
        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Left]))
        {
            Gamemanager.Inst.currentPlayer.AxisX = 0;
        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Right]))
        {
            Gamemanager.Inst.currentPlayer.AxisX = 0;
        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Jump]))
        {

        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Attack]))
        {

        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Selectskill]))
        {

        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Usingskill]))
        {

        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Grab]))
        {

        }
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.Menu]))
        {

        }

    }


}
