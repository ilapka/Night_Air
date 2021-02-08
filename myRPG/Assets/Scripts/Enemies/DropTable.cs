using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable
{
    public List<LootDrop> loot;

    public Item GetDrop()
    {
        int roll = Random.Range(0, 101);
        int weigthSum = 0;
        foreach (LootDrop drop in loot)
        {
            weigthSum += drop.Weigth;
            if (roll < weigthSum)
            {
                return ItemDatabase.Instance.GetItem(drop.ItemSlug);
            }
        }
        return null;
    }
}

public class LootDrop
{
    public string ItemSlug { get; set; }
    /// <summary>
    /// Drop chance percentage
    /// </summary>
    public int Weigth { get; set; }

    public LootDrop(string itemSlug, int weigth)
    {
        ItemSlug = itemSlug;
        Weigth = weigth;
    }
}