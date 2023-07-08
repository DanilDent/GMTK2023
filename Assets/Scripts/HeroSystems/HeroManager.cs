using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
	[SerializeField] private List<Hero> _allHeroes;
	private void Awake()
	{
		var instantiated = _allHeroes.Select(Instantiate).ToList();
		_allHeroes = instantiated;
	}
	
}