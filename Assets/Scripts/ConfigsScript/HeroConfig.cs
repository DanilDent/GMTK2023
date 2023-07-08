using UnityEngine;

[CreateAssetMenu(fileName = "Heroes", menuName = "Configs/HeroConfig", order = 1)]
public class HeroConfig : ScriptableObject
{
	[SerializeField] private Hero[] data;
	public Hero[] GetData => data;
}