using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public static HeroManager Instance;
    [SerializeField] private HeroConfig _heroConfig;
    [HideInInspector] public List<Hero> Heroes = new();

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
            UnsubscribeFromEvents();
        }
    }

    private void Start()
    {
        SubscribeToEvents();
        Heroes.AddRange(_heroConfig.GetData);
    }

    private void SubscribeToEvents()
    {
        EventService.Instance.QuestAssigned += OnQuestAssigned;
    }

    public Hero GetCurrentHero()
    {
        return GetHeroByName(GameManager.Instance.CurrentHeroNickname);
    }

    private void UnsubscribeFromEvents()
    {
        EventService.Instance.QuestAssigned -= OnQuestAssigned;
    }
    private void OnQuestAssigned(Quest quest)
    {
        var hero = GetHeroByName(GameManager.Instance.CurrentHeroNickname);
        if (!hero.AvatarOnQuestAssigned.TryGetValue(quest.Name, out var avatar))
        {
            return;
        }
        var avatarObj = new Hero.Avatar { Value = avatar };
        hero.CurrentAvatarParts = new List<Hero.Avatar> { avatarObj };
        EventService.Instance.HeroMoodChanged?.Invoke(new OnHeroMoodChangedEventArgs(hero));
    }

    public bool IsQuestCompletable(Quest quest, Hero hero)
    {
        return hero.CompletableQuests.Contains(quest.Name);
    }


    public Hero GetHeroByName(string heroName)
    {
        var heroes = Heroes.Where(h => h.Nickname == heroName).ToList();
        if (heroes.Count == 0)
        {
            Debug.Log(heroName);
            throw new System.Exception($"Can't find hero with name {heroName}");
        }
        return heroes[0];
    }

}