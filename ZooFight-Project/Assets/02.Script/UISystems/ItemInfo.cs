using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemInfo : ScriptableObject
{
    public string ItemName;
    public Sprite icon;
    public int time;
    public int effect;
    public GameObject[] prefab;
}
