using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public GameObject Minimap;
    public GameObject MinimapControlBar;

    private bool MinimapCheck = true;

    public void MinimapControl()
    {
        if (MinimapCheck)
        {
            CloseMap();
            MinimapCheck = false;
            MinimapControlBar.transform.Translate(0, -294.3f, 0);
        }
        else
        {
            OpenMap();
            MinimapCheck = true;
            MinimapControlBar.transform.Translate(0, 294.3f, 0);
        }
    }

    public void CloseMap()
    {
        Minimap.SetActive(false);
    }

    public void OpenMap()
    {
        Minimap.SetActive(true);
    }

}
