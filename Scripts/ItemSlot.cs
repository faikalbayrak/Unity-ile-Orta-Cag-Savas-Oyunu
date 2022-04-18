using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    private Item item;
    public bool isBeingDraged = false;

    public Item Item => item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = newItem.icon;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;

    }

    public void UseItem()
    {
        if (item == null || isBeingDraged == true) return;
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Inventory.instance.SwitchHotbarInventory(item);
        }
        else
        {
            if (item.itemType != Item.ItemType.ShieldItem && item.itemType != Item.ItemType.HelmetItem && item.itemType != Item.ItemType.ArmorItem && item.itemType != Item.ItemType.WeaponItem)
                item.Use();
            else if (item.itemType == Item.ItemType.CraftingItem)
                item.Use();
            
        }
        
        
    }

    public void DestroySlot()
    {
        Destroy(gameObject);
    }

    public void OnRemoveButtonCliked()
    {
        if(item != null)
        {
            Inventory.instance.RemoveItem(item);
        }
    }

    public void OnCursorEnter()
    {
        if (item == null || isBeingDraged == true) return;

        GameManager.instance.DisplayItemInfo(item.name, item.GetItemDescription(), transform.position);

    }
    public void OnCursorExit()
    {
        if (item == null) return;
        GameManager.instance.DestroyItemInfo();
    }

}
