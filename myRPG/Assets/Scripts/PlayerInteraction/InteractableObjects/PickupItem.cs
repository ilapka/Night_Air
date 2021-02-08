using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{
    [SerializeField] string ItemSlug;
    public Item ItemDrop { get; set; }

    public override void Interact()
    {
        if (ItemDrop != null)
        {
            InventoryController.Instance.GiveItem(ItemDrop);
            UIEventHandler.PickUpItem(ItemDrop);
            Destroy(gameObject);
        }
        else
        {
            InventoryController.Instance.GiveItem(ItemSlug);
            UIEventHandler.PickUpItem(ItemDatabase.Instance.GetItem(ItemSlug));
            Destroy(gameObject);
        }
    }
}
