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

    // 해상도 조절
    private void InitResolution()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0; //처음에 drop된 값 초기화
        foreach (Resolution item in resolutions)
        {
            Debug.Log(item.width + "x" + item.height + " " + item.refreshRateRatio);
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.ToString(); //해상도값 넣어줌
            resolutionDropdown.options.Add(option);//option 추가

            if (item.width == Screen.width && item.height == Screen.height) //현재 해상도의 너비는 Screen.width 높이는 Screen.height를 사용해서 알 수 있음
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
