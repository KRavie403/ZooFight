using UnityEngine;
using UnityEngine.UI;

public class LoadResultUI : MonoBehaviour
{
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
        LoadUserName();
        LoadEff();
    }

    void LoadResultImg()
    {
        //If()    // �̰��� ���
        //{
                _textSprite = Resources.Load<Sprite>("WIN");
                _t1Sprite = Resources.Load<Sprite>("WIN(2)");
                _t2Sprite = Resources.Load<Sprite>("LOSE(2)");
        //}
        //else if ()   // ���� ���
        //{
                //_textSprite = Resources.Load<Sprite>("LOSE");
                //_t1Sprite = Resources.Load<Sprite>("WIN(3)");
                //_t2Sprite = Resources.Load<Sprite>("LOSE(3)");
        //}
        //else  // ����� ���
        //{
                //_textSprite = Resources.Load<Sprite>("DRAW");
                //_t1Sprite = Resources.Load<Sprite>("DRAW(2)");
                //_t2Sprite = Resources.Load<Sprite>("DRAW(2)");
        //}
            text.sprite = _textSprite;
            t1.sprite = _t1Sprite;
            t2.sprite = _t2Sprite;
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
