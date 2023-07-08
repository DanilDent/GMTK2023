using UnityEngine;

[CreateAssetMenu(fileName = "CityConfig", menuName = "Configs/CityConfig", order = 1)]
public class CityConfig : ScriptableObject
{
    [SerializeField] private int cityHealth;
    public int CityHealth => cityHealth;
}
