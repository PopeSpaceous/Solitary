// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Jacob Holland
// Date: 04/13/2018
/* Summary: controlls the images and moves them on click.
 *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour {

    public GameObject target;
    public bool startMove = false;

    GameController gameMN;

	// Use this for initialization
	void Start () {
        GameObject gamemanager = GameObject.Find ("GameController");
        gameMN = gamemanager.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (startMove) {
            startMove = false;
            this.transform.position = target.transform.position; // move to new position
            gameMN.checkComplete = true;
        }
	}
}
