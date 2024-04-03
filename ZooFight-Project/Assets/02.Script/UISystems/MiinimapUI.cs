using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiinimapUI : MonoBehaviour
{
    public GameObject btn;

    private bool isOpen = true;

    public void MapOpen()
    {
        if (!isOpen)
        {
            btn.SetActive(true);
            isOpen = true;
        }
        else
        {
            btn.SetActive(false);
            isOpen = false;
        }
    }
}
