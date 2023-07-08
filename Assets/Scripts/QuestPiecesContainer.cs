using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestPiecesContainer : MonoBehaviour
{
	// Start is called before the first frame update
	private void Start()
	{
		int index = 0;
		//get all children, and initialize them

		foreach(var piece in GetChildren())
		{
			piece.Initialize(this, index);
			index++;
		}
	}
	private List<QuestPiece> GetChildren()
	{
		return (from Transform child in transform select child.GetComponent<QuestPiece>()).ToList();
	}

	// Update is called once per frame
	private void Update()
	{
	}
}