using UnityEngine;

public class City : MonoSingleton<City>
{
    [SerializeField] private CityConfig config;
    [SerializeField] private int currentHealth;
    private int startHealth;
    private int lowerThresholdWellBeingCity;
    private int upperThresholdDeclineCity;
    private int cityStatus = -2;

    public int CurrentHealth => currentHealth;
    private void Start()
    {
        startHealth = config.CityHealth;
        lowerThresholdWellBeingCity = config.LowerThresholdWellBeingCity;
        upperThresholdDeclineCity = config.UpperThresholdDeclineCity;
        currentHealth = startHealth;
        EventService.Instance.QuestCompleted += OnQuestComleted;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventService.Instance.QuestCompleted -= OnQuestComleted;
    }

    public void OnQuestComleted(Quest data, bool result)
    {
        ApplyChange(result ? data.SuccessfulHPChange : data.FailureHPChange);
    }

    public void ApplyChange(int valueChange)
    {
        var res = currentHealth + valueChange;
        currentHealth = res;
        if (currentHealth >= upperThresholdDeclineCity)
        {
            if (cityStatus != 1)
            {
                cityStatus = 1;
                EventService.Instance.CityStatusChange?.Invoke(cityStatus);
            }
        }
        else if (currentHealth <= lowerThresholdWellBeingCity)
        {
            if (cityStatus != -1)
            {
                cityStatus = -1;
                EventService.Instance.CityStatusChange?.Invoke(cityStatus);
            }
        }
        else
        {
            if (cityStatus != 0)
            {
                cityStatus = 0;
                EventService.Instance.CityStatusChange?.Invoke(cityStatus);
            }
        }
    }
}