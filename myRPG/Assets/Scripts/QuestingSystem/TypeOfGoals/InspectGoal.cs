using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectGoal : Goal
{
    public string ObjectName { get; set; }

    public InspectGoal(Quest quest, string objectName, string description, bool completed)
    {
        this.Quest = quest;
        this.ObjectName = objectName;
        this.Description = description;
        this.Completed = completed;
    }

    public override void Init()
    {
        base.Init();
        UIEventHandler.OnObjectInspect += InspectObject;
    }

    void InspectObject(GameObject gameObject)
    {
        if (ObjectName == gameObject.name)
        {
            Complete();
        }
    }

    public override void OnDestroy()
    {
        UIEventHandler.OnObjectInspect -= InspectObject;
    }
}
