using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GraphicManager : Singleton<GraphicManager>
{
    [SerializeField] private TMP_Text graphicsCardName;
    [SerializeField] private TMP_Dropdown displayModeDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private Image _overLay;
    [SerializeField] private GameObject _resolution;
    [SerializeField] private RectTransform _videoSettingsRect;

    //List<Resolution> displayModes = new List<Resolution>();
    List<Resolution> resolutions = new List<Resolution>();

    FullScreenMode screenMode = FullScreenMode.FullScreenWindow;
    //private int displayNum;
    private int resolutionNum;

    void Start()
    {
        GraphicsCardInfo();
        InitDisplayMode();
        InitResolution();
        displayModeDropdown.value = 1;  // default = 전체화면
        _resolution.SetActive(true);
        resolutionDropdown.value = resolutions.Count - 1;
        ResolutionDropboxOptionChange();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        DisplayDropboxOptionChange();
        AdjustBrightness();

        if ( SceneManager.sceneCountInBuildSettings == 0)
            ResolutionDropboxOptionChange();
    }

    private void GraphicsCardInfo()
    {
        // 그래픽 카드 정보

        string GCName = SystemInfo.graphicsDeviceName;
        if(graphicsCardName != null)
        {
            graphicsCardName.text = GCName;
        }
        else
        {
#if UNITY_EDITOR || DEBUG
            Debug.LogError("TMP_Text 컴포넌트가 할당되지 않았습니다.");
#endif
            graphicsCardName.text = "그래픽 카드 정보를 불러올 수 없습니다.";
        }
    }


    private void InitDisplayMode()
    {
        // 디스플레이 모드 설정

        displayModeDropdown.options.Clear();

        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData("창 모드"));
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData("전체 화면"));
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData("테두리 없는 창 모드"));

        displayModeDropdown.RefreshShownValue();
    }


    private void InitResolution()
    {
        // 해상도 설정

        resolutions.AddRange(Screen.resolutions);
        //for (int i = 0; i < Screen.resolutions.Length; i++)
        //{
        //    // 60Hz는 왜 안 되지
        //    if (Screen.resolutions[i].refreshRateRatio.value == 90)
        //    {
        //        resolutions.Add(Screen.resolutions[i]);
        //    }
        //}
        //resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0; //처음에 drop된 값 초기화
        foreach (Resolution value in resolutions)
        {
            Debug.Log(value.width + "x" + value.height + " " + value.refreshRateRatio);
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = value.ToString(); //해상도값 넣어줌
            resolutionDropdown.options.Add(option);//option 추가

            //if (value.width == Screen.width && value.height == Screen.height) //현재 해상도의 너비는 Screen.width 높이는 Screen.height를 사용해서 알 수 있음
            //{
            //    resolutionDropdown.value = optionNum;
            //}
            if (optionNum == resolutions.Count - 1) // resolutions 리스트의 마지막 값일 때
            {
                resolutionDropdown.value = optionNum;
            }

            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();
    }

    public void DisplayDropboxOptionChange()
    {
        switch (displayModeDropdown.value)
        {
            case 0:     //  창 모드
                screenMode = FullScreenMode.Windowed;
                _resolution.SetActive(true);
                _videoSettingsRect.sizeDelta = new Vector2(_videoSettingsRect.sizeDelta.x, 400);
                _videoSettingsRect.anchoredPosition = new Vector2(_videoSettingsRect.anchoredPosition.x, 0);
                break;
            case 1:     // 전체 화면
                screenMode = FullScreenMode.FullScreenWindow;
                _resolution.SetActive(false);
                _videoSettingsRect.sizeDelta = new Vector2(_videoSettingsRect.sizeDelta.x, 310);
                _videoSettingsRect.anchoredPosition = new Vector2(_videoSettingsRect.anchoredPosition.x, 20);
                break;
            case 2:     // 테두리 없는 창 모드
                screenMode = FullScreenMode.Windowed;
                _resolution.SetActive(true);
                SetBorderlessWindowedMode();
                _videoSettingsRect.sizeDelta = new Vector2(_videoSettingsRect.sizeDelta.x, 400);
                _videoSettingsRect.anchoredPosition = new Vector2(_videoSettingsRect.anchoredPosition.x, 0);
                break;
            default:
                break;
        }
        Screen.SetResolution(resolutions[resolutionNum].width,
                     resolutions[resolutionNum].height,
                     screenMode);
    }

    int curResolutionDropDownVal = 0;
    public void ResolutionDropboxOptionChange()
    {
        if(curResolutionDropDownVal == resolutionDropdown.value) return;
        if(screenMode == FullScreenMode.FullScreenWindow)
        {
            resolutionNum = resolutionDropdown.value - 1;
            Screen.SetResolution(resolutions[resolutionNum].width,
                                              resolutions[resolutionNum].height,
                                              screenMode);
        }
    }

    public void AdjustBrightness()
    {
        // 화면 밝기 조절
        _overLay.color = new Color(_overLay.color.r, _overLay.color.g, _overLay.color.b, 0.5f - _brightnessSlider.value * 0.5f);
    }


    void SetBorderlessWindowedMode()
    {
        // 테두리 없는 창 모드
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
