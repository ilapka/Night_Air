using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompletedPanel : MonoBehaviour
{
    [SerializeField] GameObject completedPanel;
    [SerializeField] GameObject backPanel;
    [SerializeField] RectTransform goalContentParent;
    [SerializeField] Text questName;
    [SerializeField] Text questDescription;
    [SerializeField] Text experianceValue;
    [SerializeField] RectTransform rewardContentParent;

    private Text goalText;

    private InventoryUIItem itemContainer;

    void Start()
    {
        UIEventHandler.OnQuestComplete += OpenCompletedPanel;

        goalText = Resources.Load<Text>("UI/Quests/GoalText");
        itemContainer = Resources.Load<InventoryUIItem>("UI/Inventory/ItemContainer");
    }

    public void OpenCompletedPanel(Quest quest)
    {
        backPanel.SetActive(true);
        completedPanel.SetActive(true);
        questName.text = quest.QuestName;
        questDescription.text = quest.Description;
        experianceValue.text = quest.ExperienceReward.ToString();

        foreach (Goal goal in quest.Goals)
        {
            Text instance = Instantiate(goalText);
            instance.transform.SetParent(goalContentParent);
            instance.text = goal.Description;
        }

        foreach (Item item in quest.ItemsReward)
        {
            InventoryUIItem instance = Instantiate(itemContainer);
            instance.SetItem(item);
            instance.transform.SetParent(rewardContentParent);
        }
    }

    public void ClosePanel()
    {
        completedPanel.SetActive(false);
        backPanel.SetActive(false);

        int childCountGoals = goalContentParent.transform.childCount;
        for (int i = 0; i < childCountGoals; i++)
        {
            Destroy(goalContentParent.GetChild(i).gameObject);
        }

        int childCountRewards = rewardContentParent.transform.childCount;
        for (int i = 0; i < childCountRewards; i++)
        {
            Destroy(rewardContentParent.GetChild(i).gameObject);
        }
    }
}
