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


public class Crytogram: Puzzle {
	//List of alphabet a-z
	public List<char> alpha= new List<char>();
	//list of Alphabet Cipher uses 3 words then (a-z) without duplicate letters
	List<char> puzCipher = new List<char>();
	//My associated text Zones
	public List<TextZone> myZones;
	//My list of scrambled letters so far
	public List<char> scramAlphaList = new List<char>();
	//visual Scrambled Alphabet that you see in UI
	public Text scramAlpha;
	//unused letters text zone reference
	public Text unUsed;

	//string to hold the cipher words
	string cipherWord="";
	//my current unscrambled word
	string myWord;
	//my scrambled word
	string mySWord;



	// Sets the parent fields
	void Awake () {
		//sets default value
		scramAlpha.text = "";
		//initalizes scrambled list to 0
		for (int q = 0; q < 26; q++) {
			scramAlphaList.Add ('0');
		}
		//starts cipher method
		cipherStart ();
		//got through each text zone
		foreach (var z in this.myZones) {
			//set mysWord to blank
			this.mySWord = "";
			//get a random word from list
			this.myWord = this.getStringFromFile ((int)z.inFields.Count);
			//give actual word to current z target
			z.actWord = this.myWord;
			//set the scrambled text field
			foreach (char x in this.myWord) {
				this.mySWord = this.mySWord + this.scramble (x).ToString ();
			}
			//set the coded word to current scrambled string
			z.codedWord = this.mySWord;
			//sets the scrambled text field
			z.setTextZone (this.mySWord);
		}
		puzzleName = "Crytogram";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		//calls hint method
		setHints ();
	}


	void Update () {
	}

	/* Your wonderful startup puzzle code here :3 */
	//creates a cipher alphabet for the puzzle
	void cipherStart(){
		//initalizes regular alphabet
		for (int x = 65; x < 91; x++) {
			alpha.Add ((char)x);
		}
		//gets a word from file 3 times
		cipherWord = getStringFromFile (5);
		cipherWord += getStringFromFile (5);
		cipherWord += getStringFromFile (5);
		//adds word to beginning of cipher (character instance only once)
		foreach (char x in cipherWord) {
			//if it doesn't contain the current character then update the alphabet
			if (!puzCipher.Contains (x)) {
				puzCipher.Add (x);
			}
		}
		//fill in the rest of the alphabet
		foreach(char x in alpha){
			if (!puzCipher.Contains ((char)x)) {
				puzCipher.Add ((char)x);
			}
		}
	}


	//gets a word from the file, parameter is #of lettered word 4 or 5
	public string getStringFromFile (int x){
		//textString holds the string data from the file which will be split by delimeter
		List<string> textString = new List<string>(); 
		//retString is the string we will return to Start() function
		string retString = "";
		//create Resource functor
		PuzzleResourceClass pr = new PuzzleResourceClass (); 
		if (x == 5) {
			//get 5 letter word list
			textString = new List<string>(pr.GetFive());
		} else {
			//get 4 letter word list
			textString = new List<string>(pr.GetFour());
		}
		//get a random integer between 0 and thelength(exclusively) of the string
		int r = (int)(Random.Range (0, (float)textString.Count));
		//Get the random tan coordinate from the string array
		retString = textString [(int)r];
		return retString;
	}
	//takes a char and runs it though ciphered alphabet to get corresponding scrambled letter
	public char scramble(char properChar){
		int indx = alpha.IndexOf(properChar);
		return puzCipher [indx];
	}

	//unscrambles a scrambled character
	public char unscrambleChar(char scramChar){
		var indx = puzCipher.IndexOf (scramChar);
		return alpha [indx];
	}


	//updates all the text in textfields
	public void updateText(){
		//counts what letter of alphabet we're on
		int counter = 0;
		//loops through each letter in Alphabet
		foreach (char letter in alpha) {
			//Loop through each textZone
			foreach (TextZone tZ in myZones) {
				//initialize a counter for each letter
				int thisLetter = 0;
				//see if word is found in the text zone string
				if (tZ.codedWord.Contains (""+letter) && (""+scramAlphaList[counter]) !="") {
					//for each input field in the textZone
					foreach (InputField inF in tZ.inFields) {
						//if they have matching letters in coded word
						if (tZ.codedWord [thisLetter] == letter) {
							//if the scrambled alphabet letter has no value 0, set it to ""
							if (scramAlphaList[counter] == '0') {
								inF.text = "";
							} else {
								//otherwise add the letter
								inF.text = ""+scramAlphaList[counter];
							}
						}
						//update current counter for letters
						thisLetter++;
					}
				}
			}
			//updates the scrambled alphabet list counter
			counter++;
		}
	}

	//updates the scrambled alphabet list seen at the bottom
	public void updateAlphaLegend(char scramLetter, char inChar){
		//Clear current char 
		if (inChar != '0') {
			if (scramAlphaList.Contains (inChar)) {
				int idx = scramAlphaList.IndexOf (inChar);
				scramAlphaList [idx] = '0';
			}
		}
		//updates the hint list with incoming chars and removes previous one
		int alphaIndx = alpha.IndexOf (scramLetter);
		scramAlphaList [alphaIndx] = inChar;
		updateScramAlpha ();
		updateText ();
	
	}

	//updates the currently scrambled alphabet list for the text field
	public void updateScramAlpha(){
		//init unused text & scramAlpha
		unUsed.text = "";
		scramAlpha.text = "";
		// see if it should be blank or add the character to the list
		foreach (char x in scramAlphaList) {
				if (x == '0') {
				scramAlpha.text += "  ";
			} else {
				scramAlpha.text += x.ToString () + " ";
			}
		}

		//add all unused characters to unused character text field
		foreach (char x in alpha) {
			if (!scramAlphaList.Contains (x)) {
				unUsed.text += x.ToString () + " ";
			}
		}
	}

	//checks all words if they're complete
	public bool checkAllWords(){
		foreach (TextZone tZ in myZones) {
			if (!tZ.checkWord ()) {
				return false;
			}
		}
		return true;
	}

	//this sets the amount of letters given based on difficulty
	void setHints(){
		switch (this.difficulty) {
		case 1:
			//15letters given if easy
			for (int x = 0; x < 15; x++) {
				int l = getRandomLetterIndex ();
				updateAlphaLegend (alpha [l],
					unscrambleChar(alpha[l]));
			}
			break;
		case 2:
			//12 letters given if medium
			for (int x = 0; x < 12; x++) {
				int l = getRandomLetterIndex ();
				updateAlphaLegend (alpha [l],
					unscrambleChar(alpha[l]));
			}
			break;
		case 3:
			//10 letters given if hard
			for (int x = 0; x < 10; x++) {
				int l = getRandomLetterIndex ();
				updateAlphaLegend (alpha [l],
					unscrambleChar(alpha[l]));
			}
			break;
		default:
			Debug.Log ("There was an error setting difficulty on Cryptogram");
			break;
		}
	}

	//gives back a random letter index from our alphabet. checks if duplicate as well
	int getRandomLetterIndex(){
		int idx = (int)(Random.Range (0, 26));
		while (scramAlphaList [idx] != '0') {
			idx = (int)(Random.Range (0, 26));
		}
		return idx;
	}

	//check to see what letters are wrong to highlight them
	public void getWrongLetters(){
		List<char> wrongLetters = new List<char> ();
		for(int ii=0; ii < this.scramAlphaList.Count; ii++) {
			if (scramAlphaList[ii] != '0') {
				if (this.scramAlphaList [ii] != unscrambleChar (this.alpha [ii])) {
					wrongLetters.Add(this.scramAlphaList[ii]);
				}
			}
		}
		this.myZones.ForEach(x=>x.setWrong(wrongLetters));
	}		
}

