using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    CharacterStats stats;
    Player player;

    void Start()
    {
        stats = GetComponent<Player>().CharacterStats;
        player = GetComponent<Player>();
    }

    public void ConsumeItem(Item item)
    {
        GameObject itemToSpawn = Instantiate(Resources.Load<GameObject>("Consumables/" + item.ObjectSlug));
        IConsumable currentConsumable = itemToSpawn.GetComponent<IConsumable>();
        currentConsumable.Consume(player);
    }
}
