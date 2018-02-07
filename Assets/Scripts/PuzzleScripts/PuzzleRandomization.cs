using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PuzzleRandomization : MonoBehaviour {

	//These will a list of the puzzle scene names
	public string[] listOfPuzzles; // Testing Note: populate this array in the inspecter, and just add one if u wanna test your own
	public PuzzlePlaceholder[] placeholders; // Note: make sure you add a placeholders in the inspecter as well


	void Start () {
		DebugPlacePuzzles ();
	}

	//TODO: Complete PuzzleRandomization

	//Debugging Use Only
	//Will place puzzles in order
	void DebugPlacePuzzles(){
		int puzzleCounter = 0; 
		for (int counter = 0; counter < placeholders.Length; counter++) {
			placeholders [counter].puzzleGoTo = listOfPuzzles [puzzleCounter];
			puzzleCounter++;
			//loop back from the start of the list
			if (puzzleCounter == listOfPuzzles.Length) {
				puzzleCounter = 0;
			}

		}


	}
}
