using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterPlacement : MonoBehaviour
{
    public GameObject[] pinkTeam;
    public GameObject[] blueTeam;

    private float mapWidth = 60.0f;
    private float mapHeight = 40.0f;
    public List<Vector3> spawnUsers = new List<Vector3>();

    private int characterNum = 0;
    private int pinkCharacterNum = 0;
    private int blueCharacterNum = 0;

    private BlockPlacement blockPlacement = null;


    void Start()
    {
        blockPlacement = GetComponent<BlockPlacement>();
        PlaceCharacters();
    }

    public void PlaceCharacters()
    {
        while (characterNum < 4)
        {
            float x = UnityEngine.Random.Range(0f, mapWidth + 1.0f);
            float y = UnityEngine.Random.Range(0f, mapHeight + 1.0f);
            x -= x % 1;
            y -= y % 1;


            Vector3 charcterPosition = new Vector3(x + 0.5f, 0.5f, y + 0.5f);
            if (charcterPosition == new Vector3(20.5f, 0.5f, 10.5f) || charcterPosition == new Vector3(40.5f, 0.5f, 10.5f))
            {
                continue;
            }

            if (IsPositionInsideMap(charcterPosition))
            {
                if (characterNum < 2)
                {
                    Instantiate(pinkTeam[pinkCharacterNum++], charcterPosition, Quaternion.identity);
                    spawnUsers.Add(charcterPosition);
                    characterNum++;
                }
                else
                {
                    Instantiate(blueTeam[blueCharacterNum++], charcterPosition, Quaternion.identity);
                    spawnUsers.Add(charcterPosition);
                    characterNum++;
                }
            }
        }
        blockPlacement.SetSpawnUsers(spawnUsers);
    }

    bool IsPositionInsideMap(Vector3 position)
    {
        return position.x < mapWidth + 0.5f && position.z < mapHeight + 0.5f;
    }
}
