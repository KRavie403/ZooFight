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

    //    // 초기 체력 값으로 UI 설정
    //    OnHPBarFilled(characterProperty.CurHP);

    //    // 초기 스테미너 값으로 UI 설정
    //    OnSPBarFilled(characterProperty.CurSP);
    //}

    //void OnHPBarFilled(float newHP)
    //{
    //    if (!Mathf.Approximately(characterProperty.MaxHP, 0.0f))
    //    {
    //        float fillAmount = newHP / characterProperty.MaxHP;

    //        // UI의 Fill Amount 조절
    //        hpBarImage.fillAmount = fillAmount;
    //    }
    //}

    //void OnSPBarFilled(float newSP)
    //{
    //    if (!Mathf.Approximately(characterProperty.MaxSP, 0.0f))
    //    {
    //        float fillAmount = newSP / characterProperty.MaxSP;

    //        // UI의 Fill Amount 조절
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
