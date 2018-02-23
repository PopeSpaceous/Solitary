using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Tangrams: Puzzle {
	//needed for Text files
	string winDir = System.Environment.GetEnvironmentVariable("windir");
	// Sets the parent fields
	void Awake () {
		puzzleName = "Tangrams";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		Debug.Log ("Difficulty for puzzle " + puzzleName + " is: "+ this.difficulty);
	}


	/* Your wonderful startup puzzle code here :3 */
	public List<Tans> myTans;
	public bool isStationary;
	public 	List<bool> tansSolved;
	int counter =0;

	//initialize the tangrams in puzzle(randomize location for scramble puzzle)
	void Start(){
		this.difficulty = 1;
		if (isStationary) {
			//gets a random set of coordinates from file based on difficulty
			string s = getStringFromFile ();
			Debug.Log ("Length of s string split array: " +s.Length);
			if (!s.Equals("")) {
				string[] allCoord = s.Split (':');
				foreach (string x in allCoord) {
					x.Replace ("\r\n", "");
				}
				Debug.Log ("Split by new line");
				int counter = 0;
				foreach (Tans myT in myTans) {
					Debug.Log ("Length of new line split array: " +allCoord.Length);
					string[] tanCoord = allCoord [counter].Split (',');
					float tempX = float.Parse (tanCoord [0]), tempY = float.Parse (tanCoord [1]);
					Vector3 newPosition = new Vector3 (tempX, tempY, 1.0f);
					myT.transform.position = newPosition;
					Debug.Log ("Check y rot: "+float.Parse (tanCoord [2])+" z rot:" + float.Parse (tanCoord [3]));
					myT.transform.Rotate (new Vector3 (0.0f, (float.Parse (tanCoord [2])), (float.Parse (tanCoord [3]))));
					myT.flipped = (tanCoord [4] == "0") ? false : true;	
					counter++;
				}
			}
		}

	}

	//passing in the puzzle Tangram
	public bool checkSolve(Tangrams x){
		//tansSolved = new List<bool>(7){ false };
		//Debug.Log ("Start check solve");
		counter = 0;
		foreach (Tans objT in myTans){
			foreach (Tans puzT in x.myTans) {
				if (puzT.type ==1)
					//Debug.Log ((objT.type==1)?("Position of MoveTan "+ objT.myTanPosition.ToString() + "   -Other Object: " + puzT.myTanPosition.ToString()):"");
				//if (objT.myTanPosition.Equals (puzT.myTanPosition)) 
				if (objT.checkPos(puzT)) {
					if (objT.type==1 && puzT.type ==1)
						//Debug.Log ("Positions Match");
					if (objT.type == puzT.type) {
						//Debug.Log ("Types Match");
						if (objT.checkRotation (puzT)) {
							//Debug.Log ("Rotations Match-" + myTans [counter].name);
							tansSolved [counter] = true;
							break;
						}
					} 
						//Debug.Log ("Types Don't match");
				} else {
					if (objT.type==1 && puzT.type ==1)
						//Debug.Log ("Positions Don't Match");
					tansSolved [counter] = false;
				}
			}
			foreach (Tans qq in myTans) {
				//checks to see if any tans are overlapping
				if (!objT.Equals (qq) && objT.myTanPosition.Equals (qq.transform.position)) {
					//Debug.Log ("Objects in the same position");
					return false;
				}
			}
			if (!tansSolved [counter]) {
				//Debug.Log ("Tan : "+ myTans[counter].name +" Doesn't Match");
				return false;
			}
			counter++;
		}
		//loop through list
			//check location
			//check orientation
			//check if flipped (parrellogram)
		//check if peices are overlapping each other
		//return check
		Debug.Log("****EVERYTHING MATCHES******");
		return true;
	}


	public string writeToFile(int fLength){
		string outString = "";
		outString+=(fLength > 0) ? ";" : "";
		int count = 0;
		foreach (Tans t in myTans) {
			outString += (count == 0) ? "" : ":\r\n";
			outString += t.transform.position.x.ToString () + "," +t.transform.position.y.ToString () + "," + 
				((int)t.transform.eulerAngles.y).ToString () + "," + ((int)t.transform.eulerAngles.z).ToString () + "," + 
				((t.flipped) ? 1 : 0).ToString ();
			if (count == 6)
				count = 0;
			else
				count++;
		}
		return outString;
		
	}

	string getStringFromFile (){
		//Debug.Log ("start read File");
		//string file;
		string textString="", retString;
		StreamReader sr;
		switch (this.difficulty){
		case 1:
			//load an easy puzzle
			Debug.Log ("Case 1");
			sr = new StreamReader ("tanEasy.txt");


			break;
		case 2:
			//load med puzzle
			//Debug.Log ("case 2");
			sr = new StreamReader ("tanMed.txt");
			break;
		case 3:
			//load hard puzzle
			//Debug.Log ("case 3");
			sr = new StreamReader ("tanDiff.txt");
			break;
		default:
			//Debug.Log ("No Difficulty Assigned");
			return "";
		}
		do {
			//Debug.Log ("reading lines");
			textString+=sr.ReadLine();
		} while (sr.Peek () != -1);
		//Debug.Log ("closed sr");
		sr.Close ();
		string[] parsedString = (string[]) (textString.Split(';')).Clone();
		Debug.Log ("Length of parsed string: " +parsedString.Length);
		//Debug.Log ("split string by ;");
		int r = (int)(Random.Range(0, (float)parsedString.Length));
		
		retString = parsedString [(int)r];
		Debug.Log ("Length of returned string: " +retString.Length);
		Debug.Log (retString);
		//Debug.Log ("Passed back this string: "+retString);
		return retString;
	}


}

