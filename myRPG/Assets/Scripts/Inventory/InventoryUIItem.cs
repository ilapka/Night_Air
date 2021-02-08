using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for UI-item (container) in UI-window of inventory
/// </summary>
public class InventoryUIItem : MonoBehaviour
{
    private Item item;
    private Text itemText;
    private Image itemImage;

    /// <summary>
    /// Method sets the UI-container values
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(Item item)
    {
        this.item = item;
        itemText = transform.Find("ItemName").GetComponent<Text>();
        itemImage = transform.Find("ItemIcon").GetComponent<Image>();
        SetupItemValues();
    }

    void SetupItemValues()
    {
        itemText.text = item.ItemName;
        itemImage.sprite = Resources.Load<Sprite>("UI/Inventory/Icons/Items/" + item.ObjectSlug);
    }

    public void OnSelectItemButton()
    {
        InventoryController.Instance.SetItemDetails(item, GetComponent<Button>());
    }
}
