using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * This class it used for debugging only for exiting the puzzle 
 * This could be kept if the player want to just exit the puzzle 
*/
public class ExitButton : MonoBehaviour {

	public Button exitButton;
	private Puzzle puzzle;

	void Start(){

		Button btn = exitButton.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);
		puzzle = GetComponentInParent<Puzzle> ();

	}
		
	void TaskOnClick(){
		puzzle.PuzzleExit ();
	}
}
