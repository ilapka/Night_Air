using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspectable : Interactable
{
    [SerializeField] string speakerName;
    [SerializeField] string[] questDialogue;
    [SerializeField] string[] afterQuestDialogue;
    
    private bool inspected = false;

    public override void Interact()
    {
        if (!inspected)
        {
            AssignQuest();
        }
        else
        {
            DialogueSystem.Instance.AddNewDialogue(afterQuestDialogue, speakerName);
            TurnOffBacklight();
        }
    }

    private void AssignQuest()
    {
        base.Interact();
        inspected = true;
        UIEventHandler.ObjectInspect(this.gameObject);
        QuestController.Instance.NextQuest();
        DialogueSystem.Instance.AddNewDialogue(questDialogue, speakerName);
    }
}
