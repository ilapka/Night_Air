using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// inventory item
/// </summary>
public class Item
{
    public enum ItemTypes { Weapon, Consumable, Quest };
    public List<BaseStat> Stats { get; set; }
    public string ObjectSlug { get; set; }
    public string Description { get; set; }
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))] //for deserialize ItemTypes from string to Enum
    public ItemTypes ItemType { get; set; }
    public string ActionName { get; set; }
    public string ItemName { get; set; }
    public bool ItemModifier { get; set; }

    public Item(List<BaseStat> _Stats, string _ObjectSlug)
    {
        Stats = _Stats;
        ObjectSlug = _ObjectSlug;
    }

    [Newtonsoft.Json.JsonConstructor]
    public Item(List<BaseStat> _Stats, string _ObjectSlug, string _Description, ItemTypes _ItemType,string _ActionName, string _ItemName, bool _ItemModifier)
    {
        Stats = _Stats;
        ObjectSlug = _ObjectSlug;
        Description = _Description;
        ItemType = _ItemType;
        ActionName = _ActionName;
        ItemName = _ItemName;
        ItemModifier = _ItemModifier;
    }
}
