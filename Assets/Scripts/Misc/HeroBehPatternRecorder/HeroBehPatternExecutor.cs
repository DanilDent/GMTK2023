using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class HeroBehPatternExecutor : MonoSingleton<HeroBehPatternExecutor>
{
    public static bool IsEnabled => _instance != null ? _instance.gameObject.activeInHierarchy : false;
    private string RECORDINGS_PATH => Application.dataPath + "\\Content\\Config\\_HeroBehPatterns\\_jsons\\Resources";
    private string RECORDINGS_PATH_PERS => Application.persistentDataPath + "\\APP_DATA";
    [SerializeField] private PatternsConfig _patterns;
    private const int _recordFrameRate = 60;
    private float _timerMax;
    private float _timer;
    private bool _isPaused;

    private void Start()
    {
        _timerMax = 1f / _recordFrameRate;
    }

    public void SetRecording(string patternName)
    {
        string path = Path.Combine(RECORDINGS_PATH, $"{patternName}.json");
        string json = Resources.Load<TextAsset>($"{patternName}")?.text ?? string.Empty;
        if (json == string.Empty)
        {
            Debug.LogWarning($"Can't find beh pattern with name {patternName}");
            return;
        }
        _recording = JsonConvert.DeserializeObject<Recording>(json);
        Debug.Log($"Recording: {_recording}");
        _currentIndex = 0;
    }

    public void StartPlay()
    {
        _heroCursorRect.gameObject.SetActive(true);
        _isPlaying = true;
        _currentIndex = 0;
        _timer = _timerMax;
    }

    public void HideBotCursor()
    {
        _heroCursorRect.gameObject.SetActive(false);
    }

    public void ShowBotCursor()
    {
        _heroCursorRect.gameObject.SetActive(true);
    }

    public void Pause()
    {
        _isPaused = true;
    }
    public void Resume()
    {
        _isPaused = false;
    }
    public bool IsPaused()
    {
        return _isPaused;
    }

    [SerializeField] private RectTransform _heroCursorRect;
    private bool _isPlaying;
    private Recording _recording;
    private int _currentIndex;

    private void Update()
    {
        if (_recording == null)
        {
            return;
        }

        if (_isPlaying && !_isPaused)
        {
            _timer -= Time.deltaTime;

            if (_timer < 0f)
            {
                _timer = _timerMax;

                if (_currentIndex >= _recording.Data.Count)
                {
                    // Fire cutscene completed event
                    _isPlaying = false;
                    _recording = null;
                    _heroCursorRect.gameObject.SetActive(false);
                    return;
                }

                Command command = _recording.Data[_currentIndex];

                UnityEngine.Vector3 relativeVec3 = SystemVector3ToUnityVector3(command.Vec3);
                UnityEngine.Vector3 absoluteVec3ScreenSpace = RelativePosToAbsolutePos(relativeVec3);
                UnityEngine.Vector3 adjustedAbsVec3 = new UnityEngine.Vector3(absoluteVec3ScreenSpace.x, absoluteVec3ScreenSpace.y, absoluteVec3ScreenSpace.z);
                if (command.CmdType == CommandType.SetCursorPosition)
                {
                    _heroCursorRect.position = adjustedAbsVec3;
                }
                else if (command.CmdType == CommandType.MoveCursorTo)
                {
                    _heroCursorRect.position += absoluteVec3ScreenSpace;
                }
                else if (command.CmdType == CommandType.ClickButtonCommand)
                {
                    DialogManager diagManager = DialogManager.Instance;
                    EventService.Instance.DiagButtonClickedByBot?.Invoke(command.BtnType);
                }

                _currentIndex++;
            }
        }
    }

    private static UnityEngine.Vector3 SystemVector3ToUnityVector3(System.Numerics.Vector3 vector)
    {
        return new UnityEngine.Vector3(vector.X, vector.Y, vector.Z);
    }

    private static UnityEngine.Vector3 RelativePosToAbsolutePos(UnityEngine.Vector3 vec)
    {
        return new UnityEngine.Vector3(vec.x * Screen.width, vec.y * Screen.height, vec.z);
    }
}
