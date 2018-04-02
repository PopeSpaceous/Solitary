using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour {
	public Crytogram Cryptogram;
	public Button myButt;
	public Text myText;
	// Use this for initialization
	void Start () {
		myButt.onClick.AddListener (TaskOnClick);
	}
	void TaskOnClick(){
		if (Cryptogram.checkAllWords ()) {
			Cryptogram.PuzzleComplete ();
		} else {
			Cryptogram.getWrongLetters ();
			//myText.text = Cryptogram.getWrongLetters ();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
