using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestPiece : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform _mainCanvas;
    private QuestPiecesContainer _container;
    private int _index;

    private Vector3 _offset;

    private bool isGiven = false;
    private RectTransform _rectTransform;

    public void Initialize(Transform mainCanvas, QuestPiecesContainer parentContainer, int index)
    {
        _mainCanvas = mainCanvas;
        _container = parentContainer;
        _index = index;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start");
        //transform.SetParent(_mainCanvas);
        _offset = Input.mousePosition - _rectTransform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("Aaaa");
        _rectTransform.position = Input.mousePosition - _offset;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Finish");
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