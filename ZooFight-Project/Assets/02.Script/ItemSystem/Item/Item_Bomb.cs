using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : ��ź
/// Value 1 ������
/// Value 2 ��ź ���� �ð�
/// Value 3 ���߹���
/// Value 4 ��ź �ӵ�
/// Value 5 ��Ÿ�
/// ĳ���͸� �з��� ���� �з���������
/// </summary>


public class Item_Bomb : Items
{
    public Item_Bomb(PlayerController player) : base(player)
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

    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();

    }
    public void BombSetting(Vector3 pos)
    {
        //ItemAction = BombActive(pos);
        ItemAction = ItemActions();
    }

    bool isDone = false;
    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();
        transform.SetParent(null);

        bool isMoveEnd = false;

        //transform.localPosition = Vector3.zero;
        // Ư�� ��η� �̵� ����
        BattleSystems.Inst.routeMaker.RouteKeys[RouteTypes.Arc].
            GetComponent<IRoute>().RouteStart(transform, dir, Value5, Value4, () =>
            {
                //NonActiveObj.SetActive(false);
                //ActivedObj.SetActive(true);
                myHitScanner.Initiate(this, myPlayer.GetEnemyTeam());
                isMoveEnd = true;
                myPlayer.ItemUseEnd();
            });
        //myPlayer.SetState()
        float duringTime = 0;
        myHitScanner.SetScanActive(true);



        while (!isDone)
        {
            // �����ð� �ʰ��� ��� ����
            if(duringTime >= Value2)
            {
                isDone = true;
                continue;
            }

            // �����ð� üũ
            duringTime += Time.deltaTime;


            if (!isMoveEnd)
            {
                yield return null;
                continue;
            }


            // ���۰����� ȿ������ �� ����Ʈ , ���� ���
            if (Targets.Count != 0) 
            {
                // Ÿ���� ������ ���۽�Ű�� ��Ʈ��



                isDone = false;
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

        myHitScanner.SetScanActive(false);

        // ����� ������ ������Ʈ ��ȯ
        Debug.Log($"{this} ActiveEnd");
        isDone = false;
        ReturnItem();
    }


    // ����� �˹��Ű�� �Լ� - Transform , �з��� �Ÿ� , �ӵ� �Է¹ޱ�
    public void PushOut(Transform transform , float Dist , float Speed)
    {
        StartCoroutine(PushedOut(transform, Dist, Speed));
    }

    public IEnumerator PushedOut(Transform transform , float Dist , float Speed)
    {
        float duringTime = 0;

        while (duringTime < Dist / Speed) 
        { 
            duringTime += Time.deltaTime;



            yield return null;
        }

    }




}
