// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
* This class will set the puzzle camera the same size and current position of the main camera 
* when the player first triggers the puzzleplaceholder.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCamera : MonoBehaviour {

	//Player Camera
	private Camera playerCam;
	//Puzzle Camera
	private Camera puzzleCam;
	//gameObject pf Puzzle Canvas
	private GameObject puzzle;

	// Use this for initialization
	void Start () {
		playerCam = GameObject.Find ("Main Camera").GetComponent<Camera> ();;
		puzzleCam = GetComponent<Camera> ();
		puzzle = GameObject.Find ("HackingTool");
		SetPuzzleCamera ();
	}

	//Set the camera location and size the same as the main camera
	//also sets the location of the puzzle gameobject (everything none UI will be the child of this gameObject)
	void SetPuzzleCamera(){
		puzzle.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y, 0);
		puzzleCam.transform.position = playerCam.transform.position;
		puzzleCam.orthographicSize = playerCam.orthographicSize;
	}
}
