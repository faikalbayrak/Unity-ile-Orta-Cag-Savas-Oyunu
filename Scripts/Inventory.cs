using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region singleton

    public static Inventory instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    public delegate void OnItemChange();
    public OnItemChange onItemChange = delegate { };

    public List<Item> inventoryItemList = new List<Item>();
    public List<Item> equipbarItemList = new List<Item>();
    public List<Item> hotbarItemList = new List<Item>();
    public HotbarController hotbarController;
    public EquipbarController equipbarController;
    bool sameFound = false;
    public bool weaponHasChanged = false;
    public bool shieldHasChanged = false;
    public bool armorHasChanged = false;
    public bool helmetHasChanged = false;

    public void SwitchEquipbarInventory(Item item)
    {
        if (item != null)
        {

            if (item.itemType == Item.ItemType.WeaponItem || item.itemType == Item.ItemType.ShieldItem || item.itemType == Item.ItemType.ArmorItem || item.itemType == Item.ItemType.HelmetItem)
            {
                foreach (Item i in inventoryItemList)
                {
                    if (i == item)
                    {
                        if (equipbarItemList.Count >= equipbarController.EquipbarSlotSize)
                        {
                            GameManager.instance.ViewInventoryInfo("No More Slots Available in Equip Bar", Color.red);

                        }
                        else
                        {
                            if (item.itemType == Item.ItemType.WeaponItem)
                            {
                                
                                weaponHasChanged = true;
                                sameFound =false;

                                for (int j = 0; j < equipbarItemList.Count; j++)
                                {
                                    if (equipbarItemList[j].itemType == item.itemType || equipbarItemList[j].name.Contains("Bow"))
                                    {
                                        Item iterator = equipbarItemList[j];
                                        equipbarItemList.Remove(equipbarItemList[j]);
                                        equipbarItemList.Add(item);
                                        inventoryItemList.Add(iterator);
                                        inventoryItemList.Remove(item);
                                        onItemChange.Invoke();
                                        sameFound = true;
                                        weaponHasChanged = true;
                                    }

                                }
                                if (!sameFound)
                                {
                                    equipbarItemList.Add(item);
                                    inventoryItemList.Remove(item);
                                    onItemChange.Invoke();
                                    weaponHasChanged = true;
                                }
                                return;
                            }
                            else if (item.itemType == Item.ItemType.ShieldItem)
                            {
                                
                                if (item.name.Contains("Bow"))
                                {
                                    shieldHasChanged = true;
                                    sameFound = false;
                                    
                                    for (int j = 0; j < equipbarItemList.Count; j++)
                                    {
                                        if (equipbarItemList[j].itemType == item.itemType)
                                        {
                                            
                                            Item iterator = equipbarItemList[j];
                                            equipbarItemList.Remove(equipbarItemList[j]);
                                            equipbarItemList.Add(item);
                                            inventoryItemList.Add(iterator);
                                            inventoryItemList.Remove(item);
                                            onItemChange.Invoke();
                                            sameFound = true;
                                            shieldHasChanged = true;
                                            for (int x = 0; x < equipbarItemList.Count; x++)
                                            {
                                                if(equipbarItemList[x].itemType == Item.ItemType.WeaponItem)
                                                {
                                                    
                                                    Item iterator2 = equipbarItemList[x];
                                                    equipbarItemList.Remove(iterator2);
                                                    inventoryItemList.Add(iterator2);
                                                    onItemChange.Invoke();
                                                    sameFound = true;
                                                    weaponHasChanged = true;
                                                }
                                            }
                                        }
                                        else if (equipbarItemList[j].itemType == Item.ItemType.WeaponItem)
                                        {

                                            Item iterator = equipbarItemList[j];
                                            equipbarItemList.Remove(equipbarItemList[j]);
                                            equipbarItemList.Add(item);
                                            inventoryItemList.Add(iterator);
                                            inventoryItemList.Remove(item);
                                            onItemChange.Invoke();
                                            sameFound = true;
                                            weaponHasChanged = true;
                                            for (int x = 0; x < equipbarItemList.Count; x++)
                                            {
                                                if (equipbarItemList[x].name.Contains("Shield"))
                                                {

                                                    Item iterator2 = equipbarItemList[x];
                                                    equipbarItemList.Remove(iterator2);
                                                    inventoryItemList.Add(iterator2);
                                                    onItemChange.Invoke();
                                                    sameFound = true;
                                                    shieldHasChanged = true;
                                                }
                                            }
                                        }

                                    }
                                    if (!sameFound)
                                    {
                                        equipbarItemList.Add(item);
                                        inventoryItemList.Remove(item);
                                        onItemChange.Invoke();
                                        shieldHasChanged = true;
                                    }
                                    return;
                                }
                                else
                                {
                                    shieldHasChanged = true;
                                    sameFound = false;

                                    for (int j = 0; j < equipbarItemList.Count; j++)
                                    {
                                        if (equipbarItemList[j].itemType == item.itemType)
                                        {
                                            Item iterator = equipbarItemList[j];
                                            equipbarItemList.Remove(equipbarItemList[j]);
                                            equipbarItemList.Add(item);
                                            inventoryItemList.Add(iterator);
                                            inventoryItemList.Remove(item);
                                            onItemChange.Invoke();
                                            sameFound = true;
                                            shieldHasChanged = true;
                                        }

                                    }
                                    if (!sameFound)
                                    {
                                        equipbarItemList.Add(item);
                                        inventoryItemList.Remove(item);
                                        onItemChange.Invoke();
                                        shieldHasChanged = true;
                                    }
                                    return;
                                }
                                
                            }
                            else if (item.itemType == Item.ItemType.HelmetItem)
                            {
                                helmetHasChanged = true;
                                sameFound = false;

                                for (int j = 0; j < equipbarItemList.Count; j++)
                                {
                                    if (equipbarItemList[j].itemType == item.itemType)
                                    {
                                        Item iterator = equipbarItemList[j];
                                        equipbarItemList.Remove(equipbarItemList[j]);
                                        equipbarItemList.Add(item);
                                        inventoryItemList.Add(iterator);
                                        inventoryItemList.Remove(item);
                                        onItemChange.Invoke();
                                        sameFound = true;
                                        helmetHasChanged = true;
                                    }

                                }
                                if (!sameFound)
                                {
                                    equipbarItemList.Add(item);
                                    inventoryItemList.Remove(item);
                                    onItemChange.Invoke();
                                    helmetHasChanged = true;
                                }
                                return;
                            }
                            else if (item.itemType == Item.ItemType.ArmorItem)
                            {
                                armorHasChanged = true;
                                sameFound = false;

                                for (int j = 0; j < equipbarItemList.Count; j++)
                                {
                                    if (equipbarItemList[j].itemType == item.itemType)
                                    {
                                        Item iterator = equipbarItemList[j];
                                        equipbarItemList.Remove(equipbarItemList[j]);
                                        equipbarItemList.Add(item);
                                        inventoryItemList.Add(iterator);
                                        inventoryItemList.Remove(item);
                                        onItemChange.Invoke();
                                        sameFound = true;
                                        armorHasChanged = true;
                                    }

                                }
                                if (!sameFound)
                                {
                                    equipbarItemList.Add(item);
                                    inventoryItemList.Remove(item);
                                    onItemChange.Invoke();
                                    armorHasChanged = true;
                                }

                                return;
                            }

                        }

                        
                        
                    }
                    
                }


                foreach (Item i in equipbarItemList)
                {
                    if (i == item && !weaponHasChanged && !helmetHasChanged && !armorHasChanged && !shieldHasChanged )
                    {
                        if(item.itemType == Item.ItemType.ShieldItem)
                        {
                            
                            if (item.name.Contains("Torch"))
                            {
                                this.FindComponent<Animator>().SetBool("isTorching", false);
                                shieldHasChanged = true;
                            }
                            else if (item.name.Contains("Shield"))
                            {
                                this.FindComponent<Animator>().SetBool("isShielding", false);
                                shieldHasChanged = true;
                            }
                            else if (item.name.Contains("Bow"))
                            {
                                this.FindComponent<Animator>().SetBool("isThereBow", false);
                                shieldHasChanged = true;
                            }
                        }
                        else if(item.itemType == Item.ItemType.WeaponItem)
                        {
                            weaponHasChanged = true;
                        }
                        else if (item.itemType == Item.ItemType.HelmetItem)
                        {
                            helmetHasChanged = true;
                        }
                        else if (item.itemType == Item.ItemType.ArmorItem)
                        {
                            armorHasChanged = true;
                        }
                        equipbarItemList.Remove(item);
                        inventoryItemList.Add(item);
                        onItemChange.Invoke();
                        return;
                    }

                }
            }
        }
        else
            return;
        
    }
    public void SwitchHotbarInventory(Item item)
    {
        
        
            //inventory to hotbar, CHECK if we have enaugh space
            foreach (Item i in inventoryItemList)
            {
                if (i == item)
                {
                    if (hotbarItemList.Count >= hotbarController.HotbarSlotSize)
                    {
                        GameManager.instance.ViewInventoryInfo("No More Slots Available in Hotbar", Color.red);
                    }
                    else
                    {
                        hotbarItemList.Add(item);
                        inventoryItemList.Remove(item);
                        onItemChange.Invoke();
                    }
                    return;
                }
            }

            //hotbar to inventory
            foreach (Item i in hotbarItemList)
            {
                if (i == item)
                {
                    hotbarItemList.Remove(item);
                    inventoryItemList.Add(item);
                    onItemChange.Invoke();
                    return;
                }
            }
        
       

    }
    public void SwitchEquipbarHotbar(Item item)
    {
        if (item != null)
        {

            if (item.itemType == Item.ItemType.WeaponItem || item.itemType == Item.ItemType.ShieldItem || item.itemType == Item.ItemType.ArmorItem || item.itemType == Item.ItemType.HelmetItem)
            {
                foreach (Item i in hotbarItemList)
                {
                    if (i == item)
                    {
                        if (equipbarItemList.Count >= equipbarController.EquipbarSlotSize)
                        {
                            GameManager.instance.ViewInventoryInfo("No More Slots Available in Equip Bar", Color.red);

                        }
                        else
                        {
                            if (item.itemType == Item.ItemType.WeaponItem)
                            {
                               
                                sameFound = false;

                                for (int j = 0; j < equipbarItemList.Count; j++)
                                {
                                    if (equipbarItemList[j].itemType == item.itemType || equipbarItemList[j].name.Contains("Bow"))
                                    {
                                        Item iterator = equipbarItemList[j];
                                        equipbarItemList.Remove(equipbarItemList[j]);
                                        equipbarItemList.Add(item);
                                        hotbarItemList.Add(iterator);
                                        hotbarItemList.Remove(item);
                                        onItemChange.Invoke();
                                        sameFound = true;
                                        weaponHasChanged = true;
                                    }

                                }
                                if (!sameFound)
                                {
                                    equipbarItemList.Add(item);
                                    hotbarItemList.Remove(item);
                                    onItemChange.Invoke();
                                    weaponHasChanged = true;
                                }
                                return;
                            }
                            else if (item.itemType == Item.ItemType.ShieldItem)
                            {
                                
                                if (item.name.Contains("Bow"))
                                {
                                    sameFound = false;

                                    for (int j = 0; j < equipbarItemList.Count; j++)
                                    {
                                        if (equipbarItemList[j].itemType == item.itemType)
                                        {
                                            
                                            Item iterator = equipbarItemList[j];
                                            equipbarItemList.Remove(equipbarItemList[j]);
                                            equipbarItemList.Add(item);
                                            hotbarItemList.Add(iterator);
                                            hotbarItemList.Remove(item);
                                            onItemChange.Invoke();
                                            sameFound = true;
                                            shieldHasChanged = true;
                                            for (int x = 0; x < equipbarItemList.Count; x++)
                                            {
                                                if (equipbarItemList[x].itemType == Item.ItemType.WeaponItem)
                                                {
                                                    
                                                    Item iterator2 = equipbarItemList[x];
                                                    equipbarItemList.Remove(iterator2);
                                                    hotbarItemList.Add(iterator2);
                                                    onItemChange.Invoke();
                                                    sameFound = true;
                                                    weaponHasChanged = true;
                                                }
                                            }
                                        }
                                        else if (equipbarItemList[j].itemType == Item.ItemType.WeaponItem)
                                        {
                                            
                                            Item iterator = equipbarItemList[j];
                                            equipbarItemList.Remove(equipbarItemList[j]);
                                            equipbarItemList.Add(item);
                                            hotbarItemList.Add(iterator);
                                            hotbarItemList.Remove(item);
                                            onItemChange.Invoke();
                                            sameFound = true;
                                            weaponHasChanged = true;
                                            for (int x = 0; x < equipbarItemList.Count; x++)
                                            {
                                                if (equipbarItemList[x].name.Contains("Shield"))
                                                {
                                                    
                                                    Item iterator2 = equipbarItemList[x];
                                                    equipbarItemList.Remove(iterator2);
                                                    hotbarItemList.Add(iterator2);
                                                    onItemChange.Invoke();
                                                    sameFound = true;
                                                    shieldHasChanged = true;
                                                }
                                            }
                                        }

                                    }
                                    if (!sameFound)
                                    {
                                        equipbarItemList.Add(item);
                                        hotbarItemList.Remove(item);
                                        onItemChange.Invoke();
                                        shieldHasChanged = true;
                                    }
                                    return;
                                }
                                else
                                {
                                    sameFound = false;
                                    
                                    for (int j = 0; j < equipbarItemList.Count; j++)
                                    {
                                        if (equipbarItemList[j].itemType == item.itemType)
                                        {
                                            Item iterator = equipbarItemList[j];
                                            equipbarItemList.Remove(equipbarItemList[j]);
                                            equipbarItemList.Add(item);
                                            hotbarItemList.Add(iterator);
                                            hotbarItemList.Remove(item);
                                            onItemChange.Invoke();
                                            sameFound = true;
                                            shieldHasChanged = true;
                                        }

                                    }
                                    if (!sameFound)
                                    {
                                        equipbarItemList.Add(item);
                                        hotbarItemList.Remove(item);
                                        onItemChange.Invoke();
                                        shieldHasChanged = true;
                                    }
                                    return;
                                }
                                
                            }
                            else if (item.itemType == Item.ItemType.HelmetItem)
                            {
                                
                                sameFound = false;

                                for (int j = 0; j < equipbarItemList.Count; j++)
                                {
                                    if (equipbarItemList[j].itemType == item.itemType)
                                    {
                                        Item iterator = equipbarItemList[j];
                                        equipbarItemList.Remove(equipbarItemList[j]);
                                        equipbarItemList.Add(item);
                                        hotbarItemList.Add(iterator);
                                        hotbarItemList.Remove(item);
                                        onItemChange.Invoke();
                                        sameFound = true;
                                        helmetHasChanged = true;
                                    }

                                }
                                if (!sameFound)
                                {
                                    equipbarItemList.Add(item);
                                    hotbarItemList.Remove(item);
                                    onItemChange.Invoke();
                                    helmetHasChanged = true;
                                }
                                return;
                            }
                            else if (item.itemType == Item.ItemType.ArmorItem)
                            {
                                
                                sameFound = false;

                                for (int j = 0; j < equipbarItemList.Count; j++)
                                {
                                    if (equipbarItemList[j].itemType == item.itemType)
                                    {
                                        Item iterator = equipbarItemList[j];
                                        equipbarItemList.Remove(equipbarItemList[j]);
                                        equipbarItemList.Add(item);
                                        hotbarItemList.Add(iterator);
                                        hotbarItemList.Remove(item);
                                        onItemChange.Invoke();
                                        sameFound = true;
                                        armorHasChanged = true;
                                    }

                                }
                                if (!sameFound)
                                {
                                    equipbarItemList.Add(item);
                                    hotbarItemList.Remove(item);
                                    onItemChange.Invoke();
                                    armorHasChanged = true;
                                }

                                return;
                            }

                        }



                    }
                }


                foreach (Item i in equipbarItemList)
                {
                    if (i == item && !weaponHasChanged && !helmetHasChanged && !armorHasChanged && !shieldHasChanged)
                    {
                        if (item.itemType == Item.ItemType.ShieldItem)
                        {
                            
                            if (item.name.Contains("Torch"))
                            {
                                this.FindComponent<Animator>().SetBool("isTorching", false);
                                shieldHasChanged = true;
                            }
                            else if(item.name.Contains("Shield"))
                            {
                                this.FindComponent<Animator>().SetBool("isShielding", false);
                                shieldHasChanged = true;
                            }
                            else if (item.name.Contains("Bow"))
                            {
                                this.FindComponent<Animator>().SetBool("isThereBow", false);
                                shieldHasChanged = true;
                            }
                        }
                        else if (item.itemType == Item.ItemType.WeaponItem)
                        {
                            weaponHasChanged = true;
                        }
                        else if (item.itemType == Item.ItemType.HelmetItem)
                        {
                            helmetHasChanged = true;
                        }
                        else if (item.itemType == Item.ItemType.ArmorItem)
                        {
                            armorHasChanged = true;
                        }




                        equipbarItemList.Remove(item);
                        hotbarItemList.Add(item);
                        onItemChange.Invoke();
                        return;
                    }

                }
            }





        }
        else
            return;


    }


    public void AddItem(Item item)
    {
        inventoryItemList.Add(item);
        onItemChange.Invoke();
    }

    public void RemoveItem(Item item)
    {
        if (inventoryItemList.Contains(item))
        {
            inventoryItemList.Remove(item);
        }
        else if (hotbarItemList.Contains(item))
        {
            hotbarItemList.Remove(item);
        }
        else if (equipbarItemList.Contains(item))
        {
            equipbarItemList.Remove(item);
        }

        onItemChange.Invoke();
    }

    public bool ContainsItem(string itemName, int amount)
    {
        int itemCounter = 0;

        foreach (Item i in inventoryItemList)
        {
            if (i.name == itemName)
            {
                itemCounter++;
            }
        }

        foreach (Item i in hotbarItemList)
        {
            if (i.name == itemName)
            {
                itemCounter++;
            }
        }

        foreach (Item i in equipbarItemList)
        {
            if (i.name == itemName)
            {
                itemCounter++;
            }
        }

        if (itemCounter >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItems(string itemName, int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            RemoveItemType(itemName);
        }
    }

    public void RemoveItemType(string itemName)
    {
        foreach (Item i in inventoryItemList)
        {
            if (i.name == itemName)
            {
                inventoryItemList.Remove(i);
                return;
            }
        }

        foreach (Item i in hotbarItemList)
        {
            if (i.name == itemName)
            {
                hotbarItemList.Remove(i);
                return;
            }
        }

        foreach (Item i in equipbarItemList)
        {
            if (i.name == itemName)
            {
                equipbarItemList.Remove(i);
                return;
            }
        }
    }
    

    
}
