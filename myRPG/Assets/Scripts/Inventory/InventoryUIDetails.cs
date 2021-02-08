using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Сlass for the panel with a description of the item and a button for its use 
/// </summary>
public class InventoryUIDetails : MonoBehaviour
{
    [SerializeField] Text statItemText;

    private Item item;
    private Button selectedItemButton, interactItemButton;
    private Text itemNameText, itemDescriptionText, interactItemButtonText;

    private void Start()
    {
        InitUIDetails();
    }

    void InitUIDetails()
    {
        gameObject.SetActive(false);
        itemNameText = transform.Find("ItemName").GetComponent<Text>();
        itemDescriptionText = transform.Find("ItemDescription").GetComponent<Text>();
        interactItemButton = transform.Find("Button").GetComponent<Button>();
        interactItemButtonText = interactItemButton.transform.Find("Text").GetComponent<Text>();
    }

    public void SetItem(Item item, Button selectedButton)
    {
        gameObject.SetActive(true);
        statItemText.text = "";
        if (item.Stats != null)
        {
            foreach(BaseStat stat in item.Stats)
            {
                statItemText.text += stat.StatName + ": " + stat.BaseValue + "\n";
            }
        }
        interactItemButton.onClick.RemoveAllListeners();
        this.item = item;
        selectedItemButton = selectedButton;
        itemNameText.text = item.ItemName;
        itemDescriptionText.text = item.Description;
        interactItemButtonText.text = item.ActionName;
        interactItemButton.onClick.AddListener(OnItemInteract);
    }

    public void OnItemInteract()
    {
        if (item.ItemType == Item.ItemTypes.Consumable)
        {
            InventoryController.Instance.ConsumeItem(item);
            Destroy(selectedItemButton.gameObject);
        }
        else if (item.ItemType == Item.ItemTypes.Weapon)
        {
            InventoryController.Instance.EquipItem(item);
            Destroy(selectedItemButton.gameObject);
        }
        RevomeItem();
    }

    public void RevomeItem()
    {
        item = null;
        gameObject.SetActive(false);
    }
}
