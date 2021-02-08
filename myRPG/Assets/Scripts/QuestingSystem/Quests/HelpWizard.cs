using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpWizard : Quest
{
    void Start()
    {
        QuestName = "Сделка с дьяволом.";
        Description = "Помогите волшебнику приручить дракона.";
        ItemsReward = new Item[] {
            ItemDatabase.Instance.GetItem("Pistol")
        };
        ExperienceReward = 100;

        Goals.Add(new KillGoal(this, 2, "Поколечить дракона.", false, 0, 2));
        Goals.Add(new InspectGoal(this, "dragon", "Поговорить с ним.", false));

        Goals.ForEach(g => g.Init());
    }
}
