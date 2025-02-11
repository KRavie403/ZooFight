using UnityEngine;
using BackEnd;
using LitJson;
using Cysharp.Threading.Tasks;

public class MapInfo : Singleton<MapInfo>
{
    private static MapInfoData data = new MapInfoData();
    public static MapInfoData Data => data;

    public void GetMapInfoFromBackend()
    {
        // 현재 로그인한 사용자 정보 불러오기
        Backend.BMember.GetUserInfo(callback =>
        {
            // 정보 불러오기 성공
            if (callback.IsSuccess())
            {

                // JSON 데이터 파싱 성공
                try
                {
                    JsonData json = callback.GetReturnValuetoJSON()["row"];

                    data.blockNum = int.Parse(json["blockNum"]?.ToString());
                    data.type = int.Parse(json["type"]?.ToString());
                    data.x = int.Parse(json["x"]?.ToString());
                    data.y = int.Parse(json["y"]?.ToString());
                    data.z = int.Parse(json["z"]?.ToString());

                    UserNickname.instance.UpdateNickname();
                }
                // JSON 데이터 파싱 실패
                catch (System.Exception e)
                {
                    // 맵을 기본 상태로 설정
                    data.Reset();
                    // try-catch 에러 출력
#if UNITY_EDITOR || DEBUG
                    Debug.LogError(e);
#endif
                }
            }
            // 정보 불러오기 실패
            else
            {
                // 블록 정보를 기본 상태로 설정
                // Tip. 일반적으로 대비해 기본적인 정보를 저장해두고 초기화할 때 불러와서 사용
                data.Reset();
#if UNITY_EDITOR || DEBUG
                Debug.LogError(callback.GetMessage());
#endif
            }
        });
    }
}

public class MapInfoData
{
    public int blockNum;                            // 블록 번호
    public int type;                                     // 블록 종류
    public float x;                                        // 블록 위치-x
    public float y;                                        // 블록 위치-y
    public float z;                                        // 블록 위치-z

    public void Reset()
    {
        blockNum = 0;
        type = 0;
        x = 0;
        y = 0;
        z = 0;
    }
}
