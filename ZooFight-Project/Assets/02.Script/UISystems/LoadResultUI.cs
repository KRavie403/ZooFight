using UnityEngine;
using UnityEngine.UI;
using static HitScanner;

public class LoadResultUI : MonoBehaviour
{
    //[SerializeField]
    //HitScanner.Team DecisionTeam = HitScanner.Team.NotSetting;

    // Eff-ī�޶� ����
    public Camera UICamera;
    [SerializeField] private float _winFOV = 50f;
    [SerializeField] private float _loseFOV = 111f;

    // ��� �̹���
    public Image text;
    public Image t1;
    public Image t2;

    [SerializeField] private Sprite _textSprite;
    [SerializeField] private Sprite _t1Sprite;
    [SerializeField] private Sprite _t2Sprite;

    // Start is called before the first frame update
    void Start()
    {
        LoadResultImg();
        //LoadUserName();
        //LoadEff();
    }

    void LoadResultImg(/*HitScanner.Team BeaconTeam*/)
    {
        //HitScanner.Team playerTeam = GetPlayerTeam();   // �÷��̾��� �� ���� ��������
        //HitScanner.Team winningTeam = Gamemanager.instance.winningTeam;

        //if (winningTeam == playerTeam)
        ////if (GameManager.instance.winningTeam == HitScanner.Team.Red)
        ////if(Gamemanager.Inst.WinnerTeam == HitScanner.Team.Red)    // �¸�
        ////if(DecisionTeam == BeaconTeam)    // �¸�
        //{
        //    _textSprite = Resources.Load<Sprite>("WIN");
        //    _t1Sprite = Resources.Load<Sprite>("WIN(2)");
        //    _t2Sprite = Resources.Load<Sprite>("LOSE(2)");
        //}
        //else if (winningTeam != playerTeam && winningTeam != HitScanner.Team.NotSetting)
        ////else if (winningTeam == HitScanner.Team.Blue)
        ////else if (GameManager.instance.winningTeam == HitScanner.Team.Blue)
        ////else if (Gamemanager.Inst.WinnerTeam == HitScanner.Team.Blue)   // �й�
        ////else if (BeaconTeam == HitScanner.Team.NotSetting)   // �й�
        //    _textSprite = Resources.Load<Sprite>("LOSE");
        //    _t1Sprite = Resources.Load<Sprite>("WIN(3)");
        //    _t2Sprite = Resources.Load<Sprite>("LOSE(3)");
        //}
        //else  // ����� ���
        //{
        //    _textSprite = Resources.Load<Sprite>("DRAW");
        //    _t1Sprite = Resources.Load<Sprite>("DRAW(2)");
        //    _t2Sprite = Resources.Load<Sprite>("DRAW(2)");
        //}
        //text.sprite = _textSprite;
        //    t1.sprite = _t1Sprite;
        //    t2.sprite = _t2Sprite;
    }

    void LoadUserName()
    {

    }

    void LoadEff()
    {
        //if (UICamera == null)
        //{
        //    UICamera = GetComponent<Camera>();
        //}

        //// Field of View ���� ����
        //if() // Win/Draw
        //{
        //    UICamera.fieldOfView = _winFOV;
        //}
        //else
        //{
        //    UICamera.fieldOfView = _loseFOV;
        //}
    }
}
