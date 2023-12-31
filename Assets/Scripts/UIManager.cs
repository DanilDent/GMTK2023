using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private RectTransform _kostilBackground;
    [SerializeField] private Button _startPlayBtn;
    [SerializeField] private RectTransform _greetingWindow;
    //
    [SerializeField] private Image _tutorTintImg;
    public bool IsTutorComplete = false;
    private Color _defaultTintColor;

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
    [SerializeField] private Button _okBtn;

    private void Start()
    {
        _defaultTintColor = _tutorTintImg.color;
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
        _eventService.DiagButtonClicked += HandleFirstTimeGetQuestButtonClick;
        //
        _startPlayBtn.onClick.AddListener(OnStartPlay);
    }

    private void OnStartPlay()
    {
        EventService.Instance.StartPlay?.Invoke();
    }

    public void HideTint()
    {
        _tutorTintImg.gameObject.SetActive(false);
    }

    private void HandleFirstTimeGetQuestButtonClick(ButtonType btnType)
    {
        if (!IsTutorComplete && btnType == ButtonType.GetQuest)
        {
            _tutorTintImg.gameObject.SetActive(true);
            _tutorTintImg.DOFade(0f, 0f);
            _tutorTintImg.DOFade(_defaultTintColor.a, 1f);
        }
    }

    private void HandleDayEnd()
    {
        _okBtn.gameObject.SetActive(false);
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
            _okBtn.gameObject.SetActive(true);
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

        _heroAvatarImg.gameObject.SetActive(true);
        // Put hero comming animation here

        Debug.Log("?????????");
        //_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x - _animationPercentForComing * Screen.width, 1f).From();
        _heroAvatarImg.DOFade(0f, 0f);
        _heroNicknameText.DOFade(0f, 0f);

        _heroAvatarImg.DOFade(1, 1);
        _heroNicknameText.DOFade(1, 1).OnComplete(() => _kostilBackground.gameObject.SetActive(false));
        _heroAvatarImg.sprite = hero.CurrentAvatarParts[0].Value;
    }

    private void HandleHeroLeaving()
    {
        var diagManager = DialogManager.Instance;

        diagManager.DisplayBlank();
        //seq.Append(_heroAvatarRect.DOMoveX(_defaultHeroAvatarPosition.x + _animationPercentForLeaving * Screen.width, 1f));

        Sequence seq = DOTween.Sequence();
        seq.Insert(0, _heroAvatarImg.DOFade(0, 1));
        seq.Insert(0, _heroNicknameText.DOFade(0, 1));
        seq.Append(_heroAvatarImg.DOFade(1, 1));
        seq.Insert(1f, _heroNicknameText.DOFade(1, 1));
        seq.OnComplete(() => diagManager.DisplayHello());
    }

    private void HandleHeroLeftFromScreen()
    {
        Sequence seq = DOTween.Sequence();
        seq.Insert(0, _heroAvatarImg.DOFade(0, 1));
        seq.Insert(0, _heroNicknameText.DOFade(0, 1));
        seq.OnComplete(() =>
        {
            GameManager.Instance.CurrentHeroNickname = string.Empty;
            _kostilBackground.gameObject.SetActive(true);
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
        _eventService.DayEnd -= HandleDayEnd;
        _eventService.DiagButtonClicked += HandleFirstTimeGetQuestButtonClick;
        //
        _startPlayBtn.onClick.RemoveAllListeners();
    }

    public void ShowGreeting()
    {
        _greetingWindow.gameObject.SetActive(true);
    }

    public void HideGreeting()
    {
        _greetingWindow.gameObject.SetActive(false);
    }
}