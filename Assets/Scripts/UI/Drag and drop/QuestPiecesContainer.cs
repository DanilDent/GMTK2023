using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestPiecesContainer : MonoBehaviour
{
    private List<QuestPiece> _questPieces;

    // Start is called before the first frame update
    private void Start()
    {
        int index = 0;
        //get all children, and initialize them

        foreach (var piece in GetChildren())
        {
            piece.Initialize(this, index);
            index++;
        }

        _questPieces = GetChildren();
        EventService.Instance.QuestLifetimeEnded += DeleteQuestInformation;
    }

    private List<QuestPiece> GetChildren()
    {
        return (from Transform child in transform select child.GetComponent<QuestPiece>()).ToList();
    }

    public void DeleteQuestInformation(Quest quest)
    {
        List<QuestInformation> questInformation = new List<QuestInformation>();
        foreach (var piece in _questPieces)
        {
            if (piece.TryGetComponent(out QuestInformation information))
            {
                questInformation.Add(information);
            }
        }

        QuestInformation deletedQuestInformation = questInformation.FirstOrDefault(x => x.Quest.Name == quest.Name);
        if (deletedQuestInformation.TryGetComponent(out QuestPiece deletedQuestPiece))
        {
            deletedQuestPiece.Destroy();
        }
    }

    public void AddQuestToPool(QuestPiece quest)
    {
        int index = 0;
        foreach (var piece in _questPieces)
        {
            index++;
        }
        _questPieces.Add(quest);
        quest.transform.SetParent(transform, false);
        quest.SetPieceIndex(index);
        quest.Initialize(this, index);
    }

    public void DeleteQuestFromList(QuestPiece quest)
    {
        _questPieces.Remove(quest);
        int index = 0;
        foreach (var piece in _questPieces)
        {
            piece.SetPieceIndex(index);
            index++;
        }
    }
}