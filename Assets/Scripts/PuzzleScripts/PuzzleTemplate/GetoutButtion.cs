using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This class it used for debugging only for exiting the puzzle
public class GetoutButtion : MonoBehaviour {

	public Button outButton;
	private Puzzle puzzle;

	void Start(){

		Button btn = outButton.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);
		puzzle = GetComponentInParent<Puzzle> ();

	}
		
	void TaskOnClick(){
		puzzle.PuzzleExit ();
	}
}
