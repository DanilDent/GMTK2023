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

    public void Initialize(QuestPiecesContainer parentContainer, int index)
    {
        _container = parentContainer;
        _index = index;
        _selfImage = GetComponent<Image>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start");
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _offset = Input.mousePosition - _rectTransform.position;
        _selfImage.raycastTarget = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("Aaaa");
        _rectTransform.position = Input.mousePosition - _offset;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Finish");
        _selfImage.raycastTarget = true;
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

    // Update is called once per frame
    private void Update()
    {
    }
}