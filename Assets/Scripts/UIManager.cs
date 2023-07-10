using DG.Tweening;
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
    private Vector3 _defaultHeroAvatarPosition;
    [SerializeField] private Image _cityBackgrouImg;
    [SerializeField] private Image _cityBackgroundNpcImg;

    [SerializeField] private Sprite BaseCity;
    [SerializeField] private Sprite HappyCity;
    [SerializeField] private Sprite SadCity;

    [SerializeField] private Sprite BaseCityNpc;
    [SerializeField] private Sprite HappyCityNpc;

    [SerializeField] private RectTransform _heroAvatarRect;

    [SerializeField]
    private float _animationPercentForComing = 0.2f;

    [SerializeField]
    private float _animationPercentForLeaving = 0.2f;

    [SerializeField] private Image[] _fadeableImgs;
    [SerializeField] private TextMeshProUGUI[] _fadeableTexts;
    [SerializeField] private RectTransform _newDayWindow;

    private void Start()
    {
        _heroManager = HeroManager.Instance;
        _eventService = EventService.Instance;
        HandleGameTimeUpdated();
        // Events
        _eventService.NewHeroComing += HandleNewHeroComing;
        _eventService.GameTimeUpdated += HandleGameTimeUpdated;
        _eventService.HeroMoodChanged += HandleHeroMoodChanged;
        _eventService.HeroLeaving += HandleHeroLeaving;
        _eventService.HeroLeftFromScreen += HandleHeroLeftFromScreen;
        _eventService.CityStatusChange += HandleCityStatusChange;
        _eventService.DayEnd += HandleDayEnd;
    }

    private void HandleDayEnd()
    {
        _newDayWindow.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();

        foreach (var img in _fadeableImgs)
        {
            seq.Insert(0, img.DOFade(0f, 0f));
        }

        foreach (var txt in _fadeableTexts)
        {
            seq.Insert(0, txt.DOFade(0f, 0f));
        }

        foreach (var img in _fadeableImgs)
        {
            seq.Insert(0, img.DOFade(1f, 2f));
        }

        foreach (var txt in _fadeableTexts)
        {
            seq.Insert(0, txt.DOFade(1f, 2f));
        }

        seq.OnComplete(() =>
        {
            NewDayManager.Instance.ExecuteNewsCommands();
            NewDayWindowUIView.Instance.ShowNews();
        });
    }

    public void SetCityBackground(int status)
    {

    }

    public void UpdateBackground(Sprite background, Sprite backgroundNpc)
    {
        _cityBackgrouImg.sprite = background;
        if (backgroundNpc != null)
        {
            _cityBackgroundNpcImg.sprite = backgroundNpc;
            _cityBackgroundNpcImg.gameObject.SetActive(true);
        }
        else
        {
            _cityBackgroundNpcImg.gameObject.SetActive(false);
        }
        _cityBackgrouImg.gameObject.SetActive(true);
    }

    private void HandleCityStatusChange(int status)
    {
        var newDayManager = NewDayManager.Instance;

        if (status == 0)
        {
            newDayManager.NewDayCommands.Enqueue(new NewDayCommand
            {
                CmdType = NewDayCmdType.SwitchBackground,
                Background = BaseCity,
                BackgroundNpc = BaseCityNpc
            });
        }
        else if (status == 1)
        {
            newDayManager.NewDayCommands.Enqueue(new NewDayCommand
            {
                CmdType = NewDayCmdType.SwitchBackground,
                Background = HappyCity,
                BackgroundNpc = HappyCityNpc
            });
        }
        else if (status == -1)
        {
            newDayManager.NewDayCommands.Enqueue(new NewDayCommand
            {
                CmdType = NewDayCmdType.SwitchBackground,
                Background = SadCity,
                BackgroundNpc = null
            });
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

        Debug.Log("?????????");
        _heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x - _animationPercentForComing * Screen.width, 1f).From();
        _heroAvatarImg.DOFade(1, 1);
        _heroNicknameText.DOFade(1, 1);
        _heroAvatarImg.sprite = hero.CurrentAvatarParts[0].Value;
    }

    private void HandleHeroLeaving()
    {
        var diagManager = DialogManager.Instance;

        Sequence seq = DOTween.Sequence();

        diagManager.DisplayBlank();
        seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x + _animationPercentForLeaving * Screen.width, 1f));

        _heroAvatarImg.DOFade(0, 1);
        _heroNicknameText.DOFade(0, 1);
        seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x, 1f)).OnComplete(() =>
        {
            diagManager.DisplayHello();
        });
        _heroAvatarImg.DOFade(1, 1);
        _heroNicknameText.DOFade(1, 1);
    }

    private void HandleHeroLeftFromScreen()
    {
        Debug.Log("????????????");
        Sequence seq = DOTween.Sequence();
        _heroAvatarImg.DOFade(0, 1);
        _heroNicknameText.DOFade(0, 1);
        seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x + _animationPercentForLeaving * Screen.width, 1f)).OnComplete(() =>
        {
            GameManager.Instance.CurrentHeroNickname = string.Empty;
        });
        GameManager.Instance.CurrentHeroNickname = string.Empty;
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
        _eventService.DayEnd -= HandleDayEnd;
    }
}