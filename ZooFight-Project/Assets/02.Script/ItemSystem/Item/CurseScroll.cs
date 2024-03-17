using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �����۸� : ���� ��ũ��
/// Value 1 ���ӽð�
/// </summary>
public class CurseScroll : Items
{

    public EffectPlayer effectPlayer;

    HitScanner.Team TargetTeam = 0;

    List<PlayerController> myTargets = new List<PlayerController>();

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    public IEnumerator CuresActive()
    {
        // ���� ��� Ÿ�� Ȯ��

        // ���� ����Ʈ ��� ���
        CuresEffectSetting();
        // ���� ���� ��� ���
        CurseSoundSetting();

        // ȿ�� ����Ʈ ���� ����
        while (true)
        {
            yield return null;
        }
    }
    public void CuresTargetSetting()
    {
        if(myPlayer.myTeam != HitScanner.Team.NotSetting)
        {
            TargetTeam = (HitScanner.Team)((int)myPlayer.myTeam * -1);
            // Ÿ������ ������ �����ؿ���

        }
    }

    public void CuresEffectSetting()
    {
        for (int i = 0; i < myTargets.Count; i++)
        {
            // ���� ����Ʈ �Ŵ����� ���ؼ� �ߵ��Ҽ��ֵ��� ��������
            effectPlayer.EffectPlay(0, myTargets[i].transform);
        }
    }

    public void CurseSoundSetting()
    {
        // ������� ���� �ʿ�
        for (int i = 0; i < myTargets.Count; i++)
        {
            // ���� ���� �Ŵ����� ���ؼ� �ߵ��Ҽ��ֵ��� ��������

        }
    }


}
