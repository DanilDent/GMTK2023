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
        if (!isStart)
        {
            return;
        }
        isStart = false;
        EventService.Instance.QuestCompleted?.Invoke(QuestManager.Instance.invisibleQuests[0], false);
        Debug.Log(city.CurrentHealth);
        EventService.Instance.QuestCompleted?.Invoke(QuestManager.Instance.invisibleQuests[0], true);
        Debug.Log(city.CurrentHealth);
        city.ApplyChange(20);
        Debug.Log(city.CurrentHealth);
        city.ApplyChange(-5);
        Debug.Log(city.CurrentHealth);
        city.ApplyChange(3);
        Debug.Log(city.CurrentHealth);
        city.ApplyChange(-50);
        Debug.Log(city.CurrentHealth);
    }
}
