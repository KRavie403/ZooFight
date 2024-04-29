using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : ItemBase
{

    protected override void Start()
    {
        base.Start();
        StartCoroutine(ItemActions());
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            StartCoroutine(ItemActions());
        }

    }



    public override IEnumerator ItemActions()
    {
        bool B = false;
        Debug.Log("BB");

        //yield return base.ItemActions();
        // 사용 시작후
        while (!B)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                B = true;
            }

            Debug.Log("CC");
            yield return base.ItemActions();
        }


    }






}
