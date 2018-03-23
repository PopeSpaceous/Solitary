using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageScramble: Puzzle {

    public GameObject easyPuzzle;
    public GameObject mediumPuzzle;
    public GameObject hardPuzzle;


    // Sets the parent fields
    void Awake () {
		puzzleName = "ImageScramble";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		Debug.Log ("Difficulty for puzzle " + puzzleName + " is: "+ this.difficulty);
	}


    /* Your wonderful startup puzzle code here :3 */

    void Start () {
        if (difficulty == 1) {
            Object.Instantiate (easyPuzzle);
        }

        if (difficulty == 2) {
            Object.Instantiate (mediumPuzzle);
        }

        if (difficulty == 3) {
            Object.Instantiate (hardPuzzle);
        }
    }

}
