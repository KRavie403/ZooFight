using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 아이템명 : 저주 스크롤
/// Value 1 지속시간
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
        // 저주 대상 타겟 확보

        // 저주 이펙트 출력 대기
        CuresEffectSetting();
        // 저주 사운드 재생 대기
        CurseSoundSetting();

        // 효과 이펙트 사운드 실행
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
            // 타겟팀의 팀원들 인출해오기

        }
    }

    public void CuresEffectSetting()
    {
        for (int i = 0; i < myTargets.Count; i++)
        {
            // 추후 이펙트 매니저를 통해서 발동할수있도록 수정예정
            effectPlayer.EffectPlay(0, myTargets[i].transform);
        }
    }

    public void CurseSoundSetting()
    {
        // 사운드버전 제작 필요
        for (int i = 0; i < myTargets.Count; i++)
        {
            // 추후 사운드 매니저를 통해서 발동할수있도록 수정예정

        }
    }


}
