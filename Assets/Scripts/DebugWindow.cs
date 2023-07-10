using TMPro;
using UnityEngine;

public class DebugWindow : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = $"Debug:\nTime: Day {GameManager.Instance.CurrentTime.Day + 1}, {GameManager.Instance.CurrentTime.ToNiceString()}\nCity HP: {City.Instance.CurrentHealth}";
    }
}