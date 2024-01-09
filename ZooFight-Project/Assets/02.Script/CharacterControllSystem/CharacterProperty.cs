using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterProperty : MonoBehaviour
{

    [Range(0f, 3f)]
    public float MoveSpeed;

    public float MaxHP;

    private float _curHP;
    public float CurHP
    {
        get
        {
            return _curHP;
        }
        set
        {
            _curHP = value;
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
