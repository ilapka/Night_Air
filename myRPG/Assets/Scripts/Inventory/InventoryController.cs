using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Сlass that manages the current items of the player's inventory 
/// </summary>
public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance {get; set;}
    public PlayerWeaponController playerWeaponController;
    public ConsumableController consumableController;
    public InventoryUIDetails inventoryDetailsPanel;
    public List<Item> playerItems = new List<Item>();

    private void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        playerWeaponController = GetComponent<PlayerWeaponController>();
        consumableController = GetComponent<ConsumableController>();
    }

    /// <summary>
    /// Add item to inventory list from database
    /// </summary>
    /// <param name="itemSlug"></param>
    public void GiveItem(string itemSlug)  
    {
        Item item = ItemDatabase.Instance.GetItem(itemSlug);
        playerItems.Add(item);
        Debug.Log(playerItems.Count + " items in inventory. Added: " + itemSlug);
        UIEventHandler.ItemAddedToInventory(item);
    }

    public void GiveItem(Item item)
    {
        playerItems.Add(item);
        Debug.Log(playerItems.Count + " items in inventory. Added: " + item.ItemName);
        UIEventHandler.ItemAddedToInventory(item);
    }

    public void SetItemDetails(Item item, Button selectedButton)
    {
        inventoryDetailsPanel.SetItem(item, selectedButton);
    }

    public void EquipItem(Item itemToEquip)
    {
        playerItems.Remove(itemToEquip);
        playerWeaponController.EquipWeapon(itemToEquip);
    }

    public void ConsumeItem(Item itemToConsume)
    {
        consumableController.ConsumeItem(itemToConsume);
    }
}
