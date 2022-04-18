using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipbarController : MonoBehaviour
{
    public int EquipbarSlotSize => gameObject.transform.childCount;
    private List<ItemSlot> equipbarSlots = new List<ItemSlot>();


    private void Start()
    {
        SetUpEquipbarSlots();
        Inventory.instance.onItemChange += UpdateEquipbarUI;
    }

    private void UpdateEquipbarUI()
    {
        int currentUsedSlotCount = Inventory.instance.equipbarItemList.Count;
        for(int i = 0; i < EquipbarSlotSize; i++)
        {
            if (i < currentUsedSlotCount)
            {
                equipbarSlots[i].AddItem(Inventory.instance.equipbarItemList[i]);

            }
            else
            {
                equipbarSlots[i].ClearSlot();
            }
        }
    }
    private void SetUpEquipbarSlots()
    {
        for(int i = 0; i < EquipbarSlotSize; i++)
        {
            ItemSlot slot = gameObject.transform.GetChild(i).GetComponent<ItemSlot>();
            equipbarSlots.Add(slot);
        }
    }
}
