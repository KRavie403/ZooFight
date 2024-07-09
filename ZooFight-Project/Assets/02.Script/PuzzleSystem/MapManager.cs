using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapData[] mapDatas = new MapData[4]; // 4개의 MapData 저장
    private int currentMapIndex = 0;

    public void SaveMapData(Dictionary<int, Vector3> placedBlockPositions)
    {
        if (currentMapIndex >= 0 && currentMapIndex < mapDatas.Length)
        {
            for(int i = 0; i< mapDatas.Length; i++)
            {
                if (mapDatas[i] == null)
                {
                    MapData mapData = mapDatas[currentMapIndex];
                    mapData.blocks = new List<BlockData>();

                    foreach (var kvp in placedBlockPositions)
                    {
                        BlockData blockData = new BlockData
                        {
                            id = kvp.Key,
                            position = kvp.Value
                        };
                        mapData.blocks.Add(blockData);
                    }

                    Debug.Log($"{currentMapIndex}번째 맵 데이터 저장");
                }
            }
        }
        else
        {
            Debug.LogError("잘못된 맵 인덱스입니다.");
        }
    }

    public Dictionary<int, Vector3> LoadMapData()
    {
        if (currentMapIndex >= 0 && currentMapIndex < mapDatas.Length)
        {
            MapData mapData = mapDatas[currentMapIndex];
            Dictionary<int, Vector3> placedBlockPositions = new Dictionary<int, Vector3>();

            foreach (var block in mapData.blocks)
            {
                placedBlockPositions.Add(block.id, block.position);
            }

            Debug.Log($"{currentMapIndex}번째 맵 데이터 로드");
            return placedBlockPositions;
        }
        else
        {
            Debug.LogError("잘못된 맵 인덱스입니다.");
            return null;
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
