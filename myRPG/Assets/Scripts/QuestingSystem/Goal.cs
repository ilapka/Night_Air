using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }


    public virtual void Init()
    {
        //defaoult init staff for inheritors
    }

    public void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        Completed = true;
        Quest.CheckGoals();
        Debug.Log($"Goal ({Description}) marked as completed!");
    }

    public virtual void OnDestroy()
    {
        //some action on destroy for inheritors
    }
}
