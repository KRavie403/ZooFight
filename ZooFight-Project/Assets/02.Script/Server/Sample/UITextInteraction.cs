using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;


public class UITextInteraction : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [System.Serializable]
    private class OnClickEvent: UnityEvent { }
    [SerializeField]
    private OnClickEvent onClickEvent;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text=GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Normal;
    }

    public void OnPointerClick(PointerEventData evenData)
    {
        onClickEvent?.Invoke();
    }
}