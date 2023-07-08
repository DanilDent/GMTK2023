using UnityEngine;

[CreateAssetMenu(fileName = "DialogConfig", menuName = "Configs/DialogConfigConfig", order = 1)]
public class DialogConfig : ScriptableObject
{
    [SerializeField] private float allDurationMessage = 1f;

    public float AllDurationMessage => allDurationMessage;
}
