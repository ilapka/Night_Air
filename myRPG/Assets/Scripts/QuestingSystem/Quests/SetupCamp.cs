using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCamp  : Quest
{
    void Start()
    {
        QuestName = "Новоселье.";
        Description = "Неподалеку виднеется пещера. Лучше взять с собой какое то оружие.";

        ExperienceReward = 20;

        Goals.Add(new CollectionGoal(this, "Katana", "Подберите оружие.", false, 0, 1));
        Goals.Add(new InspectGoal(this, "Cave", "Разбейте лагерь в пещере неподалеку.", false));
        
        Goals.ForEach(g => g.Init());
    }
}
