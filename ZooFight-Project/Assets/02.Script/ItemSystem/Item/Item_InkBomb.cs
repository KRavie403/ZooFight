using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �Թ���ź
/// Value 1 �þ� ���� ����
/// Value 2 ȿ�� ���� �ð�
/// Value 3 ȿ�� ���� ����
/// Value 4 ��ź �ӵ�
/// Value 5 ��Ÿ�
/// </summary>

public class Item_InkBomb : Items
{
    public Item_InkBomb(PlayerController player) : base(player)
    {

    }

    public HitScanner HitScanner;


    bool isItemActive = false;

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

    public void BombShoot(Vector3 Pos)
    {
        // 
        ItemAction = BombActive(Pos);
        //StartCoroutine(BombActive(Pos));
    }

    public IEnumerator BombActive(Vector3 Pos)
    {
        // �߻����� Ȯ��

        // ��ü �̵����� �غ�

        // ��ź ����Ʈ �غ�
        BombEffectSetting();
        // ��ź ���� �غ�
        BombSoundSetting();
        //

        while (!isItemActive)
        {
            yield return null;
        }
    }

    void BombEffectSetting()
    {

    }
    void BombSoundSetting()
    {

    }




}
