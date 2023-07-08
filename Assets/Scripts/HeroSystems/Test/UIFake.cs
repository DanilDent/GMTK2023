using System;
using UnityEngine;

public class UIFake : MonoBehaviour
{
	[SerializeField] private FakeTicker _ticker;
	private void Start()
	{
		EventService.Instance.HeroMoodChanged += OnHeroMoodChanged;
	}

	private void OnHeroMoodChanged(OnHeroMoodChangedEventArgs obj)
	{
		Debug.Log(obj.Hero.Nickname + " mood changed to " + obj.Hero.CurrentHeroMood.MoodName );
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			QuestManager.Instance.CheckAvalaibleQuests(_ticker.CurrentTime.Day, (int)_ticker.CurrentTime);
			var quests = QuestManager.Instance.avalaibleQuests;
			if(quests.Count > 0)
			{
				var quest = quests[0];
				var hero = HeroManager.Instance.Heroes[0];
				HeroManager.Instance.OnQuestCompleted(quest, hero);
				QuestManager.Instance.TryQuestAssignTo(hero.Nickname, quest);
			}
		}
	}
}