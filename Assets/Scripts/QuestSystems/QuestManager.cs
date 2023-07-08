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

        EventService.Instance.GameTimeUpdated -= OnGameTimeUpdate;
        EventService.Instance.QuestAssigned -= OnQuestAssigned;
    }

    private void Start()
    {
        invisibleQuests.AddRange(config.Data);

        EventService.Instance.GameTimeUpdated += OnGameTimeUpdate;
        EventService.Instance.QuestAssigned += OnQuestAssigned;
    }

    public void OnGameTimeUpdate()
    {
        CheckAvalaibleQuests();
        CheckAvalaibleQuestLifetimeEnd();
        CheckInProgressQuestResult();
    }

    public void OnQuestAssigned(Quest _quest)
    {
        Debug.Log(_quest.Name);
        string _heroName = "Test";
        GameTime _gameTime = new(0, new Vector2Int(0, 0));
        bool _heroResult = true;
        try
        {
            avalaibleQuests.Remove(_quest);
            _quest.AssignQuestTo(_heroName, _heroResult);
            inProgressQuests.Add(_quest);
        }
        catch
        { }
    }

    private void CheckAvalaibleQuests()
    {
        for (int i = invisibleQuests.Count - 1; i >= 0; i--)
        {
            var _quest = invisibleQuests[i];
            if (GameManager.Instance.CurrentTime < _quest.StartTime)
            {
                continue;
            }
            avalaibleQuests.Add(_quest);
            EventService.Instance.NewQuestBecomeAvailable?.Invoke(_quest);
            invisibleQuests.Remove(_quest);
        }
    }

    private void CheckAvalaibleQuestLifetimeEnd()
    {
        for (int i = avalaibleQuests.Count - 1; i >= 0; i--)
        {
            var _quest = avalaibleQuests[i];
            if ((_quest.StartTime + _quest.Lifetime) >= GameManager.Instance.CurrentTime)
            {
                avalaibleQuests.Remove(_quest);
                EventService.Instance.QuestCompleted?.Invoke(_quest, false);
            }
        }
    }

    private void CheckInProgressQuestResult()
    {
        for (int i = inProgressQuests.Count - 1; i >= 0; i--)
        {
            var _quest = inProgressQuests[i];

            var timeToNews = (_quest.Result ? _quest.SuccessfulNews.timeToNews : _quest.FailureNews.timeToNews) + new GameTime(1, Vector2Int.zero);
            if (_quest.AssignedTime + timeToNews >= GameManager.Instance.CurrentTime)
            {
                inProgressQuests.Remove(_quest);
                EventService.Instance.QuestCompleted?.Invoke(_quest, true);
            }
        }
    }
}