using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ÿ���� �޴� ����� �������ִ� �������̽�
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

    // ��Ʈ�ڽ��� ���� ������Ʈ�� Ÿ�������� ������ �ش� ������Ʈ ����
    public void HitAction(Component comp);

}

// Ÿ���� �ִ� ��ä�� ������ �մ� Ŭ����
public class HitScanner : MonoBehaviour
{
    public enum Team { RedTeam = -1,NotSetting,BlueTeam,AllTarget}

    // Ÿ�� ���
    Team targetTeam = Team.NotSetting;

    // �浹 ���� ������Ʈ ���̾�
    public LayerMask hitMask;

    // ������� ������Ʈ
    public Component UserComponent;



    List<GameObject> Targets;
    bool isScan = false;
    bool isActive = false;
    float ScanRange = 0.2f;

    // ��������
    public float MyDamage = 0.0f ;

    public void SetMyTeam(Team team)
    {
        targetTeam = team;
    }

    private void Awake()
    {

    }

    private void Start()
    {
        Targets = new List<GameObject>();
        //targetTeam = Team.BlueTeam;
        if(!isActive)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {

    }

    // ��� ��ü(������ or ����ü? or �ٸ� ����) �� ��� �� ����
    public void Initiate(Component comp,Team TargetTeam)
    {
        if(comp.GetComponent<Component>() != null)
        {
            UserComponent = comp;
            targetTeam = TargetTeam;

        }
    }
                                                                                                                                           
    public void SetScanActive(bool Active)
    {
        isActive = Active;
        if(Active)
        {
            enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��� ���۽�
        if(isActive)
        {
            Debug.Log("HitScanStart");
            // ����� ��Ʈ�ڽ��� ���ٸ� ����
            if (other.gameObject.GetComponent<IHitBox>() == null) return;
            else
            {
                Debug.Log(other.gameObject);
                if(targetTeam == Team.NotSetting)
                {
                    return;
                }
                else if(targetTeam != other.GetComponent<IHitBox>().Team)
                {
                    return;
                }

            
                if(!Targets.Contains(other.gameObject))
                {
                    Targets.Add(other.gameObject);
                    Debug.Log($"{other} Add Target");
                }
            
                if( UserComponent.GetComponent<Items>() != null )
                {
                    if( other.gameObject != UserComponent.GetComponent<Items>().GetPlayer().gameObject)
                    {
                        UserComponent.GetComponent<Items>().AddTargets(other.gameObject); 
                        Targets.Remove(other.gameObject);
                        Debug.Log($"{other} Add Item Target");
                    }


                }
                else if(UserComponent.GetComponent<PlayerController>() != null)
                {

                }


                if (Targets != null) 
                {
                    isScan = true;
                }

            }
            // ��ĵ�ϱ�
            //Scan(ScanRange);

            //Targets.Add(other.gameObject);


            //if ((int)targetTeam * (int)other.gameObject.GetComponent<IHitBox>().Team == 0)
            //{

            //    return;
            //}
            //if (targetTeam != other.gameObject.GetComponent<IHitBox>().Team )
            //{
            //    return;
            //}
            //Debug.Log("Hit");
            //other.GetComponent<IHitBox>().HitAction(UserComponent);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (isActive)
        {
            if(other.gameObject.GetComponent<IHitBox>() != null)
            {

            }
        }
    }


    // Ž�� ���� ����� Ž��
    public void Scan(float Range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range);

        Targets = new List<GameObject>();

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == this.gameObject) continue;

            if (collider.GetComponent<IHitBox>() != null) 
            {
                if(targetTeam == Team.AllTarget)
                {
                    if(!Targets.Contains(collider.gameObject))
                    {
                        Targets.Add(collider.gameObject);
                    }
                    continue;
                }
                if (targetTeam == Team.NotSetting) 
                {
                    continue;
                }
                if (targetTeam != collider.gameObject.GetComponent<IHitBox>().Team)
                {
                    continue;
                }
                Debug.Log(collider.gameObject);
                if (!Targets.Contains(collider.gameObject))
                {
                    Targets.Add(collider.gameObject);
                }
            }
        }
        if (Targets != null)
        {
            if (UserComponent.GetComponent<Items>() != null)
            {
                UserComponent.GetComponent<Items>().AddTargets(Targets);
            }
            else if (UserComponent.GetComponent<PlayerController>() != null) 
            {

            }
            isScan = true;
        }
    }

    public void Reset()
    {
        UserComponent = null;
        targetTeam = Team.NotSetting;
    }
}