using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextZone : MonoBehaviour {
	public Text MyText;
	public List<InputField> inFields;
	public Crytogram myCryp;
	public string codedWord, actWord;
	public Color myCol;
	public Color redC;
	// Use this for initialization
	void Start () {
		inFields.ForEach(x => x.onValueChanged.AddListener(delegate {
			ValueChangeCheck(x);}));
		ColorUtility.TryParseHtmlString ("01764FFF",out myCol);
		ColorUtility.TryParseHtmlString ("#FF002FFF",out redC);
	}
	//
	// Update is called once per frame
	void Update () {
		//Changes all letters to Uppercase
		foreach (InputField x in inFields) {
			if (x.text != "") {
				if ((x.text.ToUpper () [0] < 'A') || x.text.ToUpper () [0] > 'Z') {
					x.image.color = Color.white;
					x.placeholder.color = myCol;
					x.image.color = myCol;
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

			inF.image.color = myCol;
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
