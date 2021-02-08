using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public Item[] ItemsReward { get; set; }
    public bool Completed { get; set; }

    public GameObject[] ObjectsToChangeActive;

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        UIEventHandler.QuestUpdate(this);
    }

    public void GiveReward()
    {
        if (ItemsReward != null)
        {
            for (int i = 0; i < ItemsReward.Length; i++)
            {
                InventoryController.Instance.GiveItem(ItemsReward[i]);
            }
        }
    }

    public void ChangeActiveOfQuestObjects()
    {
        for (int i = 0; i < ObjectsToChangeActive.Length; i++)
        {
            ObjectsToChangeActive[i].SetActive(!ObjectsToChangeActive[i].activeInHierarchy);
        }
    }

    private void OnDestroy()
    {
        foreach (Goal goal in Goals)
        {
            goal.OnDestroy();
        }
    }
}
