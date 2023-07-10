using UnityEngine;

public class PlayerCursorBehaviour : MonoSingleton<PlayerCursorBehaviour>
{
    public bool LockDiagArea;
    [SerializeField] private RectTransform _diagRect;
    [SerializeField] private float _duration;

    private void Update()
    {
        //if (LockDiagArea && GameManager.Instance.LockDiagArea)
        //{
        //    if (RectTransformUtility.RectangleContainsScreenPoint(_diagRect, Input.mousePosition))
        //    {
        //        Cursor.lockState = CursorLockMode.Locked;
        //    }
        //    else
        //    {
        //        Cursor.lockState = CursorLockMode.None;
        //    }
        //}

    }
}