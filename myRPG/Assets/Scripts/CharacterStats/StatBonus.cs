using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for adding some bonus 
/// </summary>

public class StatBonus
{
    public int BonusValue { get; set; }

    public StatBonus(int bonusValue)
    {
        BonusValue = bonusValue;
    }
}
