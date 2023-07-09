using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPacker : MonoBehaviour
{
    [SerializeField] private QuestInformation _questInformationPrefab;
    [SerializeField] private QuestPiecesContainer questPiecesContainer;

    private void Start()
    {
        EventService.Instance.NewQuestBecomeAvailable += CreateNewQuestOnScreen;
    }

    private void CreateNewQuestOnScreen(Quest quest)
    {
        //Debug.Log(quest.Name);
        var questInformation = Instantiate(_questInformationPrefab, transform.position, Quaternion.identity);
        if (questInformation.TryGetComponent(out QuestPiece piece))
        {
            questPiecesContainer.AddQuestToPool(piece);
        }
        if(questInformation.TryGetComponent(out TooltipComponent tooltip))
        {
            tooltip.Text = quest.Description;
        }
        questInformation.Initialize(quest);
    }

    private void OnDisable()
    {
        EventService.Instance.NewQuestBecomeAvailable -= CreateNewQuestOnScreen;
    }
}