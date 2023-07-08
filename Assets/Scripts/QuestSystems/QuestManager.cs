using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestConfig config;
    public List<Quest> invisibleQuests = new();
    public List<Quest> avalaibleQuests = new();
    public List<Quest> inProgressQuests = new();

    public static QuestManager Instance;

    private void Awake()
    {
        if(Instance == null)
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
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        invisibleQuests.AddRange(config.GetData);
        CheckAvalaibleQuests(0, 0);
        //if(TryQuestAssignTo("Test", avalaibleQuests[0]))
        //{
        //    TryCompleteQuest(inProgressQuests[0].Name);
        //}
    }

    public void CheckAvalaibleQuests(int _currentDay, int _currentTime)
    {
        for (int i = invisibleQuests.Count - 1; i >= 0; i--)
        {
            var _quest = invisibleQuests[i];
            if (_currentDay < _quest.StartDay)
            {
                continue;
            }

            var _time = _quest.MinStartTime.x * 60 + _quest.MinStartTime.y;
            if (_currentTime < _time)
            {
                continue;
            }
            avalaibleQuests.Add(_quest);
            invisibleQuests.Remove(_quest);
        }
    }

    public bool TryQuestAssignTo(string _heroName, Quest _quest)
    {
        try 
        {
            avalaibleQuests.Remove(_quest);
            _quest.AssignQuestTo(_heroName);
            inProgressQuests.Add(_quest);
        }
        catch
        {
            return false;
        }
        return true;        
    }

    public bool TryCompleteQuest(string _name)
    {
        try
        {
            inProgressQuests.Remove(inProgressQuests.Find(quest => quest.Name == _name));
        }
        catch
        {
            return false;
        }
        return true;
    }
}
