using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiinimapUI : MonoBehaviour
{
    public GameObject background;
    public GameObject minimap;
    public GameObject minimapActivateBtn;
    public GameObject minimapOpenClose;

    private float originalWidth;
    private float originalHeight;

    [SerializeField] private TextMeshProUGUI mapBtnText;

    private bool _isOpen = true;

    public void ActivateMap()
    {
        if (_isOpen)
        {
            _isOpen = false;
            minimap.SetActive(false);
            minimapOpenClose.transform.position = new Vector3(175, 12.5f, 0);
            background.transform.position = new Vector3(175, -112.5f, 0);
            mapBtnText.text = "+";
        }
        else
        {
            _isOpen = true;
            minimap.SetActive(true);
            minimapOpenClose.transform.position = new Vector3(175, 262.5f, 0);
            background.transform.position = new Vector3(175, 137.5f, 0);
            mapBtnText.text = "-";
        }
    }

    private void Start()
    {
        AdjustMinimapUI();
    }

    void AdjustMinimapUI()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        // MinimapUI의 RectTransform에서 초기 사이즈 값을 가져옴
        RectTransform minimapRect = minimap.GetComponent<RectTransform>();
        originalWidth = minimapRect.sizeDelta.x;
        originalHeight = minimapRect.sizeDelta.y;

        // 특정 비율에 맞춰 MinimapUI의 크기를 조정
        minimapRect.sizeDelta = new Vector2(originalWidth * aspectRatio, originalHeight);
    }
}
