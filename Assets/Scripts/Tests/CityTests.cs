using Unity.VisualScripting;
using UnityEngine;

public class CityTests : MonoBehaviour
{
    [SerializeField] private City city;
    [SerializeField] private bool isStart;
    private void Start()
    {
        city = FindAnyObjectByType<City>();
        if (city == null)
        {
            return;
        }
        EventService.Instance.CityStatusChange += OnCityStatusChange;
    }
    private void OnDestroy()
    {
        EventService.Instance.CityStatusChange -= OnCityStatusChange;
    }
    private void Update()
    {

        if (!isStart)
        {
            return;
        }
        isStart = false;
        EventService.Instance.QuestCompleted?.Invoke(QuestManager.Instance.invisibleQuests[0], false);
        Debug.Log(city.CurrentHealth);
        EventService.Instance.QuestCompleted?.Invoke(QuestManager.Instance.invisibleQuests[0], true);
        Debug.Log(city.CurrentHealth);
        EventService.Instance.QuestCompleted?.Invoke(QuestManager.Instance.invisibleQuests[0], false);
        EventService.Instance.QuestCompleted?.Invoke(QuestManager.Instance.invisibleQuests[0], false);
        EventService.Instance.QuestCompleted?.Invoke(QuestManager.Instance.invisibleQuests[0], false);
        EventService.Instance.QuestCompleted?.Invoke(QuestManager.Instance.invisibleQuests[0], false);
        EventService.Instance.QuestCompleted?.Invoke(QuestManager.Instance.invisibleQuests[0], false);
        Debug.Log(city.CurrentHealth);
        city.ApplyChange(50);
        Debug.Log(city.CurrentHealth);
        city.ApplyChange(25);
        Debug.Log(city.CurrentHealth);
        city.ApplyChange(-30);
        Debug.Log(city.CurrentHealth);
        city.ApplyChange(-30);
        Debug.Log(city.CurrentHealth);
    }

    private void OnCityStatusChange(int newStatus)
    {
        Debug.Log($"New city status - {newStatus}");
    }
}
