using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public struct Hero
{
    public string HeroBehPatternName => _heroBehPatternName;


    [SerializeField] private string _heroBehPatternName;
    [SerializeField] private List<ShopList> _shopLists;

    [Serializable]
    public struct Avatar
    {
        [SerializeField] public Sprite Value;
    }
    [Serializable]
    public struct CompletableQuest
    {
        [SerializeField] public string QuestName;
        [SerializeField, InspectorName("New Avatar")] public Sprite AvatarOnQuestAssigned;
    }
    [Serializable]
    public struct HeroMoodRange
    {
        [SerializeField] public HeroMood HeroMood;
    }
    [SerializeField] private string _nickname;
    [FormerlySerializedAs("_questMoodBonuses")]
    [SerializeField] private CompletableQuest[] _completableQuestsSO;
    //[SerializeField] private List<HeroMood> _heroMoods;
    private HashSet<string> _completableQuests;
    private Dictionary<string, Sprite> _avatarOnQuestAssigned;
    private Queue<ShopList> _shopListsQueue;
    public string Nickname => _nickname;

    //public int CurrentMoodScore;
    //public HeroMood CurrentHeroMood;

    public List<Avatar> CurrentAvatarParts;

    public Queue<ShopList> ShopLists
    {
        get
        {
            if (_shopListsQueue != null) return _shopListsQueue;
            _shopListsQueue = new Queue<ShopList>();
            if (_shopLists == null || _shopLists.Count == 0)
            {
                _shopListsQueue.Enqueue(ShopList.GetDefault());
                return _shopListsQueue;
            }
            foreach (var shopList in _shopLists)
            {
                _shopListsQueue.Enqueue(shopList);
            }
            return _shopListsQueue;
        }
    }

    public HashSet<string> CompletableQuests
    {
        get
        {
            if (_completableQuests != null) return _completableQuests;
            _completableQuests = new HashSet<string>();
            foreach (var bonus in _completableQuestsSO)
            {
                _completableQuests.Add(bonus.QuestName);
            }
            return _completableQuests;
        }

    }

    public Dictionary<string, Sprite> AvatarOnQuestAssigned
    {
        get
        {
            if (_avatarOnQuestAssigned != null) return _avatarOnQuestAssigned;
            _avatarOnQuestAssigned = new Dictionary<string, Sprite>();
            foreach (var bonus in _completableQuestsSO)
            {
                if (bonus.AvatarOnQuestAssigned != null)
                {
                    _avatarOnQuestAssigned[bonus.QuestName] = bonus.AvatarOnQuestAssigned;
                }
            }
            return _avatarOnQuestAssigned;
        }
    }




}