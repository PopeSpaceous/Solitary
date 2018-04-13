// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Nathan Misener
// Date: 04/13/2018
/* Summary: 
 * Choice Buttons that this triggered when the player has completed all the levels and is in the computer screen.
 * Whatever choice made, the game will trigger a exit sequence by first loading in the EndScreen scene
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChoiceButton : MonoBehaviour {

	public Button myButt;

	// Use this for initialization
	void Start () {
		Button btn = myButt.GetComponent<Button> ();
		btn.onClick.AddListener (taskOnClick);
	}
	void Awake(){
		Button btn = myButt.GetComponent<Button> ();
		btn.onClick.AddListener (taskOnClick);
	}

	void taskOnClick(){
		if (myButt.GetComponentInChildren<Text> ().text == "Go to Phobos") {
			Player.instance.playerProgress.goToPhobos = true;
		} else {
			Player.instance.playerProgress.goToPhobos = false;
		}
		//call end screen
		GameManager.instance.ExitGame("EndScreen");
	}
}
