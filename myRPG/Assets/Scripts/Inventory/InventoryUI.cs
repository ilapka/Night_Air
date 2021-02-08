using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for UI window "inventory" with  all player items
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [SerializeField] RectTransform inventoryPanel;
    [SerializeField] RectTransform scrollview;
    [SerializeField] RectTransform backPanel;
    private InventoryUIItem itemContainer;
    private bool menuIsActive;
    //Item currentSelectedItem { get; set; }
    
    void Start()
    {
        itemContainer = Resources.Load<InventoryUIItem>("UI/Inventory/ItemContainer");
        UIEventHandler.OnItemAddedToInventoty += ItemAdded;
        inventoryPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            menuIsActive = !menuIsActive;
            inventoryPanel.gameObject.SetActive(menuIsActive);
            backPanel.gameObject.SetActive(menuIsActive);
        }
    }

    /// <summary>
    /// Adding item (container) to the inventory window
    /// </summary>
    /// <param name="item"></param>
    public void ItemAdded(Item item)
    {
        InventoryUIItem emptyItem = Instantiate(itemContainer);
        emptyItem.SetItem(item);
        emptyItem.transform.SetParent(scrollview);
    }
}
