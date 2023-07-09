using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct Hero
{
    public string HeroBehPatternName => _heroBehPatternName;

    [SerializeField] private string _heroBehPatternName;

    [Serializable]
    public struct Avatar
    {
        [SerializeField] public Sprite Value;
    }
    [Serializable]
    public struct QuestMoodBonus
    {
        [SerializeField] public string QuestName;
        [SerializeField] public int MoodBonus;
    }
    [Serializable]
    public struct HeroMoodRange
    {
        [SerializeField] public HeroMood HeroMood;
    }
    [SerializeField] private string _nickname;
    [SerializeField] private QuestMoodBonus[] _questMoodBonuses;
    [SerializeField] private List<HeroMood> _heroMoods;
    private Dictionary<string, int> _bonuses;


    public Dictionary<string, int> Bonuses
    {
        get
        {
            if (_bonuses != null) return _bonuses;
            _bonuses = new Dictionary<string, int>();
            foreach (var bonus in _questMoodBonuses)
            {
                _bonuses[bonus.QuestName] = bonus.MoodBonus;
            }
            return _bonuses;
        }

    }

    public string Nickname => _nickname;

    public int CurrentMoodScore;
    public HeroMood CurrentHeroMood;
    public List<HeroMood> HeroMoods => _heroMoods;
    public List<Avatar> CurrentAvatarParts;

}