using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct Hero
{
	[Serializable]
	public struct AvatarPart
	{
		[SerializeField] public AvatarPartType Key;
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
		[SerializeField] public int From;
		[SerializeField] public int To;
		[SerializeField] public HeroMood HeroMood;
	}
	[SerializeField] private string _nickname;
	[SerializeField] private QuestMoodBonus[] _questMoodBonuses;
	[SerializeField] private List<HeroMood> _heroMoods;
	public Dictionary<string, int> Bonuses{get; private set;}
	public string Nickname => _nickname;

	public int CurrentMoodScore;
	public HeroMood CurrentHeroMood;
	public List<HeroMood> HeroMoods => _heroMoods;
	public List<AvatarPart> CurrentAvatarParts;

	public void Init()
	{
		Bonuses = new Dictionary<string, int>();
		foreach(var bonus in _questMoodBonuses)
		{
			Bonuses[bonus.QuestName] = bonus.MoodBonus;
		}
	}




}