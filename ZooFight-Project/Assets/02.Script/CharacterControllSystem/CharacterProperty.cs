using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterProperty : MonoBehaviour
{
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

    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    private void Start()
    {
        
    }



}
