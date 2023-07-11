using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorImageController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static CursorImageController Instance;
    [SerializeField] private Texture2D _normalCursorImage;
    [SerializeField] private Texture2D _crossCursorImage;

    public bool IsDraggingQuest;
    private bool _canChangeCursor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsDraggingQuest)
        {
            if (/*RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition)*/_canChangeCursor)
            {
                Cursor.SetCursor(_crossCursorImage, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(_normalCursorImage, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(_normalCursorImage, Vector2.zero, CursorMode.Auto);
        }

        //}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Can change cursor image");
        _canChangeCursor = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _canChangeCursor = false;
        Debug.Log("CANT ");
    }
}