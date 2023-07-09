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

    public void OnQuestAssigned(Quest quest)
    {
        Debug.Log(quest.Name);
        string heroName = GameManager.Instance.CurrentHeroNickname;
        Hero hero = HeroManager.Instance.Heroes.FirstOrDefault(_ => _.Nickname == heroName);
        GameTime gameTime = GameManager.Instance.CurrentTime;
        bool heroResult = HeroManager.Instance.IsQuestCompletable(quest, hero);
        try
        {
            avalaibleQuests.Remove(quest);
            quest.AssignQuestTo(heroName, heroResult);
            inProgressQuests.Add(quest);
        }
        catch
        {
        }
    }

    private void CheckAvalaibleQuests()
    {
        for (int i = invisibleQuests.Count - 1; i >= 0; i--)
        {
            var _quest = invisibleQuests[i];
            Debug.Log(GameManager.Instance.CurrentTime);
            Debug.Log(_quest.StartTime);
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
            if ((_quest.StartTime + _quest.Lifetime) <= GameManager.Instance.CurrentTime && !inProgressQuests.Contains(_quest))
            {
                avalaibleQuests.Remove(_quest);
                EventService.Instance.QuestLifetimeEnded?.Invoke(_quest);
                //EventService.Instance.QuestCompleted?.Invoke(_quest, false);
            }
        }
    }

    private void CheckInProgressQuestResult()
    {
        for (int i = inProgressQuests.Count - 1; i >= 0; i--)
        {
            var _quest = inProgressQuests[i];

            var timeToNews = (_quest.Result ? _quest.SuccessfulNews.timeToNews : _quest.FailureNews.timeToNews)/* + new GameTime(1, Vector2Int.zero)*/;
            if (_quest.AssignedTime + timeToNews <= GameManager.Instance.CurrentTime)
            {
                inProgressQuests.Remove(_quest);
                EventService.Instance.QuestCompleted?.Invoke(_quest, _quest.Result);
            }
        }
    }
}