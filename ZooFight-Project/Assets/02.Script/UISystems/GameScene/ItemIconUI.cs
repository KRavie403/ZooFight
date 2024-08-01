using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconUI : MonoBehaviour
{
    [SerializeField] private Sprite _itemSprite;

    public int curItem;
    private string _item;

    public void UpdateSlotUI(int curItem)
    {
        MatchItemCode(curItem);
        _itemSprite = Resources.Load<Sprite>(_item);
    }

    public void RemoveSlot()
    {
        _itemSprite = null;
    }

    private void MatchItemCode(int curItem)
    {
        switch (curItem)
        {
            case 0:
                _item = "Bomb";
                break;
            case 1:
                _item = "BananaTrap";
                break;
            case 100:
                _item = "GuardDrink";
                break;
            case 101:
                _item = "PowerDrink";
                break;
            case 200:
                _item = "BlockChangeScroll";
                break;
            case 201:
                _item = "CurseScroll";
                break;
            case 202:
                _item = "SpiderBomb";
                break;
            case 203:
                _item = "InkBomb";
                break;
            case 300:
                _item = "ToyHammer";
                break;
        }
    }
}
