using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Image _heroAvatarImg;
    [SerializeField] private RectTransform _heroAvatarRect;
    //
    [SerializeField] private TextMeshProUGUI _heroNicknameText;
    [SerializeField] private TextMeshProUGUI _timeText;


    private HeroManager _heroManager;
    private EventService _eventService;
    private Vector3 _defaultHeroAvatarPosition;

    private void Start()
    {
        _heroManager = HeroManager.Instance;
        _eventService = EventService.Instance;
        HandleGameTimeUpdated();
        _defaultHeroAvatarPosition = _heroAvatarRect.position;
        // Events 
        _eventService.NewHeroComing += HandleNewHeroComing;
        _eventService.GameTimeUpdated += HandleGameTimeUpdated;
        _eventService.HeroMoodChanged += HandleHeroMoodChanged;
    }

    private void HandleHeroMoodChanged(OnHeroMoodChangedEventArgs obj)
    {
        var hero = obj.Hero;
        if (hero.Nickname != GameManager.Instance.CurrentHeroNickname) return;
        _heroNicknameText.text = hero.Nickname;
        _heroAvatarImg.sprite = hero.CurrentAvatarParts[0].Value;
    }

    private void HandleNewHeroComing(string heroNickname)
    {
        Hero hero = _heroManager.Heroes.FirstOrDefault(_ => _.Nickname == heroNickname);
        GameManager.Instance.CurrentHeroNickname = hero.Nickname;
        _heroNicknameText.text = hero.Nickname;
        // Put hero comming animation here
        _heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x - 700f, 1f).From();

        _heroAvatarImg.sprite = hero.CurrentAvatarParts[0].Value;

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