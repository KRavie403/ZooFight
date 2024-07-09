using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiinimapUI : MonoBehaviour
{
    public GameObject minimap;
    public GameObject minimapActivateBtn;
    public GameObject minimapOpenClose;

    [SerializeField] private TextMeshProUGUI mapBtnText;

    private bool _isOpen = true;

    public void ActivateMap()
    {
        if (_isOpen)
        {
            _isOpen = false;
            minimap.SetActive(false);
            minimapOpenClose.transform.Translate(0, -300, 0);
            mapBtnText.text = "+";
        }
        else
        {
            _isOpen = true;
            minimap.SetActive(true);
            minimapOpenClose.transform.Translate(0, 300, 0);
            mapBtnText.text = "-";
        }
    }
}
