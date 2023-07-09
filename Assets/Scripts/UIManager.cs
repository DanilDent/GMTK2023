﻿using DG.Tweening;
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
    [SerializeField] private Image _cityBackgrouImg;
    [SerializeField] private Image _cityBackgroundNpcImg;

    [SerializeField] private Sprite BaseCity;
    [SerializeField] private Sprite HappyCity;
    [SerializeField] private Sprite SadCity;

    [SerializeField] private Sprite BaseCityNpc;
    [SerializeField] private Sprite HappyCityNpc;

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
        _eventService.CityStatusChange += HandleCityStatusChange;
    }

    private void HandleCityStatusChange(int status)
    {
        if (status == 0)
        {
            _cityBackgrouImg.sprite = BaseCity;
            _cityBackgroundNpcImg.sprite = BaseCityNpc;
            _cityBackgroundNpcImg.gameObject.SetActive(true);
        }
        else if (status == 1)
        {
            _cityBackgrouImg.sprite = HappyCity;
            _cityBackgroundNpcImg.sprite = HappyCityNpc;
            _cityBackgroundNpcImg.gameObject.SetActive(true);
        }
        else if (status == -1)
        {
            _cityBackgrouImg.sprite = SadCity;
            _cityBackgroundNpcImg.gameObject.SetActive(false);
        }
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

        _heroAvatarRect.position = _defaultHeroAvatarPosition;
        _heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x - 700f, 1f).From();
        _heroAvatarImg.DOFade(1, 1);
        _heroAvatarImg.sprite = hero.CurrentAvatarParts[0].Value;
    }

    private void HandleHeroLeaving()
    {
        var diagManager = DialogManager.Instance;

        Sequence seq = DOTween.Sequence();
        diagManager.DisplayBlank();
        seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x + 800f, 1f));

        _heroAvatarImg.DOFade(0, 1);
        seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x, 1f)).OnComplete(() =>
        {
            diagManager.DisplayHello();
        });
        _heroAvatarImg.DOFade(1, 1);
    }

    private void HandleHeroLeftFromScreen()
    {
        Sequence seq = DOTween.Sequence();
        _heroAvatarImg.DOFade(0, 1);
        seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x + 800, 1f)).OnComplete(() =>
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
        _eventService.CityStatusChange -= HandleCityStatusChange;
    }
}