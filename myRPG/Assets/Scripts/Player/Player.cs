using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage
{
    public CharacterStats CharacterStats { get; set; }
    public PlayerLevel playerLevel { get; set; }
    private List<BaseStat> bonusOnNewLevel = new List<BaseStat>();

    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private int power = 10;
    [SerializeField] private int toughness = 10;
    [SerializeField] private int attackSpeed = 10;
    [SerializeField] private int critChance = 10;

    void Start()
    {
        playerLevel = GetComponent<PlayerLevel>();
        currentHealth = maxHealth;
        CharacterStats = new CharacterStats(power, toughness, attackSpeed, critChance);
        bonusOnNewLevel.Add(new BaseStat(BaseStat.BaseStatType.Power, 5, "Сила"));
        UIEventHandler.OnPlayerLevelChange += LevelChanged;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            Die();
        UIEventHandler.HealthChanged(currentHealth, maxHealth);
    }

    public void TakeHeal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        UIEventHandler.HealthChanged(currentHealth, maxHealth);
    }

    void LevelChanged()
    {
        CharacterStats.AddStatBonus(bonusOnNewLevel);
        UIEventHandler.StatsChanged();
    }

    private void Die()
    {
        Debug.Log("Player dead. Reset health.");
        currentHealth = maxHealth;
        UIEventHandler.HealthChanged(currentHealth, maxHealth);
    }
}
