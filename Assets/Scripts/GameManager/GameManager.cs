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
    public bool IsPaused { get => _isPaused; set { _isPaused = value; } }
    public GameTime CurrentTime => _currentGameTime;
    public GameState CurrentState => _currentState;
    public string CurrentHeroNickname { get => _currentHeroNickname; set => _currentHeroNickname = value; }

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

    public void SetGameState(GameState state)
    {
        _currentState = state;
    }

    private void Awake()
    {
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

        _currentGameTime = _timelineConfig.Days.FirstOrDefault().StartOfDay;
        SetGameState(GameState.AwaitingQuests);
    }

    private void Update()
    {
        Tick();
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

                if (_currentGameTime.Day >= _timelineConfig.Days.Length ||
                    _currentGameTime >= _timelineConfig.Days[_currentGameTime.Day].EndOfDay)
                {
                    _eventIndex = 0;
                    if (_currentGameTime.Day >= _timelineConfig.Days.Length)
                    {
                        /// GAME OVER
                        SetGameState(GameState.GameOver);
                    }
                    else
                    {
                        _currentGameTime = _timelineConfig.Days[_currentGameTime.Day + 1].StartOfDay;
                    }
                }

                GameTime deltaTime = _currentGameTime - _lastGameTimeTick;
                _eventService.GameTimeUpdated?.Invoke();
                //Debug.Log($"CurrentGameTime: {_currentGameTime}");
            }
        }
    }

    private void HandleEvents()
    {
        while (_currentGameTime.Day < _timelineConfig.Days.Length &&
            _eventIndex < _timelineConfig.Days[_currentGameTime.Day].Timeline.Length &&
            _currentGameTime >= _timelineConfig.Days[_currentGameTime.Day].Timeline[_eventIndex].GameTime)
        {
            TimelineEventData eventData = _timelineConfig.Days[_currentGameTime.Day].Timeline[_eventIndex];
            if (eventData.EventType == TimelineEventType.NewHero)
            {
                _eventService.NewHeroComing?.Invoke(eventData.Name);
                //Debug.Log($"New hero came to our village: {eventData.Name}");
                SetGameState(GameState.NewHero);
            }
            _eventIndex++;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
