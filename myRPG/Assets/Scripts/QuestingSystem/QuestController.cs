using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance;
    private Quest currentQuest;
    private Queue<Quest> questsRepository;
    [SerializeField] GameObject questsСontainer;

    void Start()
    {
        #region Singleton
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        #endregion

        currentQuest = new Quest();
        questsRepository = new Queue<Quest>();

        #region Take all quests in order from GameObject "Quests"
        foreach (Quest quest in questsСontainer.GetComponents<Quest>())
        {
            Debug.Log(quest.QuestName);
            questsRepository.Enqueue(quest);
        }
        #endregion
 
        NextQuest(0.5f); //first Quest
    }

    public bool CheckCurrentQuestComplete()
    {
        return currentQuest.Completed;
    }

    public void NextQuest(float delayBeforeNextQuestSet)
    {
        StartCoroutine(NextQuestCoroutine(delayBeforeNextQuestSet));
    }

    private IEnumerator NextQuestCoroutine(float delayBeforeNextQuestSet)
    {
        PreviousQuestComplete();
        yield return new WaitForSeconds(delayBeforeNextQuestSet);
        NextQuestAssign();
    }

    private void NextQuestAssign()
    {
        if (currentQuest != null)
            Destroy(currentQuest);
        currentQuest = questsRepository.Dequeue();
        UIEventHandler.QuestSet(currentQuest);
        currentQuest.ChangeActiveOfQuestObjects();
    }

    private void PreviousQuestComplete()
    {
        if (currentQuest.ItemsReward != null)
        {
            UIEventHandler.QuestComplete(currentQuest);
            currentQuest.GiveReward();
        }
    }
}
