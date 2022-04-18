using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool inventoryOpen = false;
    public bool InventoryOpen => inventoryOpen;
    public GameObject inventoryParent,inventoryTab,craftingTab;
    public GameObject inventorySlotPrefab,craftingSlotPrefab;
    public Transform inventoryItemTransform;
    private List<ItemSlot> itemSlotList = new List<ItemSlot>();
    public Transform craftingItemTransform;


    private void Start()
    {
        Inventory.instance.onItemChange += UpdateInventoryUI;
        UpdateInventoryUI();
        SetUpCraftingRecipes();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen)
            {
                CloseInventory();
                Time.timeScale = 1;
                
            }
            else
            {
                OpenInventory();
                Time.timeScale = 0;
            }
        }
    }

    private void SetUpCraftingRecipes()
    {
        List<Item> craftingRecipes = GameManager.instance.craftingRecipes;

        foreach(Item recipe in craftingRecipes)
        {
            GameObject Go = Instantiate(craftingSlotPrefab, craftingItemTransform);
            ItemSlot slot = Go.GetComponent<ItemSlot>();
            slot.AddItem(recipe);
        }
    }

    private void UpdateInventoryUI()
    {
        int currentItemCount = Inventory.instance.inventoryItemList.Count;

        if(currentItemCount > itemSlotList.Count)
        {
            //Add move item slots
            AddItemsSlots(currentItemCount);
        }

        for(int i = 0; i < itemSlotList.Count; i++)
        {
            if(i< currentItemCount)
            {
                itemSlotList[i].AddItem(Inventory.instance.inventoryItemList[i]);
            }
            else
            {
                itemSlotList[i].DestroySlot();
                itemSlotList.RemoveAt(i);
            }
        }
    }

    private void AddItemsSlots(int currentItemCount)
    {
        int amount = currentItemCount - itemSlotList.Count;

        for(int i = 0; i < amount; i++)
        {
            GameObject GO = Instantiate(inventorySlotPrefab, inventoryItemTransform);
            ItemSlot newSlot = GO.GetComponent<ItemSlot>();
            itemSlotList.Add(newSlot);
        }
    }

    private void OpenInventory()
    {
        ChangeCursorState(false);
        inventoryOpen = true;
        inventoryParent.SetActive(true);
    }

    private void CloseInventory()
    {
        ChangeCursorState(true);
        inventoryOpen = false;
        inventoryParent.SetActive(false);
    }

    public void OnCraftingTabCliked()
    {
        craftingTab.SetActive(true);
        inventoryTab.SetActive(false);
    }

    public void OnInventoryTabCliked()
    {
        craftingTab.SetActive(false);
        inventoryTab.SetActive(true);
    }

    private void ChangeCursorState(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
