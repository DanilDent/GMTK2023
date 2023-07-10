using System;
using UnityEngine;

[Serializable]
public struct Quest
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private GameTime minStartTime;
    [SerializeField] private News successfulNews;
    [SerializeField] private News failureNews;
    [SerializeField] private GameTime maxLifetime;
    [SerializeField] private int successfulHPChange;
    [SerializeField] private int failureHPChange;

    private string heroName;
    private bool result;
    private GameTime timeBecomeAvalaible;
    private GameTime assignedTime;

    public Quest(GameTime _minStart, GameTime _maxLifetime, News _successfulNews, News _failureNews, bool _result)
    {
        name = $"Test {_minStart.Day}d {_minStart.Hours}h {_minStart.Minutes}m";
        description = "";
        minStartTime = _minStart;
        maxLifetime = _maxLifetime;
        successfulNews = _successfulNews;
        failureNews = _failureNews;
        successfulHPChange = 0;
        failureHPChange = 0;
        heroName = "";
        result = _result;
        assignedTime = new GameTime();
        timeBecomeAvalaible = new GameTime();
    }

    public GameTime StartTime { get => minStartTime; }
    public GameTime Lifetime { get => maxLifetime; }
    public GameTime AssignedTime { get => assignedTime; }
    public GameTime TimeBecomeAvalaible { get => timeBecomeAvalaible; }
    public News SuccessfulNews { get => successfulNews; }
    public News FailureNews { get => failureNews; }
    public string Name { get => name; }
    public string Description { get => description; }
    public string HeroName { get => heroName; }
    public int SuccessfulHPChange { get => successfulHPChange; }
    public int FailureHPChange { get => failureHPChange; }
    public bool Result { get => result; }

    public void AssignQuestTo(string _heroName, bool _isSuccessful)
    {
        heroName = _heroName;
        result = _isSuccessful;
        assignedTime = GameManager.Instance.CurrentTime;
    }
    public void SetTimeBecomeAvalaible()
    {
        timeBecomeAvalaible = GameManager.Instance.CurrentTime;
    }

    public override bool Equals(object obj)
    {
        if (obj is Quest quest)
        {
            return name == quest.name;
        }
        return base.Equals(obj);
    }
}
