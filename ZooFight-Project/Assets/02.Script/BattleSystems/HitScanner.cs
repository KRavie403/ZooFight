using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IHitBox 
{
    Component myHitBox 
    {
        get; 
    }
    
    public HitScanner.Team Team 
    { 
        get; 
    }

    // 히트박스를 가진 오브젝트가 타격판정이 났을때
    // 타격판정이 난 컴포넌트의 정보를 전달해주어서 작동
    public void HitAction(Component comp);
}


public class HitScanner : MonoBehaviour
{
    public enum Team { RedTeam = -1,NotSetting,BlueTeam}

    Team targetTeam = Team.NotSetting;

    public LayerMask hitMask;

    public Component UserComponent;

    public Collider myScanner;

    public void SetMyTeam(Team team)
    {
        targetTeam = team;
    }

    private void Awake()
    {
        myScanner = GetComponent<Collider>();

    }

    private void Start()
    {
        targetTeam = Team.BlueTeam;
    }

    private void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IHitBox>() == null)
        {
            return;
        }
        if ((int)targetTeam * (int)other.gameObject.GetComponent<IHitBox>().Team == 0)
        {
            return;
        }
        if (targetTeam != other.gameObject.GetComponent<IHitBox>().Team )
        {
            return;
        }
        Debug.Log("Hit");
        other.GetComponent<IHitBox>().HitAction(UserComponent);

    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}