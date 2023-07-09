using UnityEngine;

[CreateAssetMenu(fileName = "patterns", menuName = "Configs/Pattern config", order = 1)]
public class PatternsConfig : ScriptableObject
{
    [SerializeField] public PatternStruct[] patterns;
}

