using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    [SerializeField]GameObject RedBlock;
    [SerializeField] GameObject BlueBlock;
    [SerializeField] GameObject[] DefaultBlock;

    Vector2[] JudgeVector = new Vector2[2] {new(1,1),new(-1,1) };
    Vector3[] myDirs = new Vector3[5] {Vector3.zero , Vector3.forward, Vector3.left, Vector3.back, Vector3.right };

    public bool isChangeActive = false;

    private void Awake()
    {
        // 오브젝트가 생성될때 이미 존재하는 블럭이 있으면 삭제
        if(Gamemanager.Inst.GetTeamBlock(myTeam) != null)
        {
            if(Gamemanager.Inst.GetTeamBlock(myTeam) != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Gamemanager.Inst.AddBlockObj(this);
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initate(HitScanner.Team myteam)
    {
        myTeam = myteam;
        if (myTeam == HitScanner.Team.BlueTeam) Gamemanager.Inst.BlueTeamBlock = this;
        if (myTeam == HitScanner.Team.RedTeam) Gamemanager.Inst.RedTeamBlock = this;
    }

    #region 잡기관련
    public void Grab(PlayerController player)
    {
        // 블럭교환 동작 중 잡기 불가능
        if(isChangeActive)
        {
            DeGrab(player);
            return;
        }
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

    #region 블럭 교환 관련
    public void ChangeBlockTeam()
    {
        // 블럭의 팀을 변경
        switch (myTeam) 
        {
            case HitScanner.Team.RedTeam:
                myTeam = HitScanner.Team.BlueTeam;
                break;
            case HitScanner.Team.NotSetting:
                return;
            case HitScanner.Team.BlueTeam:
                myTeam= HitScanner.Team.RedTeam;
                break;
            case HitScanner.Team.AllTarget:
                return;
            default:
                break;
        }
        
    }

    public void ForceDeGrab()
    {
        myPlayer.isGrab = false;
        myPlayer.grabPoint.curGrabBlock = null;

        StopCoroutine (BlockMove());

        myPlayer = null;
        isGrab = false;


    }

    #endregion

    #region 블럭이동관련
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

    // 진행 가능 방향 결정함수
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

    #region 승리판정관련

    public void VictoryDecide(HitScanner.Team BeaconTeam)
    {
        if (BeaconTeam == HitScanner.Team.NotSetting) return;

        if(myTeam == BeaconTeam)
        {
            // 승리시 동작할것 실행

            Debug.Log($"{myTeam} Victory!!");
        }

    }




    #endregion



}
