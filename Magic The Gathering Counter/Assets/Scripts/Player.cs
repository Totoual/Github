using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player {
	[SerializeField] public int id;
	[SerializeField] public string name;
	[SerializeField] public int health = 20;
	[SerializeField] public int maxPoisonCounter = 10;
	[SerializeField] public int currentPoisonCounter = 0;
	[SerializeField] public bool dead;

	[SerializeField] public List<deckType> activeDeck = new List<deckType>();

}

public enum deckType{
	white,
	blue,
	red,
	black,
	green,
	colorless
}


