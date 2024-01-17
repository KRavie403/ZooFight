using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterCamera : BasicCamera
{

    public Transform CameraPos;

    public Transform CharacterRot;

    [Range(-1.0f, 1.0f)]
    public float AxisX; //���콺 �¿� = �¿� �����̵�
    public float CamHorizontalRotSpeed = 100;

    [Range(-1.0f, 1.0f)]
    public float AxisY; //���콺 ���� = �þ� �����̵�
    public float CamVerticalRotSpeed = 10;
    public Vector3 curRot = Vector3.zero;


    [SerializeField]
    public Vector2 VertiaclAngle = new Vector2(-60.0f, 60.0f); // ���Ͻþ߰��� (����,����) �ִ밢��


    private void Awake()
    {
        base.CamTranform = this.transform;
        base.targetCam = this.GetComponent<Camera>();
        CameraPos = this.transform.parent;
    }

    // Start is called before the first frame update
    void Start()
    {
        //VerticalAngle = 0;
        curRot = CameraPos.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotate();

    }


    public void CameraRotate()
    {

        CharacterRot.transform.Rotate(0, AxisX * CamHorizontalRotSpeed * Time.deltaTime, 0);


        curRot.x = Mathf.Clamp(curRot.x - (AxisY * CamVerticalRotSpeed * Time.deltaTime), VertiaclAngle.x, VertiaclAngle.y);
        //temp.y = temp.z = 0;

        CameraPos.localRotation = Quaternion.Euler(curRot.x,0,0);

        //CameraPos.transform.Rotate(AxisY * CamVerticalRotSpeed * Time.deltaTime, 0, 0);
        


    }


}
