using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class Crytogram: Puzzle {
	//List of alphabet a-z
	public List<char> alpha= new List<char>();
	//list of Alphabet Cipher uses cipherword then (a-z) without duplicate letters
	List<char> puzCipher = new List<char>();
	//My associated text Zones
	public List<TextZone> myZones;
	//My list of scrambled letters so far
	public List<char> scramAlphaList = new List<char>();
	//visual Scrambled Alphabet that you see in UI
	public Text scramAlpha;

	//not used?
	List <char> scramTextList = new List<char> ();
	//List<char> TempCharList = new List<char> ();
	string cipherWord;
	//65-90
	string myWord;
	string mySWord = "";
	public Text pText;


	// Sets the parent fields
	void Awake () {
		scramAlpha.text = "";
		for (int q = 0; q < 26; q++) {
			scramAlphaList.Add ('0');
		}
		cipherStart ();
		string mySWord = "";
		foreach (var z in this.myZones) {
			//set mysWord to blank
			this.mySWord = "";
			//get a random word from list
			this.myWord = this.getStringFromFile ((int)z.inFields.Count);
			//give actual word to current z target
			z.actWord = this.myWord;
		
			foreach (char x in this.myWord) {
				this.mySWord = this.mySWord + this.scramble (x).ToString ();
			}
			z.codedWord = this.mySWord;
			z.setTextZone (this.mySWord);
		}
		//pText = GetComponent<Text>();
		puzzleName = "Crytogram";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		setHints ();



	}


	void Update () {
		//this.updateText ();
	}

	/* Your wonderful startup puzzle code here :3 */

	void cipherStart(){
		for (int x = 65; x < 91; x++) {
			alpha.Add ((char)x);
		}
		//gets a word from file
		cipherWord = getStringFromFile (5);
		//adds word to beginning of cipher (character instance only once)
		foreach (char x in cipherWord) {
			if (!puzCipher.Contains (x)) {
				puzCipher.Add (x);
			}

		}
		foreach(char x in alpha){
			if (!puzCipher.Contains ((char)x)) {
				puzCipher.Add ((char)x);
			}
		}
	}



	public string getStringFromFile (int x){
		//textString holds the string data from the file which will be split by delimeter
		//retString is the string we will return to Start() function
		List<string> textString = new List<string>(); 
		string retString = "";
		//Opens a stream reader
		StreamReader sr;
		if (x == 5) {
			//check the difficulty of the puzzle
			sr = new StreamReader ("5LWords.csv");
		} else {
			sr = new StreamReader ("4LWords.csv");
		}
		//grab each line in the file
		do {
			textString.Add(sr.ReadLine ());
		} while (sr.Peek () != -1);
		//close the stream reader
		sr.Close ();
		//clone the split string by ';' delim

		//get a random integer between 0 and thelength(exclusively) of the string
		int r = (int)(Random.Range (0, (float)textString.Count));
		//Get the random tan coordinate from the string array
		retString = textString [(int)r];
		return retString;
	}

	public char scramble(char properChar){
		int indx = alpha.IndexOf(properChar);
		return puzCipher [indx];
	}

	public char unscrambleChar(char scramChar){
		
		var indx = puzCipher.IndexOf (scramChar);
		return alpha [indx];
	}




	public void updateText(){
		//counts what letter of alphabet we're on
		int counter = 0;
		//scramAlphaList.ForEach(x=>Debug.Log(x.ToString()));
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
					//	
						if (tZ.codedWord [thisLetter] == letter) {
							if (scramAlphaList[counter] == '0') {
								inF.text = "";
							} else {
								inF.text = ""+scramAlphaList[counter];
							}
						}
						thisLetter++;
					}
				}
			}
			counter++;
		}
	}

	public void updateAlphaLegend(char scramLetter, char inChar){
		if (inChar != '0') {
			if (scramAlphaList.Contains (inChar)) {
				int idx = scramAlphaList.IndexOf (inChar);
				scramAlphaList [idx] = '0';
			}
		}
		int alphaIndx = alpha.IndexOf (scramLetter);
		scramAlphaList [alphaIndx] = inChar;
		updateScramAlpha ();
		updateText ();
	
	}

	public void updateScramAlpha(){
		scramAlpha.text = "";
		foreach (char x in scramAlphaList) {
			if (x == '0') {
				scramAlpha.text += " \r\n";
			} else {
				scramAlpha.text += x.ToString () + "\r\n";
			}
		}
	}

	public bool checkAllWords(){
		foreach (TextZone tZ in myZones) {
			if (!tZ.checkWord ()) {
				return false;
			}
		}
		return true;
	}

	void setHints(){
		switch (this.difficulty) {
		case 1:
			//15letters given
			for (int x = 0; x < 15; x++) {
				int l = getRandomLetterIndex ();
				updateAlphaLegend (alpha [l],
					unscrambleChar(alpha[l]));
			}
			break;
		case 2:
			//12 letters given
			for (int x = 0; x < 12; x++) {
				int l = getRandomLetterIndex ();
				updateAlphaLegend (alpha [l],
					unscrambleChar(alpha[l]));
			}
			break;
		case 3:
			//10 letters given
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


	int getRandomLetterIndex(){
		int idx = (int)(Random.Range (0, 26));
		while (scramAlphaList [idx] != '0') {
			idx = (int)(Random.Range (0, 26));
		}
		return idx;
	}


	public void getWrongLetters(){
		List<char> wrongLetters = new List<char> ();
		for(int ii=0; ii < this.scramAlphaList.Count; ii++) {
			if (scramAlphaList[ii] != '0') {
				//Debug.Log ("Scrambled: " + this.scramAlphaList [ii] + " VS " + unscrambleChar (this.alpha [ii]) +" Regular Letter: " +this.alpha [ii]);
				if (this.scramAlphaList [ii] != unscrambleChar (this.alpha [ii])) {
					wrongLetters.Add(this.scramAlphaList[ii]);
				}
			}
		}
		this.myZones.ForEach(x=>x.setWrong(wrongLetters));
	}		
}

