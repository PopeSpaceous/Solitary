using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextZone : MonoBehaviour {
	public Text MyText;
	public List<InputField> inFields;
	public Crytogram myCryp;
	public string codedWord, actWord;

	// Use this for initialization
	void Start () {
		inFields.ForEach(x => x.onValueChanged.AddListener(delegate {
			ValueChangeCheck(x);}));
	}
	
	// Update is called once per frame
	void Update () {
		//Changes all letters to Uppercase
		foreach (InputField x in inFields) {
			if (x.text != "") {
				if ((x.text.ToUpper () [0] < 'A') || x.text.ToUpper () [0] > 'Z') {
					x.image.color = Color.white;
					x.text = "";
				}
			}
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

	public void ValueChangeCheck(InputField inF){

		int counter = inFields.IndexOf (inF);
		if (inF.text != "") {
			myCryp.updateAlphaLegend (codedWord [counter], inF.text.ToUpper() [0]);
		} else {
			myCryp.updateAlphaLegend (codedWord [counter], '0');
			inF.image.color = Color.white;
		}

	}
		
	public bool checkWord(){
		string myTextWord = "";
		foreach (InputField inF in inFields) {
			myTextWord += inF.text;
		}
		return (myTextWord == actWord);
	}

	public void setWrong(List<char> x){
		foreach (InputField inF in this.inFields) {
			if (inF.text != "") {
				if (x.Contains (inF.text [0])) {
					inF.image.color = Color.red;
				} else {
					inF.image.color = Color.white;
				}
			}
		}
	}
}
