using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Text Health;
    [SerializeField] Image HealthFill;
    [SerializeField] Text Level;
    [SerializeField] Text questName;
    [SerializeField] RectTransform goalsContentParent;

    private Text goalText;
    private Text[] goalsArray;

    private void Start()
    {
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnPlayerLevelChange += UpdateLevel;
        UIEventHandler.OnQuestUpdate += UpdateQuestQoals;
        UIEventHandler.OnQuestSet += SetQuestGoals;
        goalText = Resources.Load<Text>("UI/Quests/GoalTextHUD");
        Init();
    }

    private void Init()
    {
        UpdateHealth(player.currentHealth, player.maxHealth);
        UpdateLevel();
    }

    void UpdateHealth(int currentHealth, int maxHealth)
    {
        Health.text = currentHealth.ToString();
        HealthFill.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    void UpdateLevel()
    {
        Level.text = player.playerLevel.Level.ToString() + " Level";
    }



    //Quest and Goals display

    void SetQuestGoals(Quest currentQuest)
    {
        if(goalsArray != null)
            ResetQuestGoals();


        if (currentQuest == null)
        {
            questName.text = "";
        }
        else
        {
            questName.text = currentQuest.QuestName;
            goalsArray = new Text[currentQuest.Goals.Count];
            for (int i = 0; i < currentQuest.Goals.Count; i++)
            {
                Text instance = Instantiate(goalText);
                instance.transform.SetParent(goalsContentParent);
                instance.text = currentQuest.Goals[i].Description;
                goalsArray[i] = instance;
            }
        }
    }

    void UpdateQuestQoals(Quest currentQuest)
    {
        for (int i = 0; i < currentQuest.Goals.Count; i++)
        {
            if (currentQuest.Goals[i].Completed)
            {
                goalsArray[i].text = StrikeThrough(currentQuest.Goals[i].Description);
                goalsArray[i].color = Color.gray;
            }
        }
    }

    void ResetQuestGoals()
    {
        for (int i = 0; i < goalsArray.Length; i++)
        {
            Destroy(goalsArray[i].gameObject);
        }
    }

    private string StrikeThrough(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }
}
