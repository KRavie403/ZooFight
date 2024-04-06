using UnityEngine;
using UnityEngine.UI;

public class LoadResultUI : MonoBehaviour
{

    // Eff-ī�޶� ����
    [SerializeField] private Camera EFFCamera;
    [SerializeField] private float _winFOV = 50f;
    [SerializeField] private float _loseFOV = 111f;

    // ��� �̹���
    public Image text;
    public Image t1;
    public Image t2;

    [SerializeField] private Sprite _textSprite;
    [SerializeField] private Sprite _t1Sprite;
    [SerializeField] private Sprite _t2Sprite;


    private void Start()
    {
        // ��/��/���º�
        LoadResultImg();
        //LoadUserName();
        LoadEff();
    }

    private void LoadResultImg(/*HitScanner.Team BeaconTeam*/)
    {
        HitScanner.Team playerTeam = Gamemanager.Inst.currentPlayer.myTeam;      // �÷��̾��� �� ���� ��������
        HitScanner.Team winningTeam = Gamemanager.Inst.VictoryTeam; // �¸��� ���� ��������

        // �¸�
        if (winningTeam == playerTeam)
        {
            _textSprite = Resources.Load<Sprite>("WIN");
            _t1Sprite = Resources.Load<Sprite>("WIN(2)");
            _t2Sprite = Resources.Load<Sprite>("LOSE(2)");
        }
        // ���º�
        else if (winningTeam == HitScanner.Team.NotSetting)
        {
            _textSprite = Resources.Load<Sprite>("DRAW");
            _t1Sprite = Resources.Load<Sprite>("DRAW(2)");
            _t2Sprite = Resources.Load<Sprite>("DRAW(2)");
        }
        // �й�
        else  
        {
            _textSprite = Resources.Load<Sprite>("LOSE");
            _t1Sprite = Resources.Load<Sprite>("WIN(3)");
            _t2Sprite = Resources.Load<Sprite>("LOSE(3)");
        }
        text.sprite = _textSprite;
        t1.sprite = _t1Sprite;
        t2.sprite = _t2Sprite;
    }

    private void LoadUserName()
    {

    }

    private void LoadEff()
    {
        // EFF ī�޶�
        //EFFCamera = GetComponent<Camera>();   // �ٸ� ������ �����ϸ� null
        // ����
        if (EFFCamera == null)
        {
            // "MyCamera"��� �±׸� ���� GameObject���� Camera ������Ʈ�� ã��
            GameObject cameraGameObject = GameObject.FindGameObjectWithTag("EffCamera");
            if (cameraGameObject != null)
            {
                EFFCamera = GetComponent<Camera>();
            }
            else
            {
                Debug.LogError("ī�޶� ã�� �� �����ϴ�.");
            }
        }

        // Field of View ���� ����
        HitScanner.Team playerTeam = Gamemanager.Inst.currentPlayer.myTeam;      // �÷��̾��� �� ���� ��������
        HitScanner.Team winningTeam = Gamemanager.Inst.VictoryTeam;                  // �¸��� ���� ��������

        // �¸�
        if (winningTeam == playerTeam)
        {
            EFFCamera.fieldOfView = _winFOV;
        }
        // ���º�
        else if (winningTeam == HitScanner.Team.NotSetting)
        {
            EFFCamera.fieldOfView = _winFOV;
        }
        // �й�
        else
        {
            EFFCamera.fieldOfView = _loseFOV;
        }
    }

    //HitScanner.Team FindPlayerTeam()
    //{
    //    // �÷��̾��� �� ���� ��������
    //    foreach (var player in Gamemanager.Inst.GetTeam(HitScanner.Team.RedTeam).Values)
    //    {
    //        if (player.gameObject == Gamemanager.Inst.currentPlayer.gameObject)
    //        {
    //            return HitScanner.Team.RedTeam;
    //        }
    //    }
    //    return HitScanner.Team.BlueTeam;
    //}
}
