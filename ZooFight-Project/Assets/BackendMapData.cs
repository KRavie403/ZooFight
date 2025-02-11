using UnityEngine;
using BackEnd;
using UnityEngine.Events;

public class BackendMapData
{
    public static BackendMapData _Inst = null;
    public static BackendMapData Inst
    {
        get
        {
            if(_Inst == null)
            {
                _Inst = new BackendMapData();
            }

            return _Inst;
        }
    }


    [System.Serializable]
    public class GameDataLoadEvent : UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private MapGameData mapGameData = new MapGameData();
    public MapGameData MapGameData => mapGameData;

    private string gameDataRowInDate = string.Empty;

    /// <summary>
    /// 뒤끝 콘솔 테이블에 새로운 맵 정보 추가
    /// </summary>
    public void GameDataInsert()
    {
        //mapGameData.Reset();   // 맵 정보를 초기값으로 설정

        Param param = new Param()
        {
            { "blockNum",            mapGameData.blockNum },
            { "type",                     mapGameData.type },
            { "x",                          mapGameData.x },
            { "y",                          mapGameData.y },
            { "z",                          mapGameData.z }
        };

        // 비동기로 테이블에 데이터 추가 시도
        Backend.GameData.Insert("MAP_DATA_1", param, callback =>
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
    /// 뒤끝 콘솔 테이블에서 맵 정보를 불러올 때 호출
    /// </summary>
    public void GameDataLoad()
    {
        Backend.GameData.GetMyData("MAP_DATA_1", new Where(), callback =>
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
                        mapGameData.blockNum = int.Parse(gameDataJson[0]["blockNum"].ToString());
                        mapGameData.type = int.Parse(gameDataJson[1]["type"].ToString());
                        mapGameData.x = int.Parse(gameDataJson[2]["x"].ToString());
                        mapGameData.y = int.Parse(gameDataJson[3]["y"].ToString());
                        mapGameData.z = int.Parse(gameDataJson[4]["z"].ToString());

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                // JSON 데이터 파싱 실패
                catch (System.Exception e)
                {
                    //mapGameData.Reset(); // 맵 정보를 초기값으로 설정
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
    /// 뒤끝 콘솔 테이블에 있는 맵 데이터 갱신
    /// </summary>
    public void GameDataUpdate(UnityAction action = null)
    {
        if(mapGameData == null)
        {
#if DEBUG || UNITY_EDITOR
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다." +
                "Insert 혹은 Load를 통해 데이터를 생성해주세요.");
#endif
            return;
        }

        Param param = new Param()
        {
            { "blockNum",          mapGameData.blockNum },
            { "type",                   mapGameData.type },
            { "x",                        mapGameData.x },
            { "y",                        mapGameData.y },
            { "z",                        mapGameData.z }
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

            Backend.GameData.UpdateV2("MAP_DATA_1", gameDataRowInDate, Backend
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
