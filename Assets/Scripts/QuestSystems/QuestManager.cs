using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestsConfig config;
    public List<Quest> invisibleQuests = new();
    public List<Quest> avalaibleQuests = new();
    public List<Quest> inProgressQuests = new();

    public static QuestManager Instance;

    // Outgoing events
    //public event Action<Quest> QuestBecomeAvalaibled;
    //public event Action<Quest, bool> QuestCompleted;

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

        //QuestBecomeAvalaibled -= EventService.Instance.NewQuestBecomeAvailable;
        //QuestCompleted -= EventService.Instance.QuestCompleted;
    }

    private void Start()
    {
        invisibleQuests.AddRange(config.Data);

        EventService.Instance.GameTimeUpdated += OnGameTimeUpdate;
        EventService.Instance.QuestAssigned += OnQuestAssigned;

        //QuestBecomeAvalaibled += EventService.Instance.NewQuestBecomeAvailable;
        //QuestCompleted += EventService.Instance.QuestCompleted;
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
        string heroName = GameManager.Instance.CurrentHeroNickname;
        Hero hero = HeroManager.Instance.Heroes.FirstOrDefault(_ => _.Nickname == heroName);
        GameTime _gameTime = GameManager.Instance.CurrentTime;
        bool _heroResult = true;
        try
        {
            avalaibleQuests.Remove(_quest);
            _quest.AssignQuestTo(heroName, _heroResult);
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
            //QuestBecomeAvalaibled?.Invoke(_quest);
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
                //QuestCompleted?.Invoke(_quest, false);
                EventService.Instance.QuestCompleted?.Invoke(_quest, false);
            }
        }
    }

    private void CheckInProgressQuestResult()
    {
        for (int i = inProgressQuests.Count - 1; i >= 0; i--)
        {
            var _quest = inProgressQuests[i];

            var timeToNews = _quest.Result ? _quest.SuccessfulNews.timeToNews : _quest.FailureNews.timeToNews;
            if (_quest.AssignedTime + timeToNews >= GameManager.Instance.CurrentTime)
            {
                inProgressQuests.Remove(_quest);
                //QuestCompleted?.Invoke(_quest, _quest.Result);
                EventService.Instance.QuestCompleted?.Invoke(_quest, true);
            }
        }
    }
}