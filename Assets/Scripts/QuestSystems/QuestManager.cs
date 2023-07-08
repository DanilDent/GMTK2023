using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestsConfig config;
    public List<Quest> invisibleQuests = new();
    public List<Quest> avalaibleQuests = new();
    public List<Quest> inProgressQuests = new();

    public static QuestManager Instance;

    // Incoming events
    public event Action<GameTime> OnTimeUpdate;
    public event Action<Quest> OnQuestAssign;
    public event Action<string> OnQuestComplete;

    // Outgoing events
    public event Action<Quest> OnQuestHasBecomeAvalaible;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
        OnTimeUpdate -= CheckAvalaibleQuests;
        OnQuestAssign -= QuestAssign;
        OnQuestComplete -= CompleteQuest;
    }

    private void Start()
    {
        //invisibleQuests.AddRange(config.GetData);
        OnTimeUpdate += CheckAvalaibleQuests;
        OnQuestAssign += QuestAssign;
        OnQuestComplete += CompleteQuest;
    }

    public void CheckAvalaibleQuests(GameTime cuurentTime)
    {
        for (int i = invisibleQuests.Count - 1; i >= 0; i--)
        {
            var _quest = invisibleQuests[i];
            if (cuurentTime < _quest.startTime)
            {
                continue;
            }
            avalaibleQuests.Add(_quest);
            OnQuestHasBecomeAvalaible?.Invoke(_quest);
            invisibleQuests.Remove(_quest);
        }
    }

    public void QuestAssign(Quest _quest)
    {
        string _heroName = "Test";
        try
        {
            avalaibleQuests.Remove(_quest);
            _quest.AssignQuestTo(_heroName);
            inProgressQuests.Add(_quest);
        }
        catch
        {

        }
    }

    public void CompleteQuest(string _name)
    {
        try
        {
            inProgressQuests.Remove(inProgressQuests.Find(quest => quest.Name == _name));
        }
        catch
        {
        }
    }
}
