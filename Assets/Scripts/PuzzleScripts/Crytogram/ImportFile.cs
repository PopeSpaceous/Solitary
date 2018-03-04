using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImportFile : MonoBehaviour {

	public string getStringFromFile (){
		//textString holds the string data from the file which will be split by delimeter
		//retString is the string we will return to Start() function
		string textString="", retString;
		//Opens a stream reader
		StreamReader sr;
		//check the difficulty of the puzzle
		sr = new StreamReader ("5LWords.csv");
	
		//grab each line in the file
		do {
			textString+=sr.ReadLine();
		} while (sr.Peek () != -1);
		//close the stream reader
		sr.Close ();
		//clone the split string by ';' delim
		string[] parsedString = (string[]) (textString.Split(',')).Clone();
		//get a random integer between 0 and thelength(exclusively) of the string
		int r = (int)(Random.Range(0, (float)parsedString.Length));
		//Get the random tan coordinate from the string array
		retString = parsedString [(int)r];
		return retString;
	}






}
