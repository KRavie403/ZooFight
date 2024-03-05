using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    List<Resolution> resolutions = new List<Resolution>();

    FullScreenMode screenMode;

    private int resolutionNum;

    void Start()
    {
        InitResolution();
    }

    // �ػ� ����
    private void InitResolution()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0; //ó���� drop�� �� �ʱ�ȭ
        foreach (Resolution item in resolutions)
        {
            Debug.Log(item.width + "x" + item.height + " " + item.refreshRateRatio);
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.ToString(); //�ػ󵵰� �־���
            resolutionDropdown.options.Add(option);//option �߰�

            if (item.width == Screen.width && item.height == Screen.height) //���� �ػ��� �ʺ�� Screen.width ���̴� Screen.height�� ����ؼ� �� �� ����
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        Screen.SetResolution(resolutions[resolutionNum].width,
        resolutions[resolutionNum].height,
        screenMode);
    }

    public void ResolutionDropboxOptionChange(int x)
    {
        resolutionNum = x;
    }
}
