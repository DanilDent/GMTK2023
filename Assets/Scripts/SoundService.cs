using DG.Tweening;
using UnityEngine;

public class SoundService : MonoSingleton<SoundService>
{
    [SerializeField] private AudioSource _audio;

    //public const string BASE_SONG = "SFX\\QuestGiver-base.mp3";
    //public const string HAPPY_SONG = "SFX\\QuestGiver-base.mp3";
    //public const string LOSE_SONG = "SFX\\QuestGiver-base.mp3";
    //public const string SAD_SONG = "SFX\\QuestGiver-base.mp3";

    public AudioClip BASE;
    public AudioClip HAPPY;
    public AudioClip LOSE;
    public AudioClip SAD;

    public void Play(float duration = 0f)
    {
        _audio.volume = 0;
        _audio.Play();
        DOTween.To(() => _audio.volume, x => _audio.volume = x, 0.1f, duration);
    }

    public void SetClip(AudioClip clip)
    {
        _audio.clip = clip;
    }

    public void Stop(float duration = 0f)
    {
        DOTween.To(() => _audio.volume, x => _audio.volume = x, 0f, duration)
            .OnComplete(() => _audio.Stop());
    }

    private void Start()
    {
        EventService.Instance.CityStatusChange += HandleCityStatusChange;
    }

    protected override void OnDestroy()
    {
        EventService.Instance.CityStatusChange -= HandleCityStatusChange;
    }

    private void HandleCityStatusChange(int status)
    {
        var newDayManager = NewDayManager.Instance;
        if (status == 0)
        {
            newDayManager.NewDayCommands.Enqueue(new NewDayCommand
            {
                CmdType = NewDayCmdType.SwitchMusic,
                Audio = BASE
            });
        }
        else if (status == 1)
        {
            newDayManager.NewDayCommands.Enqueue(new NewDayCommand
            {
                CmdType = NewDayCmdType.SwitchMusic,
                Audio = HAPPY
            });
        }
        else if (status == -1)
        {
            newDayManager.NewDayCommands.Enqueue(new NewDayCommand
            {
                CmdType = NewDayCmdType.SwitchMusic,
                Audio = SAD
            });
        }
    }
}