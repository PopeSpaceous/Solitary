using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCamera : MonoBehaviour {

	//Player Camera
	private Camera playerCam;
	//Puzzle Camera
	private Camera puzzleCam;

	// Use this for initialization
	void Start () {
		playerCam = Player.instance.GetComponentInChildren<Camera> ();
		puzzleCam = GetComponent<Camera> ();

		setPuzzleCamera ();
	}

	//Set the camera location and size the same as the main camera
	void setPuzzleCamera(){
		puzzleCam.transform.position = playerCam.transform.position;
		puzzleCam.orthographicSize = playerCam.orthographicSize;
	}
}
