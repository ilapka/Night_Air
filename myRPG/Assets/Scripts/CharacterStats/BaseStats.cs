using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// Сlass for one character stat 
/// </summary>

public class BaseStat
{
    public enum BaseStatType { Power, Toughness, AttackSpeed, СritСhance}

    /// <summary>
    /// List of active bonuses
    /// </summary>
    public List<StatBonus> BaseAdditives { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public BaseStatType StatType { get; set; }
    public int BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int FinalValue { get; set; }

    public BaseStat (int baseValue, string statName, string statDescription)
    {
        BaseAdditives = new List<StatBonus>();

        BaseValue = baseValue;
        StatName = statName;
        StatDescription = statDescription;
    }
    
    //constructor for json
    [Newtonsoft.Json.JsonConstructor]
    public BaseStat(BaseStatType statType, int baseValue, string statName)
    {
        BaseAdditives = new List<StatBonus>();

        StatType = statType;
        BaseValue = baseValue;
        StatName = statName;
    }

    /// <summary>
    /// Method for adding bonus to stat
    /// </summary>
    /// <param name="statBonus"></param>
    public void AddStatBonus(StatBonus statBonus)
    {
        BaseAdditives.Add(statBonus);
    }

    //Find - returns the first element that found
    /// <summary>
    /// Method for deleting bonus from stat
    /// </summary>
    /// <param name="statBonus"></param>
    public void RemoveStatBonus(StatBonus statBonus)
    {
        BaseAdditives.Remove(BaseAdditives.Find(x => x.BonusValue == statBonus.BonusValue));
    }

    public int GetCalculatedStatValue()
    {
        FinalValue = 0;
        FinalValue += BaseValue;
        BaseAdditives.ForEach(x => this.FinalValue += x.BonusValue);
        return FinalValue;
    }
}
