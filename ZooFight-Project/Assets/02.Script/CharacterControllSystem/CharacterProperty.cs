using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterProperty : MonoBehaviour
{

    [Range(0f, 3f)]
    public float MoveSpeed;
    [Range(0f,10.0f)]
    public float RunSpeedRate = 1;
    public float MaxHP;


    public UnityEvent<float> UpdateHp;
    private float _curHP = -100.0f;
    public float CurHP
    {
        get
        {
            if (_curHP < 0.0f) _curHP = MaxHP;
            return _curHP;
        }
        set
        {
            _curHP = Mathf.Clamp(value, 0.0f, MaxHP);
            UpdateHp?.Invoke(Mathf.Approximately(MaxHP, 0.0f) ? 0.0f : _curHP);
        }
    }

    public UnityEvent<float> UpdateSp;
    public float MaxSP;
    private float _curSP = -100.0f;
    public float CurSP
    {
        get
        {
            if (_curSP < 0.0f) _curSP = MaxSP;
            return _curSP;
        }
        set
        {
            _curSP = Mathf.Clamp(value, 0.0f, MaxSP);
            UpdateSp?.Invoke(Mathf.Approximately(MaxSP, 0.0f) ? 0.0f : _curSP);
        }
    }

    public float MotionSpeed;
    Animator _anim = null;
    public Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>(); //자기자신의 것부터 찾아봄.
                if (_anim == null) //없으면 자식의 컴포넌트를 가져옴.
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }


    public int CharacterID = -1;
    public HitScanner.Team myTeam = HitScanner.Team.NotSetting;


    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    private void Start()
    {
        
    }



}
