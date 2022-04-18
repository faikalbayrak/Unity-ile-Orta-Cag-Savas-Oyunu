using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "StatItem", menuName = "Item/StatItem")]
public class StatItem : Item
{
    public StatItemType statItemType;
    public int amount;
    public override void Use()
    {
        base.Use();
        GameManager.instance.OnStateItemUse(statItemType, amount);
        Inventory.instance.RemoveItem(this);
    }
}

public enum StatItemType{
    HealtItem,
    ThirstItem,
    FoodItem,
}
