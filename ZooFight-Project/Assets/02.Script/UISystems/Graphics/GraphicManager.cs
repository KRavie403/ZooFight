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
        //DisplayDropboxOptionChange(1);  // �� ������� �ػ󵵿� ��ġ�ϴ��� Ȯ��
        InitDisplayMode();
        InitResolution();
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

        switch (displayModeDropdown.value)
        {
            case 0:     //  â ���
                Screen.SetResolution(800, 600, screenMode);
                break;
            case 1:     // ��ü ȭ��
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, screenMode);
                break;
            case 2:     // �׵θ� ���� â ���
                SetBorderlessWindowedMode();
                break;
            default:
                break;
        }
        displayModeDropdown.RefreshShownValue();
    }

    // �ػ� ����
    private void InitResolution()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0; //ó���� drop�� �� �ʱ�ȭ
        foreach (Resolution value in resolutions)
        {
            Debug.Log(value.width + "x" + value.height + " " + value.refreshRateRatio);
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = value.ToString(); //�ػ󵵰� �־���
            resolutionDropdown.options.Add(option);//option �߰�

            if (value.width == Screen.width && value.height == Screen.height) //���� �ػ��� �ʺ�� Screen.width ���̴� Screen.height�� ����ؼ� �� �� ����
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
