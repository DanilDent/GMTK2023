using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class QuestPiece : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public QuestPiecesContainer Container;
    private int Index;

    private Vector3 _offset;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    public Vector3 lastPosition;
    public Transform parent;

    public void Initialize(QuestPiecesContainer parentContainer, int index)
    {
        Container = parentContainer;
        Index = index;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        lastPosition = transform.localPosition;
        parent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _offset = Input.mousePosition - _rectTransform.position;

        _canvasGroup.blocksRaycasts = false;

        //”¡–¿“‹ ≈¡¿Õ€…  Œ—“€ÀÀ‹
        CursorImageController.Instance.IsDraggingQuest = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = Input.mousePosition - _offset;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        transform.SetParent(parent);
        transform.localPosition = lastPosition;
        CursorImageController.Instance.IsDraggingQuest = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Destroy()
    {
        //Container.DeleteQuestFromList(this);
        Destroy(gameObject);
    }

    public void SetPieceIndex(int index)
    {
        Index = index;
    }
}