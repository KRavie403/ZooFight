using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] SelectBtn1 = new GameObject[2];
    // 게임 시작 버튼 누르면 비활성화

    public int curUser;

    private void GetUser()
    {
        // 몇 번째 유저인지 정보 불러옴
        //_currentUser = ;
        SelectCharacter(curUser);
    }

    private void SelectCharacter(int curUser)
    {
        // 모든 버튼을 비활성화
        SetButtonsActive(SelectBtn1, false);

        // 현재 유저에 따라 특정 버튼 활성화
        switch (curUser)
        {
            case 0:
                SetButtonsActive(SelectBtn1, true);
                break;
                break;
            default:
                break;
        }
    }

    private void SetButtonsActive(GameObject[] buttons, bool isActive)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(isActive);
        }
    }
}