using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questName;
    private Quest _quest;

    public Quest Quest { get => _quest; }

    public void Initialize(Quest quest)
    {
        _quest = quest;
        UpdateUIElement();
    }

    private void UpdateUIElement()
    {
        _questName.text = _quest.Name;
    }
}