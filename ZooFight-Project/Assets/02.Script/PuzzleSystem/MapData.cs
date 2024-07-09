using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMap", menuName = "Map")]
public class MapData : ScriptableObject
{
    public List<BlockData> blocks;
}

[System.Serializable]
public class BlockData
{
    public int id;
    public Vector3 position;
}
