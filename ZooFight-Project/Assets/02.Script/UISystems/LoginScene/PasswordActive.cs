using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordActive : MonoBehaviour
{
    public TMP_InputField password;
    public GameObject pwIcon;

    [SerializeField] private Image pwImage;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _selectedSprite;
    private bool _isShown;


    public void ClickPWIcon()
    {
        if (!_isShown && password.contentType == TMP_InputField.ContentType.Password)
        {
            password.contentType = TMP_InputField.ContentType.Standard;
            pwImage.sprite = _selectedSprite;
            _isShown = !_isShown;
        }
        else
        {
            password.contentType = TMP_InputField.ContentType.Password;
            pwImage.sprite = _defaultSprite;
            _isShown = !_isShown;
        }
        password.ForceLabelUpdate();
    }
}
