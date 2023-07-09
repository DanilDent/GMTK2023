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

    [SerializeField]
    private float _animationPercentForComing = 0.2f;

    [SerializeField]
    private float _animationPercentForLeaving = 0.2f;

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
        _eventService.HeroLeaving += HandleHeroLeaving;
        _eventService.HeroLeftFromScreen += HandleHeroLeftFromScreen;
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

        Debug.Log("Появление");
        _heroAvatarRect.position = _defaultHeroAvatarPosition;
        _heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x - _animationPercentForComing * Screen.width, 1f).From();
        _heroAvatarImg.DOFade(1, 1);
        _heroAvatarImg.sprite = hero.CurrentAvatarParts[0].Value;
    }

    private void HandleHeroLeaving()
    {
        var diagManager = DialogManager.Instance;

        Sequence seq = DOTween.Sequence();

        diagManager.DisplayBlank();
        seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x + _animationPercentForLeaving * Screen.width, 1f));

        _heroAvatarImg.DOFade(0, 1);
        seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x, 1f)).OnComplete(() =>
        {
            diagManager.DisplayHello();
        });
        _heroAvatarImg.DOFade(1, 1);
    }

    private void HandleHeroLeftFromScreen()
    {
        Debug.Log("Исчезновение");
        Sequence seq = DOTween.Sequence();
        _heroAvatarImg.DOFade(0, 1);
        seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x + _animationPercentForLeaving * Screen.width, 1f)).OnComplete(() =>
        {
            GameManager.Instance.CurrentHeroNickname = string.Empty;
        });
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
        _eventService.HeroMoodChanged -= HandleHeroMoodChanged;
        _eventService.HeroLeaving -= HandleHeroLeaving;
    }
}