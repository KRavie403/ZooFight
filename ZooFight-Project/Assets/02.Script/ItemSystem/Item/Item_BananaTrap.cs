using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// �����۸� : �ٳ���
/// Value 1 �з����� �ð�
/// Value 2 ���� �����ð�
/// Value 3 �з����� �Ÿ�
/// Value 4 ����ü �ӵ� 
/// Value 5 ��ô ��Ÿ�
/// </summary>

public class Item_BananaTrap : Items
{
    public Item_BananaTrap(PlayerController player) : base(player) 
    {
        
    }


    [SerializeField]
    EffectPlayer myEffect;
    [SerializeField]
    GameObject NonActiveObj;
    [SerializeField]
    GameObject ActivedObj;

    public HitScanner myHitScanner;
    
    

    protected override void Awake()
    {
        base.Awake();

        // ��Ʈ��ĳ�� ����
        if(GetComponent<HitScanner>() != null )
        {
            myHitScanner = GetComponent<HitScanner>();
            myHitScanner.enabled = false;
        }
        else
        {
            // �̽��� ���·� ����
            myHitScanner = transform.AddComponent<HitScanner>();
            myHitScanner.enabled = false;
        }

        myCode = ItemCode.BananaTrap;

        
    }

    protected override void Start()
    {
        base.Start();
        
        Transform[] objs = GetComponentsInChildren<Transform>();
        foreach( Transform obj in objs )
        {
            if( obj.gameObject.name == "NonActiveObj")
            {
                NonActiveObj = obj.gameObject;
            }
            if( obj.gameObject.name == "ActivedObj")
            {
                ActivedObj = obj.gameObject;
            }
        }

        myHitScanner.SetMyTeam(myPlayer.myTeam);
        
    }

    protected override void Update()
    {
        base.Update();

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
    }


    public override void Initate(List<float> Values, PlayerController player)
    {
        base.Initate(Values, player);

        myHitScanner.Initiate(this, myPlayer.GetEnemyTeam());
    }



    public override void ItemUse()
    {
        base.ItemUse();

        // ������ �ߵ����۽� �ʿ��� ����

        // �ڷ�ƾ�� ������ ��ü�� ��������
        isItemUse = true;
    }

    public override void ItemHitAction()
    {
        base.ItemHitAction();

    }


    protected override IEnumerator ItemActions()
    {
        // �������� ������
        yield return base.ItemActions();
        transform.SetParent(null);

        bool isMoveEnd = false;

        //transform.localPosition = Vector3.zero;
        // Ư�� ��η� �̵� ����
        BattleSystems.Inst.routeMaker.RouteKeys[RouteTypes.Arc].
            GetComponent<IRoute>().RouteStart(transform, dir, Value5, Value4, () =>
            {
                NonActiveObj.SetActive(false);
                ActivedObj.SetActive(true);
                myHitScanner.Initiate(this, myPlayer.GetEnemyTeam());
                isMoveEnd = true;
                myPlayer.ItemUseEnd();
            });
        //myPlayer.SetState()
        float duringTime = 0;
        myHitScanner.SetScanActive(true);
        // �ٳ��� ���ӽð����� ����
        while (duringTime < Value2)
        {
            // �����µ��� ���ӽð� ���� X

            if (!isMoveEnd)
            {
                yield return null;
                continue;
            }
            duringTime += Time.deltaTime;
            // �����ð� üũ


            // ���۰����� ȿ������ �� ����Ʈ , ���� ���
            if(Targets != null)
            {
                // Ÿ���� ������ ���۽�Ű�� ��Ʈ��
                foreach (var target in Targets)
                {
                    if(target.GetComponent<PlayerController>() != null)
                    {

                    }
                }

                // duringTime = Value2;
            }

            // �ߵ��� ������Ʈ �۵� �Ҵ�ó��

            if (isItemUse)
            {
                yield return null;

            }
            else
            {
                yield return base.ItemActions();
            }
            yield return null;
        }

        // ����� ������ ������Ʈ ��ȯ
        ReturnItem();

    }



}
