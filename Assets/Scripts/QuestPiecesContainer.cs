using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPiecesContainer : MonoBehaviour
{
    [SerializeField] private List<QuestPiece> _questPieces;

    // Start is called before the first frame update
    private void Start()
    {
        int index = 0;
        foreach (var piece in _questPieces)
        {
            piece.Initialize(this, index);
            index++;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}