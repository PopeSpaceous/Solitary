using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/* This will be the base class of all specific puzzle class */
public abstract class Puzzle : MonoBehaviour {
	[HideInInspector]
	public bool isPuzzleComplete = false; // maybe keep

	protected string puzzleName;
	protected int difficulty;
	protected PuzzlePlaceholder placeholder;


	// This function will allow for the door to unlock, call level's WhenPuzzleComplete to 
	//update the number of Puzzle completed and what difficulty and unload the scene
	public void PuzzleComplete(){
		isPuzzleComplete = true;
		//unLock's the player's movement
		Player.instance.canPlayerMove = true;


		/*TODO: Level function call code here */


		if(placeholder.door != null){
			//set the bool to allow the door to open
			placeholder.door.DoorLockChange(true);
		}

		//Unload the puzzle scene
		NextSceneManager.instance.UnloadPuzzleScene (puzzleName);
	} 

	public void PuzzleExit(){
		//unLock's the player's movement
		Player.instance.canPlayerMove = true;
		//Unload the puzzle scene
		NextSceneManager.instance.UnloadPuzzleScene (puzzleName);
	}
}
