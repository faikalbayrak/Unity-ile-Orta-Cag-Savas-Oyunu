using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public List<Item> itemList = new List<Item>();
    public List<Item> craftingRecipes = new List<Item>();

    public Transform canvas;
    public GameObject itemInfoPrefab;
    private GameObject currentItemInfo = null;
    public GameObject inventoryInfo;
    Animator inventoryInfoAnim;
    Text inventoryInfoText;

    public Transform mainCanvas;
    public Transform hotbarTransform;
    public Transform inventoryTransform;
    public Transform equipbarTransform;

    private void Start()
    {
        inventoryInfoAnim = inventoryInfo.GetComponent<Animator>();
        inventoryInfoText = inventoryInfo.transform.GetChild(0).GetComponent<Text>();
    }

    public void ViewInventoryInfo(string message,Color color)
    {
        inventoryInfoText.text = message;
        inventoryInfoText.color = color;

        inventoryInfoAnim.SetTrigger("getInfo");
        inventoryInfoAnim.SetBool("isViewing", true);
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Item newItem = itemList[Random.Range(0, itemList.Count)];
            Inventory.instance.AddItem(Instantiate(newItem));
        }
        inventoryInfoAnim.SetBool("isViewing", false);
    }

    public void OnStateItemUse(StatItemType itemType, int amount)
    {
        Debug.Log("Consuming " + itemType + " Add amount: " + amount);
    }

    public void DisplayItemInfo(string itemName,string itemDescription, Vector2 buttonPos)
    {
        if(currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }

        buttonPos.x += 150;
        buttonPos.y += 60;

        currentItemInfo = Instantiate(itemInfoPrefab, buttonPos, Quaternion.identity, canvas);
        currentItemInfo.GetComponent<ItemInfo>().SetUp(itemName, itemDescription);
    }

    public void DestroyItemInfo()
    {
        if(currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }
    }
}
