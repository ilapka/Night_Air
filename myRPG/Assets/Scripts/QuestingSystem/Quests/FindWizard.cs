using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWizard : Quest
{
    void Start()
    {
        QuestName = "В поисках черной магии.";
        Description = "Где то в кладбищенских лесах прячется избушка ворожея. Эйплрил сказала, что он обязательно поможет.";
        ExperienceReward = 20;

        Goals.Add(new InspectGoal(this, "Krest", "Разыщите волшебника.", false));
        
        Goals.ForEach(g => g.Init());
    }
}
