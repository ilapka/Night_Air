using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkWithWizard : Quest
{
    [SerializeField] int requiredKillSkeletonAmount = 6;

    void Start()
    {
        QuestName = "Добиться аудиенции.";
        Description = "Чернокнижник недоброжелателен.";
        ExperienceReward = 20;

        Goals.Add(new KillGoal(this, 1, "Убейте скелетов.", false, 0, requiredKillSkeletonAmount));
        Goals.Add(new InspectGoal(this, "Wizard", "Попросите волшебника помочь.", false));
        
        Goals.ForEach(g => g.Init());
    }
}
