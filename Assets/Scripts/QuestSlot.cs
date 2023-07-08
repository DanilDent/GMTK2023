using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestSlot : MonoBehaviour, IDropHandler
{
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData);
        if (eventData.pointerDrag.TryGetComponent(out QuestPiece questPiece))
        {
            GameObject quest = questPiece.gameObject;
            Destroy(quest);
        }
        //Ивент с передачей квеста герою
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}