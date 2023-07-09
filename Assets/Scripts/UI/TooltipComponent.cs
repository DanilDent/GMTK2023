using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 Offset;
    [HideInInspector] public string Text;
    private bool toolTipUsed;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.OpenTooltip(this, Text);
        toolTipUsed = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (toolTipUsed)
        {
            TooltipManager.Instance.CloseTooltip();
            toolTipUsed = false;
        }
    }
    void OnDisable()
    {
        if (toolTipUsed)
        {
            TooltipManager.Instance.CloseTooltip();
            toolTipUsed = false;
        }
    }
}
