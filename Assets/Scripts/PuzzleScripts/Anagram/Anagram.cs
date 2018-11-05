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
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Anagram: Puzzle {
	public List<Text> myDisplayText= new List<Text> ();
	public List<InputField> myInput = new List<InputField> ();
	public List<string> myWords = new List<string>(new string[]{"",""});
	List<string> myScramWords = new List<string>();



	// Sets the parent fields
	void Awake () {
		puzzleName = "Anagram";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		Debug.Log ("Difficulty for puzzle " + puzzleName + " is: "+ this.difficulty);
		//calls setup function
		setup ();
		//sets a listener if someone changes the value of the text
		myInput[0].onValueChanged.AddListener(delegate {ValueChangeCheck(); });
		myInput[1].onValueChanged.AddListener(delegate {ValueChangeCheck1(); });
		//initialize the text to empty(so theres no null errors)
		myInput[0].text = "";
		myInput[1].text = "";
	}
	//changes the colour of the first input field to it's original colour
	public void ValueChangeCheck(){
		Color mycol; 
		ColorUtility.TryParseHtmlString ("#047E55FF",out mycol);
		myInput [0].image.color = mycol;

	}
	//changes the colour of the second input field to it's original colour
	public void ValueChangeCheck1(){
		Color mycol; 
		ColorUtility.TryParseHtmlString ("#047E55FF",out mycol);
		myInput [1].image.color = mycol;
	}


	void Update(){
		//changes all input to uppercase
		foreach (InputField inF in myInput) {
			if (inF.text != "") {
				inF.text = inF.text.ToUpper ();
			}
		}
	}


	void setup(){
		//check difficulty
		switch (this.difficulty) {
		case 1:
			//get a string from the 4letter word  file
			myWords [0] = getStringFromFile (4);
			//scrambles the words
			scrambleWords (0);
			//sets the text to display the scrambled word
			myDisplayText [0].text = myScramWords [0];
			myInput [1].text = "";
			//deletes the second input field
			myInput [1].placeholder.GetComponent<Text> ().text = "";
			Destroy (myInput [1].image);
			Destroy (myInput [1]);
			Destroy (myDisplayText [1]);
			break;
		case 2:
			//same steps as previous one, except it grabs a 5 or 6 letter word instead
			myWords [0] = getStringFromFile (Random.Range (5, 7));
			scrambleWords (0);
			myDisplayText [0].text = myScramWords [0];
			myInput [1].text = "";
			myInput [1].placeholder.GetComponent<Text> ().text = "";
			Destroy (myInput [1].image);
			Destroy (myInput [1]);
			Destroy (myDisplayText [1]);
			break;
		case 3:
			//similar to the other two, but this one grabs 2, 5-6 letter words
			myWords [0] = getStringFromFile (Random.Range (5, 7));
			scrambleWords (0);
			myDisplayText [0].text = myScramWords [0];
			myWords [1] = getStringFromFile (Random.Range (5, 7));
			scrambleWords (1);
			myDisplayText [1].text = myScramWords [1];
			break;
		default:
			Debug.Log ("Error in the anagram word extraction");
			break;

		}

	}

	public string getStringFromFile (int x){
		
		//textString holds the string data from the file which will be split by delimeter
		//retString is the string we will return to Start() function
		List<string> textString = new List<string>(); 
		//create Resource functor
		PuzzleResourceClass pr = new PuzzleResourceClass (); 
		string retString = "";
		if (x == 6) {
			//get 6 letter word list
			textString = new List<string>(pr.GetSix());
		}
		else if (x == 5) {
			//get 5 letter word list
			textString = new List<string>(pr.GetFive());
		} else {
			//get 4 letter word list
			textString = new List<string>(pr.GetFour());
		}
		//get a random integer between 0 and thelength(exclusively) of the string
		int r = (int)(Random.Range (0, (float)textString.Count));
		//gets a random string from the list
		retString = textString [(int)r];
		return retString;
	}

	//function to scramble the words, parameter is the index of the word to scramble
	void scrambleWords(int i){
		string tempWord = "";
		//array of ints to store which indexs we've stored already
		List<int> array = new List<int> ();
		//check length
		while (tempWord.Length != myWords [i].Length) {
			//get a random integer from 0 - length of the string
			int r = Random.Range (0, myWords [i].Length);
			//check to see if index exists in array
			if (!array.Contains (r)) {
				//add index, and add the char at [index] to the temp word
				array.Add (r);
				tempWord += myWords[i][r];
			}
		}
		//if the word isn't the same, add it. Otherwise, recur the function
		if (tempWord != myWords [i]) {
			myScramWords.Add (tempWord);
		} else if (tempWord == myWords [i]) {
			scrambleWords (i);
		}
	}


}
	