using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPC : Interactable
{
    protected Animator anim;
    [SerializeField] protected string nameNPC;
    [SerializeField] protected string[] firstDialogue;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void Interact()
    {
        base.Interact();
        DialogueSystem.Instance.AddNewDialogue(firstDialogue, nameNPC, anim);
    }
}
