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
using System.IO;

public class Tangrams: Puzzle {
	// Sets the parent fields
	void Awake () {
		puzzleName = "Tangrams";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
	
	}


	/* Your wonderful startup puzzle code here :3 */

	//List of the tans associated with this tangram
	public List<Tans> myTans;
	//a bool to see if its the outline tangram or movable one
	public bool isStationary;
	//A list to see how many tans are in the right spot
	public 	List<bool> tansSolved;
	int counter =0;
	public PuzzleCamera pC; 

	//initialize the tangrams in puzzle(randomize location for scramble puzzle)
	void Start(){
		//if its the outline tangram 
		if (isStationary) {
			//gets a random set of coordinates for the tans from file based on difficulty
			string s = getStringFromFile ();
			//If its an empty string, something went wrong
			if (!s.Equals("")) {
				//splits string by the ':' delim, seperating each tan's coordinates
				string[] allCoord = s.Split (':');
				//gets rid of any pesky newlines
				foreach (string x in allCoord) {
					x.Replace ("\r\n", "");
				}

				int counter = 0;
				foreach (Tans myT in myTans) {
					//split string by comma delims. X pos, y Pos, y Rot, z rot, is flipped
					string[] tanCoord = allCoord [counter].Split (',');
					//make temp variables to hold the x,y coordinates
					float tempX = float.Parse (tanCoord [0]), tempY = float.Parse (tanCoord [1]);
					//transforms the positions
					Vector3 newPosition = new Vector3 (tempX+pC.transform.position.x, tempY+pC.transform.position.y, 1.0f);
					myT.transform.position = newPosition;
					//myT.snapToGrid();
					//transforms the rotation of tan
					myT.transform.Rotate (new Vector3 (0.0f, (float.Parse (tanCoord [2])),0.0f ));
					myT.rotateTan((float.Parse (tanCoord [3])));

					//check if flipped
					myT.flipped = (tanCoord [4] == "0") ? false : true;	
					myT.transform.localScale += new Vector3 (0.04f, 0.04f, 0);
					counter++;
				}
			}
		}

	}

	//passing in the puzzle Tangram
	public bool checkSolve(Tangrams x){
		counter = 0;
		//Go through each Tan in the Movable Tangram
		foreach (Tans objT in myTans){
			//loop through each tan in the Outline Tangram
			foreach (Tans puzT in x.myTans) {
				//check to see if the positions match
				if (objT.checkPos(puzT)) {
					//check to see if the tan types match
					if (objT.type == puzT.type) {
						//check to see if both have the same rotation value
//						Debug.Log("Type Matches");
						if (objT.checkRotation (puzT)) {
//							Debug.Log ("Rotation Matches");
							//if all those checks match, then set the tan's solved value to true
							tansSolved [counter] = true;
							//go to next tan
							break;
						}
//								else {
//							Debug.Log ("MyTan Dir: " + objT.direction + "  InTan dir: " + puzT.direction);
//						}
					} 
				//if the positions don't match on any tan set that counter to false
				} else {
					tansSolved [counter] = false;
				}
			}
			foreach (Tans qq in myTans) {
				//checks to see if any tans are overlapping
				if (!objT.Equals (qq) && objT.myTanPosition.Equals (qq.transform.position)) {
					tansSolved[counter] = false;
				}
			}
			//if there are any unsolved 
			counter++;
		}
		foreach (bool ii in tansSolved) {
			int xx = 0;
			if (!ii) {
//				Debug.Log ("My Tan: " + myTans [xx].name + " Did not solve correctly");
				return false;
			}
					xx++;
		}
		if (counter < 7) {
			if (!tansSolved [counter]) {
				//Debug.Log ("Tan : "+ myTans[counter].name +" Doesn't Match");
				return false;
			}
		}
		//send this message if the tangram is solved. Should be removed when we are finished the game
	
		this.PuzzleComplete ();
		return true;
	}

	//This converts the current position of the movable tangram to a string. Passed in an int to see if the file is empty or not
	public string writeToFile(int fLength){
		//set up the string that is to be returned
		string outString = "";
		//if there is nothing in the file, don't add a semicolon, else add a semicolin to end the last Tangram 
		outString+=(fLength > 0) ? ";" : "";
		//counts how many tans we've worked with
		int count = 0;
		//loop through the movable tans
		foreach (Tans t in myTans) {
			//if its the first tan, add nothing else add a colon and newline
			outString += (count == 0) ? "" : ":\r\n";
			//add x,y position, y,z rotation, and if its flipped
			outString += (Mathf.Round((t.transform.position.x -pC.transform.position.x)*10000)/10000).ToString () 
				+ "," +(Mathf.Round((t.transform.position.y - pC.transform.position.y)*10000)/10000).ToString () 
				+ "," + ((int)t.transform.eulerAngles.y).ToString ()
				+ "," + ((int)t.transform.eulerAngles.z).ToString () + "," + 
				((t.flipped) ? 1 : 0).ToString ();
			//add to count
				count++;
		}
		//return the string to Export script
		return outString;
	}

	//This function gets a random tangram string with coordinates from a file.
	string getStringFromFile (){
		//textString holds the string data from the file which will be split by delimeter
		//retString is the string we will return to Start() function
		string textString="", retString;
		//create Resource functor
		PuzzleResourceClass pr = new PuzzleResourceClass (); 
		//Opens a stream reader

		//check the difficulty of the puzzle
		switch (this.difficulty){
		case 1:
			//load an easy puzzle
			textString = pr.GetTanEasy();
			break;
		case 2:
			//load med puzzle
			textString = pr.GetTanMed();
			break;
		case 3:
			//load hard puzzle
			textString = pr.GetTanDiff();
			break;
		default:
			//Debug.Log ("No Difficulty Assigned");
			return "";
		}
		//clone the split string by ';' delim
		string[] parsedString = (string[]) (textString.Split(';')).Clone();
		//get a random integer between 0 and thelength(exclusively) of the string
		int r = (int)(Random.Range(0, (float)parsedString.Length));
		//Get the random tan coordinate from the string array
		retString = parsedString [(int)r];
		return retString;
	}
}

