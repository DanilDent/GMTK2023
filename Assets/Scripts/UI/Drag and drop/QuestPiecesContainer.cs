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
    }

    private List<QuestPiece> GetChildren()
    {
        return (from Transform child in transform select child.GetComponent<QuestPiece>()).ToList();
    }

    public void AddQuestToPool(QuestPiece quest)
    {
        int index = 0;
        foreach (var piece in _questPieces)
        {
            index++;
        }
        _questPieces.Add(quest);
        quest.transform.parent = transform;
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