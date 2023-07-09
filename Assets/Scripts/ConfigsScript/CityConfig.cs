using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "CityConfig", menuName = "Configs/CityConfig", order = 1)]
public class CityConfig : ScriptableObject
{
    [SerializeField] private int cityHealth;
    [SerializeField] private int lowerThresholdWellBeingCity;
    [SerializeField] private int upperThresholdDeclineCity;
    public int CityHealth => cityHealth;
    public int LowerThresholdWellBeingCity => lowerThresholdWellBeingCity;
    public int UpperThresholdDeclineCity => upperThresholdDeclineCity;
}
