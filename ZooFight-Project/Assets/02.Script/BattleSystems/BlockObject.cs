using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{

    bool isGrab = false;
    PlayerController myPlayer=null;

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

    IEnumerator BlockMove()
    {
        Vector3 curdir = Vector3.zero;

        Debug.Log("MoveStart");

        while (isGrab)
        {
            curdir.x = curDir.x;
            curdir.z = curDir.y;
            transform.position += curdir * Time.deltaTime * BlockMoveSpeed;
            yield return null;
        }
        myPlayer.isGrab = false;
    }

    public void SetcurDir(Vector2 dir,Vector3 curForward)
    {
        curDir = dir;

    }

    // 진행 가능 방향 결정함수
    public Vector2 DistSelect(Vector3 pos,Vector3 curForward)
    {

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

    public int DistSelect2(Vector3 pos, Vector3 curForward)
    {
        Vector3 vector3 = pos - transform.position;
        Vector2 dir = new Vector2(pos.x, pos.z);



        if (Vector2.Dot(JudgeVector[0], dir) > 0)
        {
            if (Vector2.Dot(JudgeVector[1], dir) > 0)
            {
                return 1;
            }
            else if (Vector2.Dot(JudgeVector[1], dir) < 0)
            {
                return 4;
            }
            else
            {
                return 0;
            }

        }
        else if (Vector2.Dot(JudgeVector[0], dir) < 0)
        {
            if (Vector2.Dot(JudgeVector[1], dir) > 0)
            {
                return 2;
            }
            else if (Vector2.Dot(JudgeVector[1], dir) < 0)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }


    }


}
