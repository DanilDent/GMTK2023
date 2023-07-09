using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] private RectTransform TooltipPanel;
    private TextMeshProUGUI textLabel;

    public static TooltipManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        textLabel = TooltipPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OpenTooltip(TooltipComponent _component, string _text)
    {
        var pos = _component.GetComponent<RectTransform>().position + _component.Offset;
        TooltipPanel.position = pos;
        TooltipPanel.gameObject.SetActive(true);
        textLabel.text= _text;
    }
    public void CloseTooltip()
    {
        TooltipPanel.position = Vector3.zero;
        TooltipPanel.gameObject.SetActive(false);
    }
}
