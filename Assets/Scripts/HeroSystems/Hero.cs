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
	public struct HeroBehaviourRange
	{
		[SerializeField] public int From;
		[SerializeField] public int To;
		[SerializeField] public HeroBehaviour HeroBehaviour;
	}
	[SerializeField] private string _nickname;
	[SerializeField] private List<AvatarPart> _avatarParts;
	[SerializeField] private QuestMoodBonus[] _questMoodBonuses;
	[SerializeField] private List<HeroBehaviourRange> _heroBehaviourRanges;

	public Dictionary<string, int> Bonuses{get; private set;}
	public string Nickname => _nickname;
	public List<AvatarPart> AvatarParts => _avatarParts;
	public int CurrentMood;
	public HeroBehaviour CurrentHeroBehaviour;
	public List<HeroBehaviourRange> HeroBehaviourRanges => _heroBehaviourRanges;

	public void Init()
	{
		Bonuses = new Dictionary<string, int>();
		foreach(var bonus in _questMoodBonuses)
		{
			Bonuses[bonus.QuestName] = bonus.MoodBonus;
		}
	}

	
	

}