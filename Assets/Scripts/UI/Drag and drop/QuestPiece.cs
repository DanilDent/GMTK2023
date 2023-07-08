using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestPiece : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private QuestPiecesContainer _container;
    private int _index;
    private Image _selfImage;

    private Vector3 _offset;

    private bool isGiven = false;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    public void Initialize(QuestPiecesContainer parentContainer, int index)
    {
        _container = parentContainer;
        _index = index;
        _selfImage = GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Start");
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _offset = Input.mousePosition - _rectTransform.position;
        //_selfImage.raycastTarget = false;
        _canvasGroup.blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Aaaa");
        _rectTransform.position = Input.mousePosition - _offset;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Finish");
        _canvasGroup.blocksRaycasts = true;
        if (!isGiven)
        {
            transform.SetParent(_container.transform);
            transform.SetSiblingIndex(_index);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Destroy()
    {
        _container.DeleteQuestFromList(this);
        Destroy(gameObject);
    }

    public void SetPieceIndex(int index)
    {
        _index = index;
    }
}