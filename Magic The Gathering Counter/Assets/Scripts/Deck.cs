using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Deck 	{

	[SerializeField] public List<deckType> deck = new List<deckType>();
	[SerializeField] public int counter;
}

