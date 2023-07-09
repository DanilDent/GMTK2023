using System;
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
		EventService.Instance.QuestCompleted += OnQuestCompleted;
		EventService.Instance.QuestAssigned += OnQuestAssigned;
	}



	private void UnsubscribeFromEvents()
	{
		EventService.Instance.QuestCompleted -= OnQuestCompleted;
		EventService.Instance.QuestAssigned -= OnQuestAssigned;
	}
	private void OnQuestAssigned(Quest quest)
	{
		var hero = GetHeroByName(GameManager.Instance.CurrentHeroNickname);
		if(!hero.AvatarOnQuestAssigned.TryGetValue(quest.Name, out var avatar))
		{
			return;
		}
		hero.CurrentAvatarParts = new List<Hero.Avatar>();
		var avatarObj = new Hero.Avatar { Value = avatar };
		hero.CurrentAvatarParts = new List<Hero.Avatar> { avatarObj };
		UpdateHeroMood(hero);
	}
	private void OnQuestCompleted(Quest quest, bool success)
	{
		OnQuestCompleted(quest, quest.HeroName);
	}

	public void OnQuestCompleted(Quest quest, Hero hero)
	{
		if(hero.Bonuses.TryGetValue(quest.Name, out var bonus))
		{
			hero.CurrentMoodScore += bonus;
			UpdateHeroMood(hero);
		}
	}

	public bool IsQuestCompletable(Quest quest, Hero hero)
	{
		if(hero.Bonuses.TryGetValue(quest.Name, out var bonus))
		{
			return bonus > 0;
		}
		return false;
	}

	public void OnQuestCompleted(Quest quest, string heroName)
	{
		Hero hero = GetHeroByName(heroName);
		int bonus;
		if(hero.Bonuses.TryGetValue(quest.Name, out bonus))
		{
			hero.CurrentMoodScore += bonus;
			UpdateHeroMood(hero);
		}
		else
		{
			hero.CurrentMoodScore -= 10;
			UpdateHeroMood(hero);
		}
	}

	public Hero GetHeroByName(string heroName)
	{
		var heroes = Heroes.Where(h => h.Nickname == heroName).ToList();
		if(heroes.Count == 0)
		{
			Debug.Log(heroName);
			throw new Exception();
		}
		return heroes[0];
	}

	private void ChangeHeroMood(Hero hero, HeroMood mood)
	{
		hero.CurrentHeroMood = mood;
		hero.CurrentAvatarParts = mood.AvatarParts;
		var moodChangedEventArgs = new OnHeroMoodChangedEventArgs(hero, mood);
		EventService.Instance.HeroMoodChanged?.Invoke(moodChangedEventArgs);
	}

	public void UpdateHeroMood(Hero hero)
	{
		var behaviour = hero.CurrentHeroMood;
		var moods = hero.HeroMoods;
		foreach(var mood in moods.Where(range => hero.CurrentMoodScore >= range.FromScore && hero.CurrentMoodScore <= range.ToScore))
		{
			behaviour = mood;
			break;
		}
		ChangeHeroMood(hero, behaviour);
	}
}