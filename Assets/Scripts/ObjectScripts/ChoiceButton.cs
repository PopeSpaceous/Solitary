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
