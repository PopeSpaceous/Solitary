using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageScramble: Puzzle {

    GameController gameMN;
	public AudioSource mySound;
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
            GameObject easyPuzzle = Instantiate (Resources.Load("3x3 Puzzle"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            easyPuzzle.transform.parent = GameObject.Find ("Puzzle Canvas").transform;
            easyPuzzle.transform.localPosition = new Vector3 (-9.2f, 3.9f, 0);
        }

        if (difficulty == 2) {
            GameObject mediumPuzzle = Instantiate (Resources.Load ("4x4 Puzzle"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
            mediumPuzzle.transform.parent = GameObject.Find ("Puzzle Canvas").transform;
            mediumPuzzle.transform.localPosition = new Vector3 (-9.6f, 3.9f, 0);
        }

        if (difficulty == 3) {
            GameObject hardPuzzle = Instantiate (Resources.Load ("5x5 Puzzle"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
            hardPuzzle.transform.parent = GameObject.Find ("Puzzle Canvas").transform;
            hardPuzzle.transform.localPosition = new Vector3 (-8.3f, 3.9f, 0);
        }

        GameObject gamemanager = GameObject.Find ("GameController");
        gameMN = gamemanager.GetComponent<GameController> ();
		gameMN.mySound = mySound;
    }

    void Update () {
       if(gameMN.win == true) {
            PuzzleComplete ();
        } 
    }

}
