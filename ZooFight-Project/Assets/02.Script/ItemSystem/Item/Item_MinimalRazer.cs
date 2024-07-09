using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �����۸� : ����ȭ ����
/// Value 1 ȿ�� ���� �ð�
/// Value 2 ����ð�
/// Value 3 ������ ����
/// Value 4 ����ӵ�
/// Value 5 ������ ��Ÿ�
/// </summary>


public class Item_MinimalRazer : Items 
{
    public Item_MinimalRazer(PlayerController player) : base(player)
    {

    }

    public EffectPlayer effectPlayer;
    // �����׽�Ʈ��
    public GameObject Test1;

    public GameObject RazerGunObj;

    public Transform RazerStartPoint;

    public HitScanner myHitScanner;



    Vector3 Dir = Vector3.zero;


    protected override void Awake()
    {
        base.Awake();
        //Effectmanager.Inst.CreateEffectObj(effectPlayer, transform);\
        // ��Ʈ��ĳ�� ����
        if (GetComponent<HitScanner>() != null)
        {
            myHitScanner = GetComponent<HitScanner>();

            // �׽�Ʈ ���� ����
            if(myHitScanner == null )
            {
                gameObject.AddComponent<HitScanner>();
            }
            // �׽�Ʈ ���� ����

            myHitScanner.enabled = false;
        }
        else
        {
            // �̽��� ���·� ����
            myHitScanner = transform.AddComponent<HitScanner>();
            myHitScanner.enabled = false;
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
       
        //effectPlayer = EffectSetting.keys[EffectCode.Item_MinimalRazer];

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Initate(List<float> Values, PlayerController player)
    {
        base.Initate(Values, player);
    }


    protected override IEnumerator ItemActions()
    {
        yield return base.ItemActions();

        float duringTime = 0;

        List<GameObject> curTargets = new List<GameObject>();


        // �ߵ��� ��Ʈ��ĳ�� ����
        myHitScanner.SetScanActive(true);

        while (duringTime < Value2)
        {
            duringTime += Time.deltaTime;


            // Ÿ�� ������ ���� ��� 
            if(Targets != null)
            {
                foreach (GameObject T in Targets)
                {
                    if (curTargets.Contains(T)) continue;

                    if (T.GetComponent<PlayerController>() != null)
                    {
                        T.GetComponent<PlayerController>().PlayerSizeChange(0.5f);
                        curTargets.Add(T);
                    }
                }
            }
        }

        // ��� ����� ũ�� ���󺹱�
        foreach (GameObject T in curTargets)
        {
            T.GetComponent<PlayerController>().PlayerSizeChange(1.0f);
        }


        ReturnItem();

    }

    
    
    public void RazerEffectSetting()
    {
        // 1������Ʈ ���ܽ���

        // 2������Ʈ ����Ÿ���ŭ ����

    }

    public void RazerSoundSetting()
    {
        //Soundmanager.Inst.SoundPool.
        // ���� ���� ���
    }

    public void RazerShoot()
    {

    }

    public override void ItemUse()
    {
        base.ItemUse();
        //effectPlayer.
    }


}
