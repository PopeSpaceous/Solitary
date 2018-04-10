using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RestartButton : MonoBehaviour {
	public Button myButt;
	public SimonSays game;
	// Use this for initialization
	void Start () {
		myButt.onClick.AddListener (taskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void taskOnClick(){
		if (game.firstTime) {
			game.StartGame ();
		} else {
			//call replay function
			game.ReplayPattern ();
		}
	}
}
