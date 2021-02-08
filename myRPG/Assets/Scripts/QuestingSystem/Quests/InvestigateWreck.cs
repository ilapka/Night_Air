using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateWreck : Quest
{
    void Start()
    {
        QuestName = "Кораблекрушение.";
        Description = "Ваш корабль потерпел крушение. Осмотрите место падения.";
        ExperienceReward = 20;

        Goals.Add(new InspectGoal(this, "SpaceShip", "Осмотрите место падения корабля.", false));
        
        Goals.ForEach(g => g.Init());
    }
}
