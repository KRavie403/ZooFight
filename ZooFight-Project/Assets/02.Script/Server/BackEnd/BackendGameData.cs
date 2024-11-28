using UnityEngine;
using BackEnd;
using UnityEngine.Events;

public class BackendGameData : Singleton<BackendGameData>
{
    [System.Serializable]
    public class GameDataLoadEvent : UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private UserGameData userGameData = new UserGameData();
    public UserGameData UserGameData => userGameData;

    private string gameDataRowInDate = string.Empty;

    /// <summary>
    /// 뒤끝 콘솔 테이블에 새로운 유저 정보 추가
    /// </summary>
    public void GameDataInsert()
    {
        //userGameData.Reset();   // 유저 정보를 초기값으로 설정

        Param param = new Param()
        {
            { "character",          userGameData.character },
            { "team",          userGameData.team }
        };

        // 비동기로 테이블에 데이터 추가 시도
        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            // 게임 정보 추가 성공
            if (callback.IsSuccess()){
                // 게임 정보의 고유값
                gameDataRowInDate = callback.GetInDate();

#if DEBUG || UNITY_EDITOR
                Debug.Log($"게임 정보 데이터 삽입에 성공했습니다. {callback}");
#endif
            }
            else   // 실패
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError($"게임 정보 데이터 삽입에 실패했습니다. {callback}");
#endif
            }
        });
    }

    /// <summary>
    /// 뒤끝 콘솔 테이블에서 유저 정보를 불러올 때 호출
    /// </summary>
    public void GameDataLoad()
    {
        Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
        {
            // 게임 정보 불러오기 성공
            if (callback.IsSuccess())
            {
#if DEBUG || UNITY_EDITOR
                Debug.Log($"게임 정보 데이터 불러오기에 성공했습니다. : {callback}");
#endif
                // JSON 데이터 파싱 성공
                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    // 받아온 데이터의 개수가 0이면 데이터가 없는 것
                    if (gameDataJson.Count <= 0)
                    {
#if DEBUG || UNITY_EDITOR
                        Debug.LogWarning("데이터가 존재하지 않습니다.");
#endif
                    }
                    else
                    {
                        // 불러온 게임 정보의 고유값
                        gameDataRowInDate = gameDataJson[0]["inDate"].ToString();

                        // 불러온 게임 정보를 userGameData 변수에 저장
                        userGameData.character = int.Parse(gameDataJson[0]["character"].ToString());
                        userGameData.character = int.Parse(gameDataJson[1]["user"].ToString());

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                // JSON 데이터 파싱 실패
                catch (System.Exception e)
                {
                    //userGameData.Reset(); // 유저 정보를 초기값으로 설정
#if DEBUG || UNITY_EDITOR
                    Debug.LogError(e);
#endif
                }
            }
            else
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError($"게임 정보 데이터 불러오기에 실패했습니다. : {callback}");
#endif
            }
        });
    }

    /// <summary>
    /// 뒤끝 콘솔 테이블에 있는 유저 데이터 갱신
    /// </summary>
    public void GameDataUpdate(UnityAction action = null)
    {
        if(userGameData == null)
        {
#if DEBUG || UNITY_EDITOR
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다." +
                "Insert 혹은 Load를 통해 데이터를 생성해주세요.");
#endif
            return;
        }

        Param param = new Param()
        {
            { "character",          userGameData.character },
            { "team",          userGameData.team }
        };

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
#if DEBUG || UNITY_EDITOR
            Debug.LogError("유저의 inDate 정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
#endif
        }

        // 테이블에 저장되어 있는 값 중 inDate 칼럼의 값과
        // 소유하는 유저의 owner_inDate가 일치하는 row를 검색하여 수정하는 UpdateV2() 비동기로 호출
        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정을 요청합니다.");

            Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend
                .UserInDate, param, callback =>
                {
                    if (callback.IsSuccess())
                    {
#if DEBUG || UNITY_EDITOR
                        Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");

                        action?.Invoke();
#endif
                    }
                    else
                    {
#if DEBUG || UNITY_EDITOR
                        Debug.LogError($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
#endif
                    }
                    return;
                });
        }
    }
}
