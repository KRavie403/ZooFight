using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour/*Singleton<GraphicManager>*/
{
    [SerializeField] private TMP_Text graphicsCardName;
    [SerializeField] private TMP_Dropdown displayModeDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider _contrastSlider;
    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private Image _overLay;

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
        displayModeDropdown.value = 1;  // ��üȭ�� default
        resolutionDropdown.value = resolutions.Count - 1;
        ResolutionDropboxOptionChange();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if( SceneManager.sceneCountInBuildSettings == 0)
            ResolutionDropboxOptionChange();
    }

    // �׷��� ī�� ����
    private void GraphicsCardInfo()
    {
        string GCName = SystemInfo.graphicsDeviceName;
        if(graphicsCardName != null)
        {
            graphicsCardName.text = GCName;
        }
        else
        {
            Debug.LogError("TMP_Text ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
            graphicsCardName.text = "�׷��� ī�� ������ �ҷ��� �� �����ϴ�.";
        }
    }

    // ���÷��� ��� ����
    private void InitDisplayMode()
    {
        displayModeDropdown.options.Clear();

        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData("â ���"));
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData("��ü ȭ��"));
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData("�׵θ� ���� â ���"));

        displayModeDropdown.RefreshShownValue();
    }

    // �ػ� ����
    private void InitResolution()
    {
        resolutions.AddRange(Screen.resolutions);
        //for (int i = 0; i < Screen.resolutions.Length; i++)
        //{
        //    // 60Hz�� �� �� ����
        //    if (Screen.resolutions[i].refreshRateRatio.value == 90)
        //    {
        //        resolutions.Add(Screen.resolutions[i]);
        //    }
        //}
        //resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0; //ó���� drop�� �� �ʱ�ȭ
        foreach (Resolution value in resolutions)
        {
            Debug.Log(value.width + "x" + value.height + " " + value.refreshRateRatio);
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = value.ToString(); //�ػ󵵰� �־���
            resolutionDropdown.options.Add(option);//option �߰�

            //if (value.width == Screen.width && value.height == Screen.height) //���� �ػ��� �ʺ�� Screen.width ���̴� Screen.height�� ����ؼ� �� �� ����
            //{
            //    resolutionDropdown.value = optionNum;
            //}
            if (optionNum == resolutions.Count - 1) // resolutions ����Ʈ�� ������ ���̸�
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
            case 0:     //  â ���
                screenMode = FullScreenMode.Windowed;
                break;
            case 1:     // ��ü ȭ��
                screenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:     // �׵θ� ���� â ���
                screenMode = FullScreenMode.Windowed;
                SetBorderlessWindowedMode();
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
    //public void ResolutionDropboxOption()
    //{
    //    curResolutionDropDownVal = resolutionDropdown.value - 1;
    //}

    public void DarkOverlay()
    {
        _overLay.color = new Color(0, 0, 0, 1.0f - _brightnessSlider.value);
        //var tempColor = _overLay.color;
        //tempColor.a = _brightnessSlider.value;
        //_overLay.color = tempColor;
    }

    // �׵θ� ���� â ���
    void SetBorderlessWindowedMode()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            var hwnd = GetActiveWindow();
            SetWindowLong(hwnd, GWL_STYLE, (GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU) | WS_POPUP);
        }
    }

    // Windows API �Լ����� ����
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
