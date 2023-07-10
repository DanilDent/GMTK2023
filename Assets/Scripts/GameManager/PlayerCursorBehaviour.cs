using System;
using System.Collections;
using UnityEngine;

public class PlayerCursorBehaviour : MonoSingleton<PlayerCursorBehaviour>
{
    public bool LockDiagArea;
    [SerializeField] private RectTransform _diagRect;
    Vector3 _prevMousePos;
    [SerializeField] private float _duration;

    private void Start()
    {
        _prevMousePos = Input.mousePosition;
    }

    private void Update()
    {
        _prevMousePos = Input.mousePosition;

        if (LockDiagArea && GameManager.Instance.LockDiagArea)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_diagRect, Input.mousePosition))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }

    private IEnumerator InvokeWithDelay(float delay, Action action)
    {
        yield return null;
        action();
    }
}