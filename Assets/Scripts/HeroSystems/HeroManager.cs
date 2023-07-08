using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
	public static HeroManager Instance;
	[SerializeField] private HeroConfig _heroConfig;
	public List<Hero> Heroes = new();
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
		Heroes.AddRange(_heroConfig.GetData);
		foreach(var hero in Heroes)
		{
			hero.Init();
		}
	}
	public void OnQuestCompleted(Quest quest, Hero hero)
	{
		int bonus;
		if(hero.Bonuses.TryGetValue(quest.Name, out bonus))
		{
			hero.CurrentMood += bonus;
			UpdateHeroMood(hero);
		}
	}
	public void OnQuestCompleted(Quest quest, string heroName)
	{
		Hero hero = GetHeroByName(heroName);
		int bonus;
		if(hero.Bonuses.TryGetValue(quest.Name, out bonus))
		{
			hero.CurrentMood += bonus;
			UpdateHeroMood(hero);
		}
	}
	public Hero GetHeroByName(string heroName)
	{
		var heroes = Heroes.Where(h => h.Nickname == heroName).ToList();
		if(heroes.Count == 0)
		{
			throw new Exception();
		}
		return heroes[0];
	}
	public void ChangeHeroMood(Hero hero, HeroMood mood)
	{
		hero.CurrentHeroMood = mood;
		hero.CurrentAvatarParts = mood.AvatarParts;
		
	}
	public void UpdateHeroMood(Hero hero)
	{
		var behaviour = hero.CurrentHeroMood;
		var ranges = hero.HeroMoods;
		foreach(var range in ranges.Where(range => hero.CurrentMood >= range.FromScore && hero.CurrentMood <= range.ToScore))
		{
			behaviour = range;
			break;
		}
		ChangeHeroMood(hero, behaviour);
	}
}