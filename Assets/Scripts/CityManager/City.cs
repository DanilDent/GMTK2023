using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [SerializeField] private CityConfig config;
    [SerializeField] private int currentHealth;
    private int maxHealth;

    public int CurrentHealth => currentHealth;
    private void Start()
    {
        maxHealth = config.CityHealth;
        currentHealth = maxHealth;
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
        }
        else
        {
            currentHealth = res;
            EventService.Instance.CityHealthChanged?.Invoke((float)currentHealth / maxHealth);
        }
    }
}