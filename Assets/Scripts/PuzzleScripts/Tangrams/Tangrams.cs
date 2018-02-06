﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tangrams: Puzzle {

	// Sets the parent fields
	void Awake () {
		puzzleName = "Tangrams";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		Debug.Log ("Difficulty for puzzle " + puzzleName + " is: "+ this.difficulty);
	}


	/* Your wonderful startup puzzle code here :3 */

}

