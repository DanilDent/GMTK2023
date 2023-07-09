using UnityEngine;

public class City : MonoBehaviour
{
    [SerializeField] private CityConfig config;
    [SerializeField] private int currentHealth;
    private int maxHealth;
    private int lowerThresholdWellBeingCity;
    private int upperThresholdDeclineCity;
    private int cityStatus = -2;

    public int CurrentHealth => currentHealth;
    private void Start()
    {
        maxHealth = config.CityHealth;
        lowerThresholdWellBeingCity = config.LowerThresholdWellBeingCity;
        upperThresholdDeclineCity = config.UpperThresholdDeclineCity;
        currentHealth = maxHealth / 2;
        EventService.Instance.QuestCompleted += OnQuestComleted;
    }
    private void OnDestroy()
    {
        EventService.Instance.QuestCompleted -= OnQuestComleted;
    }

    public void OnQuestComleted(Quest data, bool result)
    {
        ApplyChange(result ? data.SuccessfulHPChange : data.FailureHPChange);
    }

    public void ApplyChange(int valueChange)
    {
        var res = currentHealth + valueChange;
        if (res >= maxHealth)
        {
            currentHealth = maxHealth;
            EventService.Instance.CityHealthChanged?.Invoke(1f);
        }
        else if (res <= 0)
        {
            currentHealth = 0;
            EventService.Instance.CityHealthChanged?.Invoke(0f);
            EventService.Instance.CityDestroyed?.Invoke();
            EventService.Instance.Defeat?.Invoke();
        }
        else
        {
            currentHealth = res;
            EventService.Instance.CityHealthChanged?.Invoke((float)currentHealth / maxHealth);
        }

        if (currentHealth > upperThresholdDeclineCity)
        {
            if (cityStatus != 1)
            {
                cityStatus = 1;
                EventService.Instance.CityStatusChange?.Invoke(cityStatus);
            }
        }
        else if (currentHealth < lowerThresholdWellBeingCity)
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