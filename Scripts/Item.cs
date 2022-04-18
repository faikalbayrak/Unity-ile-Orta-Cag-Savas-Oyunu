using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item",menuName ="Item/baseItem")]
public class Item : ScriptableObject
{
    new public string name = "Default Item";
    public GameObject itemObj;
    public ItemType itemType;
    public Sprite icon = null;
    public string itemDescription = "Used for crafting";

    public virtual void Use()
    {
        Debug.Log("Using "+ name);
    }

    public virtual string GetItemDescription()
    {
        return itemDescription;
    }

    public enum ItemType
    {
        WeaponItem,
        AmmunitionItem,
        ShieldItem,
        ArmorItem,
        HelmetItem,
        CraftingItem,
        StatItem
    }
}
