using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    //private FieldItems field;

    //public GameObject inventoryPanel;
    //public GameObject shopPanel;

    //public Slot slot;    // 현재 활성 슬롯
    //public Transform slotHolder;

    //public TMP_Text itemsCnt;  // 현재 아이템 개수
    //public TMP_Text slotsCnt;   // 현재 이용중인 슬롯 개수
    //public TMP_Text cache;      // 현재 보유한 금액

    //private void Start()
    //{
    //    slots = slotHolder.GetComponentsInChildren<Slot>();
    //    inven.onSlotCountChange += SlotChange;      // onSlotCountChange 가 참조할 메소드
    //    inven.onChangeItem += RedrawSlotUI;
    //    RedrawSlotUI();                                                 // 모든 슬롯 초기화
    //    //closeShop.onClick.AddListener(DeActiveShop);
    //}

    //// 사용 가능한 슬롯만 활성화
    //private void SlotChange(int val)
    //{
    //    slot.slotnum = i;
    //    Button button;
    //    if (slot.TryGetComponent<Button>(out button))
    //    {
    //        button.interactable = (i < inven.SlotCnt);
    //    }
    //}

    //private void Update()
    //{
    //    if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab)) && !_isStoreActive)
    //    {
    //        _activeInventory = !_activeInventory;
    //        inventoryPanel.SetActive(_activeInventory);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        _activeInventory = !_activeInventory;
    //        _isStoreActive = !_isStoreActive;
    //        inventoryPanel.SetActive(_activeInventory);
    //        shopPanel.SetActive(_isStoreActive);
    //    }
    //    if (Input.GetKeyDown(KeyCode.G) && !_activeInventory)
    //    {
    //        _isStoreActive = !_isStoreActive;
    //        ActiveShop(_isStoreActive);
    //        //shopPanel.SetActive(_isStoreActive);
    //    }
    //    cache.text = ItemDatabase.Instance.money.ToString();
    //    itemsCnt.text = field.ItemsCount.ToString();
    //}

    //// 인벤토리 슬롯 개수 조정
    //public void AddSlot()
    //{
    //    inven.SlotCnt++;
    //    slotsCnt.text = inven.SlotCnt.ToString();
    //}

    //private void RedrawSlotUI()
    //{
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        Debug.Log("RedrawSlot");
    //        slots[i].RemoveSlot();
    //    }
    //    for (int i = 0; i < inven.items.Count; i++)
    //    {
    //        slots[i].item = inven.items[i];
    //        slots[i].UpdateSlotUI();
    //    }
    //}

    //public void ActiveShop(bool isOpen)
    //{
    //    if (!_activeInventory)
    //    {
    //        _isStoreActive = isOpen;
    //        shop.SetActive(isOpen);
    //        inventoryPanel.SetActive(isOpen);
    //        shopPanel.SetActive(_isStoreActive);
    //        for (int i = 0; i < slots.Length; i++)
    //        {
    //            slots[i].isShopMode = isOpen;
    //        }
    //    }
    //}

    //public void Throw()
    //{
    //    slot[i - 1] = null;
    //}
}
