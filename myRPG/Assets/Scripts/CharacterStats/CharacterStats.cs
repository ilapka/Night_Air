using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for all stats of character
/// </summary>

public class CharacterStats
{
    public List<BaseStat> stats = new List<BaseStat>();

    public CharacterStats(int power, int toughness, int attackSpeed, int critСhance)
    {
        stats = new List<BaseStat>
        {
            new BaseStat(BaseStat.BaseStatType.Power, power, "Сила"),
            new BaseStat(BaseStat.BaseStatType.СritСhance, critСhance, "Криты %"),
        };
    }

    public BaseStat GetStat(BaseStat.BaseStatType statType)
    {
        return stats.Find(x => x.StatType == statType);
    }

    /// <summary>
    /// Method for adding a bonus to one or more character stats
    /// </summary>
    /// <param name="statBonuses"></param>
    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }
}
