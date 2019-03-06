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
using UnityEngine.UI;

public class TextZone : MonoBehaviour {
	//my scrambled word text
	public Text MyText;
	//list of input fields for cryptogram
	public List<InputField> inFields;
	//ref to cryptogram
	public Crytogram myCryp;
	//the coded and actual word 
	public string codedWord, actWord;
	//my colours for the fields
	public Color myCol;
	public Color redC;
	// Use this for initialization
	void Start () {
		//add listener for each field
		inFields.ForEach(x => x.onValueChanged.AddListener(delegate {
			ValueChangeCheck(x);}));
		//set colours
		ColorUtility.TryParseHtmlString ("01764FFF",out myCol);
		ColorUtility.TryParseHtmlString ("#FF002FFF",out redC);
	}
	// Update is called once per frame
	void Update () {
		//Changes all letters to Uppercase
		foreach (InputField x in inFields) {
			if (x.text != "") {
				//changes colour of fields to the default colour and text to "" if non alpha character
				if ((x.text.ToUpper () [0] < 'A') || x.text.ToUpper () [0] > 'Z') {
					x.image.color = Color.white;
					x.placeholder.color = myCol;
					x.image.color = myCol;
					x.text = "";
				}
			}
			//change to upper
			if (x.text.ToUpper () != x.text) {
				x.text = x.text.ToUpper ();
			}
		}
	}

	//Sets the scrambled message
	public void setTextZone(string inString){
		string temp = "";
		foreach (char c in inString) {
			temp += c + " ";
		}
		this.MyText.text = temp;
	}

	//sets the listener method call for current input field
	public void ValueChangeCheck(InputField inF){
		//updates the legend of letters used(changes the text in all fields)
		int counter = inFields.IndexOf (inF);
		if (inF.text != "") {
			myCryp.updateAlphaLegend (codedWord [counter], inF.text.ToUpper() [0]);
		} else {
			myCryp.updateAlphaLegend (codedWord [counter], '0');
			//change colour to default
			inF.image.color = myCol;
		}

	}
		
	//see if the word in all input fields match the unscrambled word
	public bool checkWord(){
		string myTextWord = "";
		foreach (InputField inF in inFields) {
			myTextWord += inF.text;
		}
		return (myTextWord == actWord);
	}

	//if the letter is wrong change field colour to red
	public void setWrong(List<char> x){
		foreach (InputField inF in this.inFields) {
			if (inF.text != "") {
				if (x.Contains (inF.text [0])) {
					inF.image.color = Color.white;
					inF.placeholder.color = redC;
					inF.image.color = redC;
				} else {
					inF.image.color = Color.white;
					inF.placeholder.color = myCol;
					inF.image.color = myCol;
				}
			}
		}
	}
}
