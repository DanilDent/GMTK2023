using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPacker : MonoBehaviour
{
    [SerializeField] private QuestInformation _questInformation;
    [SerializeField] private QuestPiecesContainer questPiecesContainer;

    private void Start()
    {
        EventService.Instance.NewQuestBecomeAvailable += CreateNewQuestOnScreen;
    }

    private void CreateNewQuestOnScreen(Quest quest)
    {
        Debug.Log(quest.Name);
        var questInformation = Instantiate(_questInformation, transform.position, Quaternion.identity);
        if (questInformation.TryGetComponent(out QuestPiece piece))
        {
            questPiecesContainer.AddQuestToPool(piece);
        }
        questInformation.Initialize(quest);
    }

    private void OnDisable()
    {
        EventService.Instance.NewQuestBecomeAvailable -= CreateNewQuestOnScreen;
    }
}