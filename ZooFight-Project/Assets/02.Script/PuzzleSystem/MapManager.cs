using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapData[] mapDatas = new MapData[4]; // 4개의 MapData 저장
    public GameObject[] blockPrefabs;                      // 블록 타입별 프리팹 배열

    private int currentMapIndex = 0;
    public void SaveMapData(List<BlockData> blocks)
    {
        if (currentMapIndex >= 0 && currentMapIndex < mapDatas.Length)
        {
            MapData mapData = mapDatas[currentMapIndex];

            if (mapData != null)
            {
                if (mapData.blocks == null)
                {
                    mapData.blocks = new List<BlockData>();
                }

                mapData.blocks.AddRange(blocks);  // blocks에 있는 모든 BlockData를 추가

                Debug.Log($"{currentMapIndex}번째 맵 데이터 저장");
            }
            else
            {
                Debug.LogError("현재 맵 데이터가 null입니다.");
            }
        }
        else
        {
            Debug.LogError("잘못된 맵 인덱스입니다.");
        }
    }



    public void LoadMapData()
    {
        // 0~3 사이 랜덤 인덱스 선택
        int currentMapIndex = Random.Range(0, mapDatas.Length);

        Debug.Log("맵 번호: " + currentMapIndex);

        // 선택된 맵 데이터 가져오기
        MapData selectedMap = mapDatas[currentMapIndex];


        foreach (BlockData block in selectedMap.blocks)
        {
            // 블록의 위치 설정
            Vector3 blockPosition = new Vector3(block.x, block.y, block.z);

            // 블록 타입에 맞는 프리팹 선택
            GameObject blockPrefab = blockPrefabs[block.type];

            // 블록 인스턴스 생성
            GameObject blockInstance;
            if (block.type == 3 || block.type == 4 || block.type == 5)
            {
                blockInstance = Instantiate(blockPrefab, blockPosition, Quaternion.Euler(0, 90, 0));
            }
            else
                blockInstance = Instantiate(blockPrefab, blockPosition, Quaternion.identity);

            // 블록에 Block 스크립트 추가 및 초기화
            //BlockObject blockComponent = blockInstance.AddComponent<BlockObject>();
            //blockComponent.Initialize(block.blockNum, block.type, blockPosition);

#if UNITY_EDITOR || DEBUG
            Debug.Log($"블록 생성 - 번호: {block.blockNum}, 타입: {block.type}, 위치: {blockPosition}");
#endif
        }
    }

    public void SetCurrentMapIndex(int index)
    {
        if (index >= 0 && index < mapDatas.Length)
        {
            currentMapIndex = index;
        }
        else
        {
            Debug.LogError("잘못된 맵 인덱스입니다.");
        }
    }
}
