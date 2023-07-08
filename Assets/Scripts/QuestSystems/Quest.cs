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
    private GameTime assignedTime;

    public GameTime StartTime { get => minStartTime; }
    public GameTime Lifetime { get => maxLifetime; }
    public GameTime AssignedTime { get => assignedTime; }
    public News SuccessfulNews { get => successfulNews; }
    public News FailureNews { get => failureNews; }
    public string Name { get => name; }
    public string Description { get => description; }
    public string HeroName { get => heroName; }
    public int SuccessfulHPChange { get => successfulHPChange; }
    public int FailureHPChange { get => failureHPChange; }
    public bool Result { get => result; }

    public void AssignQuestTo(GameTime _currentTime, string _heroName, bool _isSuccessful)
    {
        heroName = _heroName;
        result = _isSuccessful;
        assignedTime = _currentTime;
    }
}
