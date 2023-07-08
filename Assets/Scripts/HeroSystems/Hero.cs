using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "Configs/Hero", order = 0)]
public class Hero : ScriptableObject
{
	[Serializable]
	public struct AvatarPart
	{
		[SerializeField] private AvatarPartType _key;
		[SerializeField] private Sprite _value;
	}
	[Serializable]
	public struct QuestMoodBonus
	{
		//[SerializeField] private Quest _quest;
		[SerializeField] private int _moodBonus;
	}
	[Serializable]
	public struct HeroBehaviourRange
	{
		[SerializeField] private int _from;
		[SerializeField] private int _to;
		[SerializeField] private HeroBehaviour _heroBehaviour;
	}
	[SerializeField] private string _nickname;
	[SerializeField] private AvatarPart[] _avatarParts;
	[SerializeField] private QuestMoodBonus[] _questMoodBonuses;
	[SerializeField] private HeroBehaviourRange[] _heroBehaviourRanges;



}