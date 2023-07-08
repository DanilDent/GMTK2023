using System;
using UnityEngine;

[Serializable]
public struct Quest
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private GameTime minStartTime;
    [SerializeField] private string successfulNews;
    [SerializeField] private string failureNews;
    [SerializeField] private int maxLifetime;
    [SerializeField] private int successfulHPChange;
    [SerializeField] private int failureHPChange;
    private string heroName;

    public GameTime startTime { get => minStartTime; }
    public string Name { get => name; }
    public string Description { get => description; }
    public string SuccessfulNews { get => successfulNews; }
    public string FailureNews { get => failureNews; }
    public int MaxLifetime { get => maxLifetime; }
    public int SuccessfulHPChange { get => successfulHPChange; }
    public int FailureHPChange { get => failureHPChange; }
    public string HeroName { get => heroName; }

    public void AssignQuestTo(string _heroName)
    {
        heroName = _heroName;
    }
}
