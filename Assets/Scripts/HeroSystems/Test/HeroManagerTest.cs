using System;
using System.Collections.Generic;
using UnityEngine;

public class HeroManagerTest : MonoBehaviour
{
	private QuestManager _questManager;
	private HeroManager _heroManager;
	private GameManager _gameManager;
	private int _testsPassed = 0;
	private const int TotalTests = 7;
	private HashSet<string> _passedTests = new HashSet<string>();
	private void Start()
	{
		EventService.Instance.NewQuestBecomeAvailable += OnNewQuestBecomeAvailable;
		EventService.Instance.QuestAssigned += OnQuestAssigned;
		EventService.Instance.QuestCompleted += OnQuestCompleted;
		EventService.Instance.GameTimeUpdated += OnGameTimeUpdated;
		EventService.Instance.NewHeroComing += OnNewHeroComing;
		EventService.Instance.HeroMoodChanged += OnHeroMoodChanged;
		_questManager = QuestManager.Instance;
		_heroManager = HeroManager.Instance;
		_gameManager = GameManager.Instance;
	}

	// day starts
	// when quests appears, debug log
	// when character appears, debug log
	// assign quest to character, ok
	// assign quest to character, error
	// when quest completed, debug log
	// when character appears, debug log
	// when quest appears, debug log
	// assign, complete


	private void Update()
	{
		Debug.Log(_gameManager.CurrentTime);
	}
	private void OnHeroMoodChanged(OnHeroMoodChangedEventArgs obj)
	{
		TestPassed("Hero mood changed");
	}
	//ok
	private void OnNewHeroComing(string obj)
	{
		TestPassed("New hero coming");
	}

	//ok
	private void OnGameTimeUpdated()
	{
		TestPassed("Game time updated");
	}

	private void OnQuestCompleted(Quest arg1, bool arg2)
	{
		TestPassed("Quest completed");
	}

	private void OnQuestAssigned(Quest obj)
	{
		TestPassed("Quest assigned");
	}
	//ok
	private void OnNewQuestBecomeAvailable(Quest obj)
	{
		TestPassed("Quest become available");
	}
	private void TestPassed(string message)
	{
		if(_passedTests.Contains(message))
			return;
		_passedTests.Add(message);
		_testsPassed++;
		Debug.Log("Test passed: " + _testsPassed + "/" + TotalTests + " " + message);
	}
}