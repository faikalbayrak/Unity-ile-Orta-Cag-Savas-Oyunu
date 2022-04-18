using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private ItemSlot itemSlot;
    private RectTransform hotbarRect;
    private RectTransform inventoryRect;
    private RectTransform equipbarRect;
    public GameObject previewPrefab;
    private GameObject currentPreview;
    private Image image;
    private Color baseColor;
    private bool isHotbarSlot;
    private bool isEquipbarSlot;
    



    void Start()
    {
        itemSlot = GetComponent<ItemSlot>();
        hotbarRect = GameManager.instance.hotbarTransform as RectTransform;
        inventoryRect = GameManager.instance.inventoryTransform as RectTransform;
        equipbarRect = GameManager.instance.equipbarTransform as RectTransform;
        image = GetComponent<Image>();
        baseColor = image.color;
        isHotbarSlot = RectTransformUtility.RectangleContainsScreenPoint(hotbarRect, transform.position);
        isEquipbarSlot = RectTransformUtility.RectangleContainsScreenPoint(equipbarRect, transform.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       if(itemSlot.Item != null)
        {
            itemSlot.OnCursorExit();
            itemSlot.isBeingDraged = true;

            //change alpha
            var tmpColor = baseColor;
            tmpColor.a = 0.6f;
            image.color = tmpColor;

            currentPreview = Instantiate(previewPrefab, GameManager.instance.mainCanvas);
            currentPreview.GetComponent<Image>().sprite = itemSlot.Item.icon;
            currentPreview.transform.position = transform.position;
        }
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (itemSlot.Item != null)
            currentPreview.transform.position = Input.mousePosition;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (itemSlot.Item != null)
        {
            itemSlot.isBeingDraged = false;
            image.color = baseColor;

            if ((RectTransformUtility.RectangleContainsScreenPoint(hotbarRect, Input.mousePosition) && !isHotbarSlot)
                || (RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, Input.mousePosition) && isHotbarSlot))
            {
                Inventory.instance.SwitchHotbarInventory(itemSlot.Item);
            }

            if ((RectTransformUtility.RectangleContainsScreenPoint(equipbarRect, Input.mousePosition) && !isEquipbarSlot)
                || (RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, Input.mousePosition) && isEquipbarSlot))
            {
                Inventory.instance.SwitchEquipbarInventory(itemSlot.Item);
            }

            if ((RectTransformUtility.RectangleContainsScreenPoint(equipbarRect, Input.mousePosition) && !isEquipbarSlot)
                || (RectTransformUtility.RectangleContainsScreenPoint(hotbarRect, Input.mousePosition) && isEquipbarSlot))
            {
                Inventory.instance.SwitchEquipbarHotbar(itemSlot.Item);
            }
            if ((RectTransformUtility.RectangleContainsScreenPoint(equipbarRect, Input.mousePosition) && !isHotbarSlot)
               || (RectTransformUtility.RectangleContainsScreenPoint(hotbarRect, Input.mousePosition) && isHotbarSlot))
            {
                Inventory.instance.SwitchEquipbarHotbar(itemSlot.Item);
            }


            Destroy(currentPreview);
        }
        
    }
}
