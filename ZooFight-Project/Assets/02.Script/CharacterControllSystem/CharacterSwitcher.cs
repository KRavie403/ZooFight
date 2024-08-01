using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] characterPrefabs;   // 캐릭터 프리팹 배열
    public Transform spawnPoint;                 // 캐릭터가 스폰될 위치

    private int curIndex = 0;                    // 현재 캐릭터 인덱스
    private GameObject currentCharacter;    // 현재 활성화된 캐릭터

    public Button rightButton; 
    public Button leftButton; 

    void Start()
    {
        // 처음 캐릭터를 스폰
        SpawnCharacter(curIndex);

        // 버튼 클릭 이벤트에 메서드 연결
        rightButton.onClick.AddListener(OnRightButtonClick);
        leftButton.onClick.AddListener(OnLeftButtonClick);
    }

    private void GetUser()
    {
        // 몇 번째 유저인지 정보 불러옴
        //_currentUser = ;
    }

    void OnRightButtonClick()
    {
        // 인덱스를 증가시키고 배열 크기를 넘어가면 0으로 설정
        curIndex = (curIndex + 1) % characterPrefabs.Length;
        SpawnCharacter(curIndex);
    }

    void OnLeftButtonClick()
    {
        // 인덱스를 감소시키고 0보다 작아지면 배열의 마지막 인덱스로 설정
        curIndex = (curIndex - 1 + characterPrefabs.Length) % characterPrefabs.Length;
        SpawnCharacter(curIndex);
    }

    void SpawnCharacter(int index)
    {
        // 기존 캐릭터 삭제
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        // 새로운 캐릭터 스폰
        currentCharacter = Instantiate(characterPrefabs[index], spawnPoint.position, spawnPoint.rotation);
    }
}
