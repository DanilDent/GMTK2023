using UnityEngine;
using UnityEngine.EventSystems;

public class QuestSlot : MonoBehaviour, IDropHandler
{
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData);
        if (eventData.pointerDrag.TryGetComponent(out QuestPiece questPiece))
        {
            questPiece.TryGetComponent(out QuestInformation questInformation);
            EventService.Instance.QuestAssigned.Invoke(questInformation.Quest);
            PlayerCursorBehaviour.Instance.LockDiagArea = true;
            if (!UIManager.Instance.IsTutorComplete)
            {
                UIManager.Instance.IsTutorComplete = true;
                UIManager.Instance.HideTint();
            }
            questPiece.GetComponent<RectTransform>().SetParent(questPiece.parent);
            questPiece.GetComponent<RectTransform>().localPosition = questPiece.lastPosition;
            questPiece.gameObject.SetActive(false);
            questPiece.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //”¡–¿“‹ ≈¡¿Õ€…  Œ—“€ÀÀ‹
            CursorImageController.Instance.IsDraggingQuest = false;
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