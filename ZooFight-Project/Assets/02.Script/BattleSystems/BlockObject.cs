using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{

    bool isGrab = false;
    PlayerController myPlayer=null;

    public HitScanner.Team myTeam = HitScanner.Team.NotSetting;

    float BlockMoveSpeed
    {
        get => myPlayer.MoveSpeed;
    }
    public Transform myBlockObj;

    public Vector2 curDir = Vector2.zero;

    Vector2[] JudgeVector = new Vector2[2] {new(1,1),new(-1,1) };
    Vector3[] myDirs = new Vector3[5] {Vector3.zero , Vector3.forward, Vector3.left, Vector3.back, Vector3.right };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region ������
    public void Grab(PlayerController player)
    {
        myPlayer = player;


        myPlayer.grabPoint.curGrabBlock = this;
        myPlayer.isGrab = true;
        isGrab = true;
        StartCoroutine(BlockMove());
    }

    public void DeGrab(PlayerController player)
    {
        myPlayer.isGrab = false;
        
        myPlayer.grabPoint.curGrabBlock = null;

        
        StopCoroutine(BlockMove());

        myPlayer = null;
        isGrab=false;

    }
    #endregion

    #region �����̵�����
    IEnumerator BlockMove()
    {
        Vector3 curdir = Vector3.zero;

        Debug.Log("MoveStart");

        while (isGrab)
        {
            if (myPlayer.GetIsmoving() == true)
            {
                curdir.x = curDir.x;
                curdir.z = curDir.y;
                transform.position += curdir * Time.deltaTime * BlockMoveSpeed;
            }
            yield return null;
        }
        if(myPlayer != null)
        {
            myPlayer.isGrab = false;
        }
    }

    public void SetcurDir(Vector2 dir,Vector3 curForward)
    {
        curDir = dir;

    }

    // ���� ���� ���� �����Լ�
    public Vector2 DistSelect(Vector3 pos,Vector3 curForward)
    {
        if(pos == Vector3.zero) return Vector2.zero;
        Quaternion rot =  Quaternion.FromToRotation(Vector3.forward, curForward);

        Vector3 vector3 = rot * pos;

        Vector2 dir = new Vector2(vector3.x, vector3.z);
        


        if (Vector2.Dot(JudgeVector[0],dir) > 0)
        {
            if (Vector2.Dot(JudgeVector[1], dir) > 0)
            {
                return Vector2.up;
            }
            else if (Vector2.Dot(JudgeVector[1], dir) < 0)
            {
                return Vector2.right;
            }
            else
            {
                return Vector2.zero;
            }

        }
        else if (Vector2.Dot(JudgeVector[0],dir)< 0)
        {
            if (Vector2.Dot(JudgeVector[1], dir) > 0)
            {
                return Vector2.left;
            }
            else if (Vector2.Dot(JudgeVector[1], dir) < 0)
            {
                return Vector2.down;
            }
            else
            {
                return Vector2.zero;
            }
        }
        else
        {
            return Vector2.zero;
        }


    }

    #endregion

    #region �¸���������

    public void VictoryDecide(HitScanner.Team BeaconTeam)
    {
        if (BeaconTeam == HitScanner.Team.NotSetting) return;

        if(myTeam == BeaconTeam)
        {
            // �¸��� �����Ұ� ����

            Debug.Log($"{myTeam} Victory!!");
        }

    }




    #endregion



}