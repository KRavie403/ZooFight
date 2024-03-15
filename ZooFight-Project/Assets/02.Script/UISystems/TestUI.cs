using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{

    public TMP_Text[] txt;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(KeyAction)i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < txt.Length; i++)
        //{
        //    txt[i].text = KeySetting.keys[(KeyAction)i].ToString();
        //}
    }

    private void OnGUI()
    {
        Event keyevent = Event.current;

        if (keyevent.isKey)
        {
            if (!KeySetting.keys.ContainsValue(keyevent.keyCode))
            {
                KeySetting.keys[(KeyAction)key] = keyevent.keyCode;
                key = -1;
                RefeshUI();
            }
            else
            {
                //KeyCode tempcode = KeySetting.keys[(KeyAction)key];

                key = -1;
            }
        }
    }
    int key = -1;

    public void RefeshUI()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(KeyAction)i].ToString();
        }
    }


    public void ChangeKey(int num)
    {
        key = num;
    }


}
