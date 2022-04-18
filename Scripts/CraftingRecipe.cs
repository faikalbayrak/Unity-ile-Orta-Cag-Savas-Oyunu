using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CraftingRecipe/baseRecipe")]
public class CraftingRecipe : Item
{
    public Item result;
    public Ingredient[] ingredients;
    

    public void Awake()
    {
        
    }

    private bool CanCraft()
    {
        foreach(Ingredient ingredient in ingredients)
        {
            bool constainsCurrentIngredient = Inventory.instance.ContainsItem(ingredient.item.name, ingredient.amount);
            if (!constainsCurrentIngredient)
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveIngrediensFromInventory()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            Inventory.instance.RemoveItems(ingredient.item.name, ingredient.amount);

        }
    }

    
    public override void Use()
    {
        if (CanCraft())
        {
            RemoveIngrediensFromInventory();

            Inventory.instance.AddItem(result);

            GameManager.instance.ViewInventoryInfo("You Just Crafted : " + result.name,Color.green);
            
            
            //Debug.Log("You Just Crafted : " + result.name);
        }
        else
        {
            GameManager.instance.ViewInventoryInfo("You Don't Have Enough Ingredients to Craft: " + result.name, Color.red);
            //Debug.Log("You Don't Have Enough Ingredients to Craft: " + result.name);
        }
    }
    


    public override string GetItemDescription()
    {
        string itemIngredients = "";
        foreach (Ingredient ingredient in ingredients)
        {
            itemIngredients += "- " + ingredient.amount + " " + ingredient.item.name + "\n";

        }
        return itemIngredients;
    }


    [System.Serializable]
    public class Ingredient
    {
        public Item item;
        public int amount;
    }
}
