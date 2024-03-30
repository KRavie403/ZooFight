using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

[System.Serializable]
public struct ZoomData
{
    public float ZoomSpeed;
    public Vector2 ZoomRange;
    public float ZoomLerpSpeed;
    public float curDist;
    public float desireDist;
    // �ʱ�ӵ����� ������
    public ZoomData(float speed)
    {
        ZoomSpeed = speed;
        ZoomRange = new Vector2(1, 2);
        ZoomLerpSpeed = 3.0f;
        curDist = 0.0f;
        desireDist = 0.0f;
    }
}

public class CharacterCamera : BasicCamera
{
    public Camera myCam;
    public LayerMask crashMask;

    public Transform CameraPos;

    //public Transform CharacterRot;

    float Offset = 0.5f;
    float RotSpeed = 180.0f;
    public ZoomData myZoomData =new ZoomData(3.0f);

    [Range(-1.0f, 1.0f)]
    public float AxisX; //���콺 �¿� = �¿� �����̵�
    public float CamHorizontalRotSpeed = 100;

    [Range(-1.0f, 1.0f)]
    public float AxisY; //���콺 ���� = �þ� �����̵�
    public float CamVerticalRotSpeed = 10;
    public Vector3 curRot = Vector3.zero;

    public bool CameraChange = false;

    // ������� ȭ��
    public bool isPlayerCam
    {
        get => (Gamemanager.Inst.currentPlayer.GetComponentInChildren<CharacterCamera>() == this ?
                true : false);
    }


    [SerializeField]
    public Vector2 VertiaclAngle = new Vector2(-60.0f, 60.0f); // ���Ͻþ߰��� (����,����) �ִ밢��


    private void Awake()
    {



        //base.CamTranform = this.transform;
        //base.targetCam = this.GetComponent<Camera>();
        //CameraPos = this.transform.parent;

    }

    // Start is called before the first frame update
    void Start()
    {
        myCam = GetComponentInChildren<Camera>();
        CamTranform = myCam.transform;
        curRot = transform.localRotation.eulerAngles;
        myZoomData.desireDist = myZoomData.curDist = myCam.transform.localPosition.magnitude;
        //VerticalAngle = 0;
        //myCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!CameraChange)
        {
            CameraRotate();

        }

    }


    public void CameraRotate()
    {

        curRot.x = Mathf.Clamp(curRot.x - 
            (AxisY * CamVerticalRotSpeed * Time.deltaTime)
            , VertiaclAngle.x, VertiaclAngle.y);
        curRot.y += AxisX * RotSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);


        transform.parent.localRotation = Quaternion.Euler(0,curRot.y,0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, 
            Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, 0),
            Time.deltaTime * 10.0f);

        myZoomData.desireDist = Mathf.Clamp(myZoomData.desireDist -
            Input.GetAxis("Mouse ScrollWheel") * myZoomData.ZoomSpeed, 
            myZoomData.ZoomRange.x, myZoomData.ZoomRange.y);


        if(Physics.Raycast(transform.position,-transform.forward,
            out RaycastHit hit , myZoomData.curDist + Offset, crashMask))
        {
            myZoomData.curDist = hit.distance - Offset; 
        }
        else
        {
            myZoomData.curDist = Mathf.Lerp(myZoomData.curDist,myZoomData.desireDist,
                Time.deltaTime * myZoomData.ZoomLerpSpeed);
        }

        myCam.transform.localPosition = Vector3.back * myZoomData.curDist;

        //temp.y = temp.z = 0;


        //CameraPos.localRotation = Quaternion.Euler(curRot.x,0,0);

        //CameraPos.transform.Rotate(AxisY * CamVerticalRotSpeed * Time.deltaTime, 0, 0);

        //if (Input.GetMouseButton(1))
        //{
        // rot X ���� �ٲٰ�

        //    curRot.x = Mathf.Clamp(curRot.x - Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime, LookUpRange.x, LookUpRange.y);
        //    curRot.y += Input.GetAxis("Mouse X") * RotSpeed * Time.deltaTime;
        //    transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
        //}

        //if (!toggleCameraRotation) //����� �����ִ��� Ȯ��.
        //{
        //    transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
        //    transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, 0), Time.deltaTime * 10.0f);
        //}
        //else
        //{
        //    transform.localRotation = Quaternion.Euler(0, curRot.y, 0);
        //}


    }


}
