using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicManager : Singleton<GraphicManager>
{
    [SerializeField] private TMP_Text graphicsCardName;
    [SerializeField] private TMP_Dropdown displayModeDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider _contrastSlider;
    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private Image _overLay;

    //List<Resolution> displayModes = new List<Resolution>();
    List<Resolution> resolutions = new List<Resolution>();

    FullScreenMode screenMode;
    //private int displayNum;
    private int resolutionNum;

    void Start()
    {
        GraphicsCardInfo();
        //DisplayDropboxOptionChange(1);  // 본 모니터의 해상도와 일치하는지 확인
        InitDisplayMode();
        InitResolution();
    }

    // 그래픽 카드 정보
    private void GraphicsCardInfo()
    {
        string GCName = SystemInfo.graphicsDeviceName;
        if(graphicsCardName != null)
        {
            graphicsCardName.text = GCName;
        }
        else
        {
            Debug.LogError("TMP_Text 컴포넌트가 할당되지 않았습니다.");
            graphicsCardName.text = "그래픽 카드 정보를 불러올 수 없습니다.";
        }
    }

    // 디스플레이 모드 설정
    private void InitDisplayMode()
    {
        displayModeDropdown.options.Clear();

        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData("창 모드"));
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData("전체 화면"));
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData("테두리 없는 창 모드"));

        switch (displayModeDropdown.value)
        {
            case 0:     //  창 모드
                Screen.SetResolution(800, 600, screenMode);
                break;
            case 1:     // 전체 화면
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, screenMode);
                break;
            case 2:     // 테두리 없는 창 모드
                SetBorderlessWindowedMode();
                break;
            default:
                break;
        }
        displayModeDropdown.RefreshShownValue();
    }

    // 해상도 조절
    private void InitResolution()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0; //처음에 drop된 값 초기화
        foreach (Resolution value in resolutions)
        {
            Debug.Log(value.width + "x" + value.height + " " + value.refreshRateRatio);
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = value.ToString(); //해상도값 넣어줌
            resolutionDropdown.options.Add(option);//option 추가

            if (value.width == Screen.width && value.height == Screen.height) //현재 해상도의 너비는 Screen.width 높이는 Screen.height를 사용해서 알 수 있음
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();
    }

    //public void DisplayDropboxOptionChange(int x)
    //{
    //    Debug.Log("DisplayDropboxOptionChange-DisplayNum:" + displayNum);
    //    displayNum = x;
    //}
    public void ResolutionDropboxOptionChange(int x)
    {
        resolutionNum = x;
        Screen.SetResolution(resolutions[resolutionNum].width,
                                          resolutions[resolutionNum].height,
                                          screenMode);
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void DarkOverlay()
    {
        _overLay.color = new Color(0, 0, 0, 1.0f - _brightnessSlider.value);
        //var tempColor = _overLay.color;
        //tempColor.a = _brightnessSlider.value;
        //_overLay.color = tempColor;
    }

    // 테두리 없는 창 모드
    void SetBorderlessWindowedMode()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            var hwnd = GetActiveWindow();
            SetWindowLong(hwnd, GWL_STYLE, (GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU) | WS_POPUP);
        }
    }

    // Windows API 함수들을 선언
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern int GetWindowLong(System.IntPtr hWnd, int nIndex);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern int SetWindowLong(System.IntPtr hWnd, int nIndex, int dwNewLong);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern System.IntPtr GetActiveWindow();

    const int GWL_STYLE = -16;
    const int WS_SYSMENU = 0x00080000;
    const int WS_POPUP = unchecked((int)0x80000000);
}
