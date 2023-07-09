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
    public void Play(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
    }

    public void Stop()
    {
        _audio.Stop();
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
        if (status == 0)
        {
            Play(BASE);
        }
        else if (status == 1)
        {
            Play(HAPPY);
        }
        else if (status == -1)
        {
            Play(SAD);
        }
    }
}