using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public interface
    public GameTime CurrentTime => _currentGameTime;

    // Singleton impl
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    // From inspector
    [SerializeField] private GameTimelineConfig _timelineConfig;
    // Other dependencies
    private EventService _eventService;

    private GameTime _currentGameTime;
    private GameTime _lastGameTimeTick;
    private float _timer;
    private bool _isPaused;

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
        _timer = _timelineConfig.SecRealTimeToMinsGameTime;
        _eventService = EventService.Instance;

        _currentGameTime = _timelineConfig.Timeline.FirstOrDefault(_ => _.EventType == TimelineEventType.StartOfDay).GameTime;
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
                _timer = _timelineConfig.SecRealTimeToMinsGameTime;
                _lastGameTimeTick = _currentGameTime;
                _currentGameTime += new GameTime { Minutes = _timelineConfig.GameTimeStepChange };
                GameTime deltaTime = _currentGameTime - _lastGameTimeTick;
                _eventService.GameTimeUpdated?.Invoke(deltaTime);
                Debug.Log($"CurrentGameTime: {_currentGameTime}");
            }
        }
    }

    private void SwitchTimePause()
    {
        _isPaused = !_isPaused;
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
