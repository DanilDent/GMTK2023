using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        AwaitingQuests,
        NewHero,
        BehaviourAnalysis,
        QuestGiving,
        NewsReading,
        GameOver
    }

    // Public interface
    public bool LockDiagArea = false;

    public bool IsPaused
    {
        get => _isPaused;
        set { _isPaused = value; }
    }

    public GameTime CurrentTime => _currentGameTime;
    public GameState CurrentState => _currentState;

    public string CurrentHeroNickname
    {
        get => _currentHeroNickname;
        set => _currentHeroNickname = value;
    }

    // Singleton impl
    public static GameManager Instance => _instance;

    private static GameManager _instance;

    // From inspector
    [SerializeField] private GameTimelineConfig _timelineConfig;

    // Other dependencies
    private EventService _eventService;

    private GameTime _currentGameTime;
    private GameTime _lastGameTimeTick;
    private string _currentHeroNickname;
    private float _timer;
    private bool _isPaused;
    private int _eventIndex;
    private GameState _currentState;
    [SerializeField] private RectTransform _diagRect;
    private int _dayIndex;

    public void SetCurrentTime(GameTime time) => _currentGameTime = time;

    public void SetGameState(GameState state)
    {
        _currentState = state;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _timer = (float)_timelineConfig.SecRealTimeToMinsGameTime / _timelineConfig.GameTimeStepChange;
        _eventService = EventService.Instance;

        // Events
        _eventService.NextDay += HandleNextDay;

        _currentGameTime = _timelineConfig.Days.FirstOrDefault().StartOfDay;
        SetGameState(GameState.AwaitingQuests);

        EventService.Instance.QuestAssigned += OnQuestAssined;
        SoundService.Instance.SetClip(SoundService.Instance.BASE);
        SoundService.Instance.Play(2f);

    }

    private void OnDestroy()
    {
        _instance = null;
        EventService.Instance.QuestAssigned -= OnQuestAssined;
        _eventService.NextDay -= HandleNextDay;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        Tick();
    }

    private void HideCursor()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(_diagRect, Input.mousePosition))
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    private void Tick()
    {
        if (!_isPaused)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0f)
            {
                _timer = (float)_timelineConfig.SecRealTimeToMinsGameTime / _timelineConfig.GameTimeStepChange;
                _lastGameTimeTick = _currentGameTime;
                _currentGameTime += new GameTime(0, 0, minutes: 1);
                HandleEvents();

                if (_dayIndex < _timelineConfig.Days.Length &&
                    _currentGameTime >= _timelineConfig.Days[_dayIndex].EndOfDay)
                {
                    IsPaused = true;
                    EventService.Instance.DayEnd?.Invoke();
                    SoundService.Instance.Stop(duration: 2f);
                    if (HeroBehPatternExecutor.IsEnabled)
                    {
                        HeroBehPatternExecutor.Instance.Pause();
                    }


                }

                GameTime deltaTime = _currentGameTime - _lastGameTimeTick;
                _eventService.GameTimeUpdated?.Invoke();
                //Debug.Log($"CurrentGameTime: {_currentGameTime}");
            }
        }
    }

    private void HandleNextDay()
    {
        _eventIndex = 0;
        if (_dayIndex + 1 >= _timelineConfig.Days.Length)
        {
            if (City.Instance.CurrentHealth <= 0)
            {
                EventService.Instance.Defeat?.Invoke();
                SoundService.Instance.SetClip(SoundService.Instance.LOSE);
                SoundService.Instance.Play();
            }
            else
            {
                EventService.Instance.Victory?.Invoke();
                SoundService.Instance.SetClip(SoundService.Instance.HAPPY);
                SoundService.Instance.Play();
            }
            SetGameState(GameState.GameOver);
            return;
        }
        else
        {
            if (City.Instance.CurrentHealth <= 0)
            {
                EventService.Instance.Defeat?.Invoke();
                SoundService.Instance.SetClip(SoundService.Instance.LOSE);
                SoundService.Instance.Play();
                return;
            }
            ++_dayIndex;
            _currentGameTime = _timelineConfig.Days[_dayIndex].StartOfDay;
            IsPaused = false;
            if (HeroBehPatternExecutor.IsEnabled)
            {
                HeroBehPatternExecutor.Instance.Resume();
            }
            NewDayManager.Instance.ExecuteNewDayCommands();
        }
    }

    private void OnQuestAssined(Quest _)
    {
        IsPaused = false;
    }

    private void HandleEvents()
    {
        while (_currentGameTime.Day < _timelineConfig.Days.Length &&
               _eventIndex < _timelineConfig.Days[_dayIndex].Timeline.Length &&
               _currentGameTime >= _timelineConfig.Days[_dayIndex].Timeline[_eventIndex].GameTime)
        {
            TimelineEventData eventData = _timelineConfig.Days[_dayIndex].Timeline[_eventIndex];
            if (eventData.EventType == TimelineEventType.NewHero)
            {
                IsPaused = true;
                _eventService.NewHeroComing?.Invoke(eventData.Name);
                //Debug.Log($"New hero came to our village: {eventData.Name}");
                SetGameState(GameState.NewHero);
                DialogManager.Instance.DisplayHello();
                Hero hero = HeroManager.Instance.Heroes.FirstOrDefault(_ => _.Nickname == eventData.Name);
                if (HeroBehPatternExecutor.IsEnabled)
                {
                    if (hero.HeroBehPatternName != string.Empty)
                    {
                        HeroBehPatternExecutor.Instance.SetRecording(hero.HeroBehPatternName);
                        HeroBehPatternExecutor.Instance.StartPlay();
                    }
                    else
                    {
                        Debug.Log($"Invalid hero pattern name {hero.HeroBehPatternName} for hero {hero.Nickname}");
                    }
                }
            }
            _eventIndex++;
        }
    }
}