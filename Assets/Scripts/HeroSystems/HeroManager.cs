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
	public void ApplyQuestBonuses(Quest quest, Hero hero)
	{
		int bonus;
		if(hero.Bonuses.TryGetValue(quest.Name, out bonus))
		{
			hero.CurrentMood += bonus;
			UpdateHeroBehaviour(hero);
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
	public void AddAvatarPartToHero(Hero.AvatarPart part, Hero hero)
	{
		hero.AvatarParts.Add(part);
	}
	public void ChangeHeroBehaviour(Hero hero, HeroBehaviour behaviour)
	{
		hero.CurrentHeroBehaviour = behaviour;
	}
	public void UpdateHeroBehaviour(Hero hero)
	{
		var behaviour = hero.CurrentHeroBehaviour;
		var ranges = hero.HeroBehaviourRanges;
		foreach(var range in ranges.Where(range => hero.CurrentMood >= range.From && hero.CurrentMood <= range.To))
		{
			behaviour = range.HeroBehaviour;
			break;
		}
		ChangeHeroBehaviour(hero, behaviour);
	}
}