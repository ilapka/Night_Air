using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance;
<<<<<<< HEAD
    private Quest currentQuest;
=======
    public Quest currentQuest;
>>>>>>> 091093464f7386e88cd320859e5655d38aa244d2
    private Queue<Quest> questsRepository = new Queue<Quest>();
    private float delayNextQuestSet;
    [SerializeField] GameObject questsContener;

    void Start()
    {
        #region Singleton
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        #endregion

        #region Take all quests in order from GameObject "Quests"
        foreach (Quest quest in questsContener.GetComponents<Quest>())
        {
            Debug.Log(quest.QuestName);
            questsRepository.Enqueue(quest);
        }
        #endregion

        currentQuest = new Quest();
        NextQuest(0.5F); //first Quest
    }

    public void NextQuest()
    {
        delayNextQuestSet = 4f;
        StartCoroutine(NextQuestCoroutine());  
    }

    public void NextQuest(float delayNextQuestSet)
    {
        this.delayNextQuestSet = delayNextQuestSet;
        StartCoroutine(NextQuestCoroutine());
    }

    public bool CheckCurrentQuestComplete()
    {
        return currentQuest.Completed;
    }

    private IEnumerator NextQuestCoroutine()
    {
        PreviousQuestComplete();
        yield return new WaitForSeconds(delayNextQuestSet);
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
