using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckButton : MonoBehaviour {
	public Anagram myAnagram;
	public Button myButt;

	// Use this for initialization
	void Start () {
		//set listener
		myButt.onClick.AddListener (TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
	}
	void TaskOnClick(){
		List<bool> comp = new List<bool>();
		int counter = 0;
		//check each input field
		foreach (InputField inField in myAnagram.myInput) {
			//if both input field and scrambled word are blank, then break out of loop 
			if (inField.text == "" && myAnagram.myWords [counter] == "") {
				break;
			}
			//if the infield is empty, make it false
			if (inField.text == "") {
				comp.Add(false);
			} else {
				//otherwise add it in to the bool array
				comp.Add(inField.text == myAnagram.myWords [counter].Trim ());
			}
			//change the colour of the input field if it is wrong
			if (!comp[counter]) {
				inField.image.color = Color.red;
			}
			counter++;
		}
		//if any of the bools are false fail to complete puzzle
		if (!comp.Contains(false)) {
			myAnagram.PuzzleComplete ();
		}
	}
	


}
