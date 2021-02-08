using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : MonoBehaviour, IConsumable
{
    [SerializeField] int ReplenishedHealth = 50;

    public void Consume(Player player)
    {
        player.TakeHeal(ReplenishedHealth);
        Debug.Log($"Heal - {ReplenishedHealth} units");
        Destroy(gameObject);
    }
}