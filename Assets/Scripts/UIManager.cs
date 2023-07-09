using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
	[SerializeField] private Image _heroAvatarImg;
	[SerializeField] private TextMeshProUGUI _heroNicknameText;
	[SerializeField] private TextMeshProUGUI _timeText;
	

	private HeroManager _heroManager;
	private EventService _eventService;

	private void Start()
	{
		_heroManager = HeroManager.Instance;
		_eventService = EventService.Instance;
		HandleGameTimeUpdated();
		// Events 
		_eventService.NewHeroComing += HandleNewHeroComing;
		_eventService.GameTimeUpdated += HandleGameTimeUpdated;
	}

	private void HandleNewHeroComing(string heroNickname)
	{
		Hero hero = _heroManager.Heroes.FirstOrDefault(_ => _.Nickname == heroNickname);
		_heroAvatarImg.sprite = hero.CurrentAvatarParts[0].Value;
		GameManager.Instance.CurrentHeroNickname = hero.Nickname;
		_heroNicknameText.text = hero.Nickname;
	}

	private void HandleGameTimeUpdated()
	{
		GameTime currentGameTime = GameManager.Instance.CurrentTime;
		_timeText.text = currentGameTime.ToNiceString();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_eventService.NewHeroComing -= HandleNewHeroComing;
		_eventService.GameTimeUpdated -= HandleGameTimeUpdated;
	}
}