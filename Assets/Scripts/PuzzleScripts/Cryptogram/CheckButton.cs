// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Nathan Misener
// Date: 04/13/2018
/* Summary: 
 *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour {
	//refs cryptogram puzzle and self
	public Cryptogram Cryptogram;
	public Button myButt;
	// Use this for initialization
	void Start () {
		myButt.onClick.AddListener (TaskOnClick);
	}
	//calls check all word method on cryptogram
	void TaskOnClick(){
		if (Cryptogram.checkAllWords ()) {
			Cryptogram.PuzzleComplete ();
		} else {
			Cryptogram.GetComponent<AudioSource> ().Play ();
			Cryptogram.getWrongLetters ();
			//myText.text = Cryptogram.getWrongLetters ();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
