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
            questPiece.Destroy();
            questPiece.TryGetComponent(out QuestInformation questInformation);
            EventService.Instance.QuestAssigned(questInformation.Quest);
        }
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