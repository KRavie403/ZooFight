using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : �Թ���ź
/// Value 1 ȿ�� ���ӽð�
/// Value 2 ȿ�� ���� ����
/// Value 3 ???
/// </summary>

public class InkBomb : Items
{

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

    public override void UseItems()
    {
        base.UseItems();

    }

    void BombEffectSetting()
    {

    }
    void BombSoundSetting()
    {

    }




}
