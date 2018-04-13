// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * This class is used to exit a puzzle, using the assgined button
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {

	public Button exitButton;
	private Puzzle puzzle;

	void Start(){
        //Set refs
		Button btn = exitButton.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);
		puzzle = GetComponentInParent<Puzzle> ();

	}
	//Trigger a puzzle exit call
	void TaskOnClick(){
		puzzle.PuzzleExit ();
	}
}
