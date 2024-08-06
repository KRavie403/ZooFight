using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] characterPrefabs;   // 캐릭터 프리팹 배열
    public Transform spawnPoint;                 // 캐릭터가 스폰될 위치

    private int curIndex = 0;                    // 현재 캐릭터 인덱스
    private GameObject currentCharacter;    // 현재 활성화된 캐릭터

    private CharacterSelection _characterSelection;
    private Button _rightButton; 
    private Button _leftButton;

    private void Awake()
    {
        spawnPoint.position = new Vector3(2.43752193f, 5.06699991f, 68.4682541f);
    }
    private void Start()
    {
        // 처음 캐릭터를 스폰
        //spawnPoint = currentCharacter.transform;
        SpawnCharacter(curIndex);

        //// 버튼 클릭 이벤트에 메서드 연결
        //_rightButton.onClick.AddListener(OnRightButtonClick);
        //_leftButton.onClick.AddListener(OnLeftButtonClick);
    }

    //private void GetUserk(int curUser)
    //{
    //    // 몇 번째 유저인지 정보 불러옴
    //    //_currentUser = ;
    //    switch (curUser)
    //    {
    //        case 0:
    //            _leftButton = _characterSelection.SelectBtn1[0].GetComponent<Button>;
    //            break;
    //        case 1:
    //            minimapUI.SetActive(false);
    //            characterStat[0].SetActive(false);
    //            characterStat[1].SetActive(true);
    //            // Gamemanager에서 유저 1의 캐릭터 종류 받아오는 코드 추가
    //            //_textSprite[0] = Resources.Load<Sprite>(_character);
    //            _characterImage[2].sprite = Resources.Load<Sprite>(_character);
    //            break;
    //        case 2:
    //            minimapUI.SetActive(false);
    //            characterStat[0].SetActive(false);
    //            characterStat[1].SetActive(true);
    //            // Gamemanager에서 유저 2의 캐릭터 종류 받아오는 코드 추가
    //            //_textSprite[1] = Resources.Load<Sprite>(_character);
    //            _characterImage[2].sprite = Resources.Load<Sprite>(_character);
    //            break;
    //        default:
    //            Debug.LogError("유저 번호가 할당되지 않았습니다.");
    //            break;
    //    }
    //}

    public void OnRightButtonClick()
    {
        // 인덱스를 증가시키고 배열 크기를 넘어가면 0으로 설정
        curIndex = (curIndex + 1) % characterPrefabs.Length;
        SpawnCharacter(curIndex);
    }

    public void OnLeftButtonClick()
    {
        // 인덱스를 감소시키고 0보다 작아지면 배열의 마지막 인덱스로 설정
        curIndex = (curIndex - 1 + characterPrefabs.Length) % characterPrefabs.Length;
        SpawnCharacter(curIndex);
    }

    private void SpawnCharacter(int index)
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
