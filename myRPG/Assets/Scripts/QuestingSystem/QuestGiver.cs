using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    public bool AssignedQuest { get; private set; }
    public bool Helped { get; private set; }
    [SerializeField] string[] inProgressDialogue;
    [SerializeField] string[] completeDialogue;
    [SerializeField] string[] afterCompleteDialogue;
    [SerializeField] float delayBeforeQuestSet;
    [SerializeField] float delayBeforeNextQuestSet;

    public override void Interact()
    {
        TurnOffBacklight();
        if (!AssignedQuest && !Helped)
        {
            AssignQuest();
        }
        else if (AssignedQuest && !Helped)
        {
            CheckQuest();
        }
        else
        {
            DialogueSystem.Instance.AddNewDialogue(afterCompleteDialogue, nameNPC);
        }
    }

    private void AssignQuest()
    {
        AssignedQuest = true;
        QuestController.Instance.NextQuest(delayBeforeQuestSet);
        DialogueSystem.Instance.AddNewDialogue(base.firstDialogue, nameNPC, anim);
    }

    private void CheckQuest()
    {
        if (QuestController.Instance.CheckCurrentQuestComplete())
            CompleteQuest();
        else
            DialogueSystem.Instance.AddNewDialogue(inProgressDialogue, nameNPC);
    }

    private void CompleteQuest()
    {
        Helped = true;
        AssignedQuest = false;
        DialogueSystem.Instance.AddNewDialogue(completeDialogue, nameNPC, anim);
        QuestController.Instance.NextQuest(delayBeforeNextQuestSet);
    }
}
