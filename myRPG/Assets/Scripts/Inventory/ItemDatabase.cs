using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// Class that loads and stores all existing items from JSON and can gives them by reference value
/// </summary>
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    private List<Item> Items;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        BuildDatabase();
    }

    private void BuildDatabase()
    {
        Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
        #region Check
        /*
        Debug.Log(Items[0].ItemName + " " + Items[0].ActionName);
        Debug.Log(Items[0].Stats[0].StatName + " lvl is " + Items[0].Stats[0].GetCalculatedStatValue());
        Debug.Log(Items[0].Stats[1].StatName + " lvl is " + Items[0].Stats[1].GetCalculatedStatValue());
        Debug.Log(Items[0].Stats[2].StatName + " lvl is " + Items[0].Stats[2].GetCalculatedStatValue());

        Debug.Log(Items[1].ItemName + " " + Items[0].ActionName);
        Debug.Log(Items[1].Stats[0].StatName + " lvl is " + Items[1].Stats[0].GetCalculatedStatValue());
        Debug.Log(Items[1].Stats[1].StatName + " lvl is " + Items[1].Stats[1].GetCalculatedStatValue());
        Debug.Log(Items[1].Stats[2].StatName + " lvl is " + Items[1].Stats[2].GetCalculatedStatValue());

        Debug.Log(Items[2].ItemName + " " + Items[0].ActionName);
        */
        #endregion
    }

    public Item GetItem(string itemSlug)
    {
        foreach (Item item in Items)
        {
            if (item.ObjectSlug == itemSlug)
                return item;
        }
        Debug.LogWarning("Couldn't find item: " + itemSlug);
        return null;
    }
}
