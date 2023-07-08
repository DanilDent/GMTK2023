using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    // Outgoing events
    public event Action<Quest> QuestBecomeAvalaibled;
    public event Action<Quest, bool> QuestCompleted;

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

        QuestBecomeAvalaibled -= EventService.Instance.NewQuestBecomeAvailable;
        QuestCompleted -= EventService.Instance.QuestCompleted;
    }

    private void Start()
    {
        invisibleQuests.AddRange(config.Data);

        EventService.Instance.GameTimeUpdated += OnGameTimeUpdate;
        EventService.Instance.QuestAssigned += OnQuestAssigned;

        QuestBecomeAvalaibled += EventService.Instance.NewQuestBecomeAvailable;
        QuestCompleted += EventService.Instance.QuestCompleted;
    }

    public void OnGameTimeUpdate(GameTime _currentTime)
    {
        CheckAvalaibleQuests(_currentTime);
        CheckAvalaibleQuestLifetimeEnd(_currentTime);
        CheckInProgressQuestResult(_currentTime);
    }

    public void OnQuestAssigned(Quest _quest)
    {
        string _heroName = "Test";
        GameTime _gameTime = new(0, new Vector2Int(0, 0));
        bool _heroResult = true;
        try
        {
            avalaibleQuests.Remove(_quest);
            _quest.AssignQuestTo(_gameTime, _heroName, _heroResult);
            inProgressQuests.Add(_quest);
        }
        catch
        { }
    }


    private  void CheckAvalaibleQuests(GameTime _currentTime)
    {
        for (int i = invisibleQuests.Count - 1; i >= 0; i--)
        {
            var _quest = invisibleQuests[i];
            if (_currentTime < _quest.StartTime)
            {
                continue;
            }
            avalaibleQuests.Add(_quest);
            QuestBecomeAvalaibled?.Invoke(_quest);
            invisibleQuests.Remove(_quest);
        }
    }

    private void CheckAvalaibleQuestLifetimeEnd(GameTime _currentTime)
    {
        for (int i = avalaibleQuests.Count - 1; i >= 0; i--)
        {
            var _quest = avalaibleQuests[i];
            if ((_quest.StartTime + _quest.Lifetime) >= _currentTime)
            {
                avalaibleQuests.Remove(_quest);
                QuestCompleted?.Invoke(_quest, false);
            }
        }
    }
    private void CheckInProgressQuestResult(GameTime _currentTime)
    {
        for (int i = inProgressQuests.Count - 1; i >= 0; i--)
        {
            var _quest = inProgressQuests[i];

            var timeToNews = _quest.Result ? _quest.SuccessfulNews.timeToNews : _quest.FailureNews.timeToNews;
            if (_quest.AssignedTime + timeToNews >= _currentTime)
            {
                inProgressQuests.Remove(_quest);
                QuestCompleted?.Invoke(_quest, _quest.Result);
            }
        }
    }
}
