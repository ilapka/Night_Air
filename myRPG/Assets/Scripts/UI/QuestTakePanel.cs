using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTakePanel : MonoBehaviour
{
    [SerializeField] GameObject takePanel;
    [SerializeField] GameObject backPanel;
    [SerializeField] RectTransform goalContentParent;
    [SerializeField] Text takeQuestName;
    [SerializeField] Text takeQuestDescription;

    private Text goalText;

    void Start()
    {
        UIEventHandler.OnQuestSet += OpenTakePanel;
        goalText = Resources.Load<Text>("UI/Quests/GoalText");
    }

    public void OpenTakePanel(Quest quest)
    {
        if (quest != null)
        {
            backPanel.SetActive(true);
            takePanel.SetActive(true);
            Debug.Log(quest.QuestName);
            Debug.Log(quest.Description);
            takeQuestName.text = quest.QuestName;
            takeQuestDescription.text = quest.Description;

            foreach (Goal goal in quest.Goals)
            {
                Text instance = Instantiate(goalText);
                instance.transform.SetParent(goalContentParent);
                instance.text = goal.Description;
            }
        }
    }

    public void ClosePanel()
    {
        takePanel.SetActive(false);
        backPanel.SetActive(false);

        int childCountGoals = goalContentParent.transform.childCount;
        for (int i = 0; i < childCountGoals; i++)
        {
            Destroy(goalContentParent.GetChild(i).gameObject);
        }
    }
}
