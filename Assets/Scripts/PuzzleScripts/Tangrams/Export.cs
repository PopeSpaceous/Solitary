using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class Export : MonoBehaviour {

	string winDir = System.Environment.GetEnvironmentVariable("windir");
	public Button fButt;
	public Tangrams myTan;

	// Use this for initialization
	void Start () {
		Button btn = fButt.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);
		//Tans myTan = parTan.GetComponent<Tans> ();

	}

	void TaskOnClick(){
		
		switch (this.name) {
		case "Easy":
			Debug.Log ("Printing to File");
			StreamWriter writer = new StreamWriter ("tanEasy.txt", append: true);
			int fL = (int)(new FileInfo ("tanEasy.txt").Length);
			writer.WriteLine (myTan.writeToFile (fL));
			writer.Close ();
			//export to easy file
			break;
		case "Med":
			//export to med file
			break;
		case "Diff":
			//export to difficult file
			break;
		}
	}


}
