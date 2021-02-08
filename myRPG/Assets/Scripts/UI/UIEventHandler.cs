using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler OnItemAddedToInventoty;
    public static event ItemEventHandler OnPickUpItem;
    public static event ItemEventHandler OnItemEquipped;

    public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);
    public static event PlayerHealthEventHandler OnPlayerHealthChanged;

    public delegate void StatsEventHandler();
    public static event StatsEventHandler OnStatsChanged;

    public delegate void PlayerLevelEventHandler();
    public static event PlayerLevelEventHandler OnPlayerLevelChange;

    public delegate void QuestEventHandler(Quest quest);
    public static event QuestEventHandler OnQuestSet;
    public static event QuestEventHandler OnQuestUpdate;
    public static event QuestEventHandler OnQuestComplete;

    public delegate void InspectObjectHendler(GameObject gameObject);
    public static event InspectObjectHendler OnObjectInspect;


    public static void ItemAddedToInventory(Item item)
    {
        OnItemAddedToInventoty?.Invoke(item);
    }

    public static void PickUpItem(Item item)
    {
        OnPickUpItem?.Invoke(item);
    }

    public static void ItemEquipped(Item item)
    {
        OnItemEquipped?.Invoke(item);
    }

    public static void HealthChanged(int currentHealth, int maxHealth)
    {
        OnPlayerHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public static void StatsChanged()
    {
        OnStatsChanged?.Invoke();
    }

    public static void PlayerLevelChanged()
    {
        OnPlayerLevelChange?.Invoke();
    }

    public static void QuestUpdate(Quest quest)
    {
        OnQuestUpdate?.Invoke(quest);
    }

    public static void QuestSet(Quest quest)
    {
        OnQuestSet?.Invoke(quest);
    }

    public static void QuestComplete(Quest quest)
    {
        OnQuestComplete?.Invoke(quest);
    }

    public static void ObjectInspect(GameObject gameObject)
    {
        OnObjectInspect?.Invoke(gameObject);
    }
}
