using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : ��ź
/// Value 1 ������
/// Value 2 
/// Value 3 ���߹���
/// Value 4 ��ź �ӵ�
/// Value 5 ��Ÿ�
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
        ItemAction = BombActive(pos);

    }

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
        // �ٳ��� ���ӽð����� ����
        while (duringTime < Value2)
        {

            // �����µ��� ���ӽð� ���� X
            if (!isMoveEnd)
            {
                yield return null;
                continue;
            }

            // �����ð� üũ
            duringTime += Time.deltaTime;


            // ���۰����� ȿ������ �� ����Ʈ , ���� ���
            if (Targets != null)
            {
                // Ÿ���� ������ ���۽�Ű�� ��Ʈ��


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
        myHitScanner.SetScanActive(false);
        // ����� ������ ������Ʈ ��ȯ
        ReturnItem();
    }

    

    public IEnumerator BombActive(Vector3 pos)
    {

        while (true)
        {


            yield return null;
        }
    }

}
