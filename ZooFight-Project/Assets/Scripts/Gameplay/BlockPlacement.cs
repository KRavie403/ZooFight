using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacement : MonoBehaviour
{
    public GameObject[] block1x1;
    public GameObject[] block1x2;
    public GameObject[] block2x1;
    public GameObject pinkBlock;
    public GameObject blueBlock;


    private float mapWidth = 60.0f;
    private float mapHeight = 40.0f;
    private Dictionary<int, Vector3> mapCoordinates = new Dictionary<int, Vector3>();
    private List<Vector3> placedBlockPositions = new List<Vector3>();
    private List<Vector3> receivedSpawnUsers = new List<Vector3>();

    // �α� Ȯ��
    private int blockCount = 0;
    private int maxCount = 0;
    private int mapNum = 1;
    private int blockNum = 0;


    void Start()
    {
        // �� ��ǥ ����(40x60)
        GenerateMapCoordiantes();
        // ��� ��ġ
        PlaceBlocks();
    }

    void GenerateMapCoordiantes()
    {
        for (float y = 0.5f; y < mapHeight + 0.5f; y++)
        {
            for (float x = 0.5f; x < mapWidth + 0.5; x++)
            {
                mapCoordinates.Add(mapNum, new Vector3(x, 0.5f, y));
                mapNum++;
            }
        }
    }

    void PlaceBlocks()
    {
        mapCoordinates[0] = new Vector3(0, 0, 0);
        // Ż�� ��� ��ġ
        Vector3 pinkBlockPosition = new Vector3(20.5f, 0.5f, 10.5f);
        Vector3 blueBlockPosition = new Vector3(40.5f, 0.5f, 10.5f);
        Instantiate(pinkBlock, pinkBlockPosition, Quaternion.identity);
        Instantiate(blueBlock, blueBlockPosition, Quaternion.identity);
        mapCoordinates[FindMapCoordinatesKey(mapCoordinates, pinkBlockPosition)] = new Vector3(0, 0, 0);
        mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blueBlockPosition)] = new Vector3(0, 0, 0);
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(receivedSpawnUsers[i]);
            mapCoordinates[FindMapCoordinatesKey(mapCoordinates, receivedSpawnUsers[i])] = new Vector3(0, 0, 0);
            MarkPositionAsOccupied(receivedSpawnUsers[i]);
        }
        MarkPositionAsOccupied(pinkBlockPosition);
        MarkPositionAsOccupied(blueBlockPosition);

        // �������� ��� ��ֹ� ��ġ
        while (blockCount < 2250 & maxCount < 400)
        {
            float x = UnityEngine.Random.Range(0f, mapWidth + 1.0f);
            float y = UnityEngine.Random.Range(0f, mapHeight + 1.0f);
            x -= x % 1;
            y -= y % 1;

            float randomValue = UnityEngine.Random.Range(0f, 1f);

            Vector3 blockPosition = new Vector3(x + 0.5f, 0.5f, y + 0.5f);

            // ��ġ�� ���� ����� �ʰ�, ���̰� mapHeight�� �ʺ� mapWidth ���� �ʴ��� Ȯ��
            if (IsPositionInsideMap(blockPosition))
            {
                // 80% Ȯ���� 1:2 �Ǵ� 2:1 ��� ����
                if (randomValue < 0.8f)
                {
                    if (Random.Range(0f, 1f) < 0.5f) // 50% Ȯ���� 1:2 ��� ����
                    {
                        if (mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] != Vector3.zero)
                        {
                            blockPosition.z += 1.0f;
                            if (mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] != Vector3.zero)
                            {
                                mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] = new Vector3(0, 0, 0);
                                blockPosition.z -= 1.0f;
                                mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] = new Vector3(0, 0, 0);
                                blockPosition.z += 0.5f;

                                blockNum %= 3;
                                Instantiate(block1x2[blockNum++], blockPosition, Quaternion.identity);
                                MarkPositionAsOccupied(blockPosition);
                            }
                        }
                    }
                    else if (Random.Range(0f, 1f) < 0.5f)// ������ 50% Ȯ���� 2:1 ��� ����
                    {
                        if (mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] != Vector3.zero)
                        {
                            blockPosition.x += 1.0f;
                            if (mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] != new Vector3(0, 0, 0))
                            {
                                mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] = new Vector3(0, 0, 0);
                                blockPosition.x -= 1.0f;
                                mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] = new Vector3(0, 0, 0);
                                blockPosition.x += 0.5f;

                                blockNum %= 3;
                                Instantiate(block2x1[blockNum++], blockPosition, Quaternion.identity);
                                MarkPositionAsOccupied(blockPosition);
                            }
                        }
                    }
                }
                else // ������ 20% Ȯ���� 1:1 ��� ����
                {
                    if (mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] != Vector3.zero)
                    {
                        mapCoordinates[FindMapCoordinatesKey(mapCoordinates, blockPosition)] = new Vector3(0, 0, 0);

                        blockNum %= 3;
                        Instantiate(block1x1[blockNum++], blockPosition, Quaternion.identity);
                        MarkPositionAsOccupied(blockPosition);
                    }
                }
                blockCount++;
                maxCount = 0;
                Debug.Log("blockCount: " + blockCount);
            }
        }
    }

    static int FindMapCoordinatesKey(Dictionary<int, Vector3> mapCoordinates, Vector3 position)
    {
        foreach (KeyValuePair<int, Vector3> kvp in mapCoordinates)
        {
            if (kvp.Value == position)
            {
                return kvp.Key;
            }
        }
        return 0;
    }

    void MarkPositionAsOccupied(Vector3 position)
    {
        placedBlockPositions.Add(position);
    }

    bool IsPositionInsideMap(Vector3 position)
    {
        return position.x < mapWidth + 0.5f && position.z < mapHeight + 0.5f;
    }

    public void SetSpawnUsers(List<Vector3> spawnUsers)
    {
        receivedSpawnUsers = new List<Vector3>(spawnUsers);
    }
}
