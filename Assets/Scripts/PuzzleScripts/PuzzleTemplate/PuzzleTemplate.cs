using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleTemplate: Puzzle {

	// Use this for initialization
	void Awake () {
		puzzleName = "PuzzleTemplate";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		Debug.Log ("Difficulty is: "+ this.difficulty);
	}


	/* your wonderful puzzle code here :3 */

}
