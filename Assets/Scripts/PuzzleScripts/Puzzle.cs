using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/* This will be the base class of all specific puzzle classes */
public abstract class Puzzle : MonoBehaviour {
	[HideInInspector]
	public bool isPuzzleComplete = false;

	protected string puzzleName;
	protected int difficulty;
	protected PuzzlePlaceholder placeholder;
    public int puzzleID; // must be set in inspector  


    // This function will allow for the door to unlock, call PuzzleExit to 
    //update the number of Puzzles completed, what difficulty, and unload the scene
    public void PuzzleComplete(){
		isPuzzleComplete = true;
        placeholder.PuzzleExit(isPuzzleComplete);
	}
    // Will call PuzzleExit to unload the scene
    public void PuzzleExit(){
        placeholder.PuzzleExit(isPuzzleComplete);
    }
}
