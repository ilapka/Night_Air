using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSlayer : Quest
{
    [SerializeField] int requiredKillChomperAmount = 6;

    void Start()
    {
        QuestName = "Эйприл в беде.";
        Description = "Помогите девушке-путешествинице посетить могилу предков.";
        ItemsReward = new Item[] {
            ItemDatabase.Instance.GetItem("Pistol")
        };
        ExperienceReward = 100;

        Goals.Add(new KillGoal(this, 0, "Убейте монстров, заселивших кладбище.", false, 0, requiredKillChomperAmount));
        Goals.Add(new CollectionGoal(this, "potion_health", "Найдите зелье лечения.", false, 0, 1));

        Goals.ForEach(g => g.Init());
    }
}
