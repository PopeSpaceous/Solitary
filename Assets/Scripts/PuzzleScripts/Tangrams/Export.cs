using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class Export : MonoBehaviour {
	//variable needed for file handling
	//reference to the button
	public Button fButt;
	//reference to the movable tangram 
	public Tangrams myTan;

	// Use this for initialization
	void Start () {
		//get the button
		Button btn = fButt.GetComponent<Button> ();
		//add an on click listener
		btn.onClick.AddListener (TaskOnClick);
		//Tans myTan = parTan.GetComponent<Tans> ();

	}
	//When button is clicked
	void TaskOnClick(){
		StreamWriter writer;
		int fL;
		//check the name of the button
		switch (this.name) {
		//if easy
		case "Easy":
			Debug.Log ("Printing to Easy File");
			//set the writer to open easy file and append to it
			writer = new StreamWriter ("tanEasy.txt", append: true);
			//get the length of the file to see if its empty
			fL = (int)(new FileInfo ("tanEasy.txt").Length);
			//call the tangrams write to file function
			writer.WriteLine (myTan.writeToFile (fL));
			//close the writer
			writer.Close ();
			//export to easy file
			break;
		case "Med":
			//export to med file
			Debug.Log ("Printing to Med File");
			writer = new StreamWriter ("tanMed.txt", append: true);
			fL = (int)(new FileInfo ("tanMed.txt").Length);
			writer.WriteLine (myTan.writeToFile (fL));
			writer.Close ();
			break;
		case "Diff":
			//export to difficult file
			Debug.Log ("Printing to Diff File");
			writer = new StreamWriter ("tanDiff.txt", append: true);
			fL = (int)(new FileInfo ("tanDiff.txt").Length);
			writer.WriteLine (myTan.writeToFile (fL));
			writer.Close ();
			break;
		default:
			Debug.Log ("Oops, something went wrong in Export script");
			break;
		}

	}


}
