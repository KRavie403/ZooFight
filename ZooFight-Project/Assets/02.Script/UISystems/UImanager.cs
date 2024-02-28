using Palmmedia.ReportGenerator.Core;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public static CanvasGroup settingsCanvas = null;

    //public Image hpBarImage;
    //public Image spBarImage;

    //public CharacterProperty characterProperty;

    //private void Start()
    //{
    //    characterProperty.UpdateHp.AddListener(OnHPBarFilled);
    //    characterProperty.UpdateSp.AddListener(OnSPBarFilled);

    //    // �ʱ� ü�� ������ UI ����
    //    OnHPBarFilled(characterProperty.CurHP);

    //    // �ʱ� ���׹̳� ������ UI ����
    //    OnSPBarFilled(characterProperty.CurSP);
    //}

    //void OnHPBarFilled(float newHP)
    //{
    //    if (!Mathf.Approximately(characterProperty.MaxHP, 0.0f))
    //    {
    //        float fillAmount = newHP / characterProperty.MaxHP;

    //        // UI�� Fill Amount ����
    //        hpBarImage.fillAmount = fillAmount;
    //    }
    //}

    //void OnSPBarFilled(float newSP)
    //{
    //    if (!Mathf.Approximately(characterProperty.MaxSP, 0.0f))
    //    {
    //        float fillAmount = newSP / characterProperty.MaxSP;

    //        // UI�� Fill Amount ����
    //        spBarImage.fillAmount = fillAmount;
    //    }
    //}

    private void Awake()
    {
        base.Initialize();
        if (settingsCanvas == null)
        {
            settingsCanvas = Camera.main.GetComponent<CanvasGroup>();
            if(settingsCanvas != null )
            {
                settingsCanvas = Camera.main.gameObject.AddComponent<CanvasGroup>();
            }
        }
    }
}
