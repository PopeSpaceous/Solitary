using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerScreen : MonoBehaviour {
	bool showScreen =false;

	public Image screen;
	public Button exButt;
	public Text logText;
	public Text butttext;
	string textToDisplay ="";
	List<string> Logs= new List<string>();
	public List<Door> hubDoors = new List<Door>();
	private float regAlpha;
	// Use this for initialization
	void Start () {
		regAlpha=screen.canvasRenderer.GetAlpha();
		addLogs ();
		toggleView ();
	}
	// Update is called once per frame
	void Update () {
	}




	public void toggleView(){
		if (showScreen) {
			getText ();
			Show (screen.canvasRenderer);
			Show (logText.canvasRenderer);
			exButt.image.gameObject.SetActive (true);
			Player.instance.ChangeMovementLock (false);
			Player.instance.actionButtion = false;
			animateText ();
			showScreen = false;
		} 
		else {
			Hide (screen.canvasRenderer);
			Hide (logText.canvasRenderer);
			exButt.image.gameObject.SetActive (false);
			Player.instance.ChangeMovementLock (true);
			Player.instance.actionButtion = false;
			showScreen = true;
		}
	}

	void getText(){
		textToDisplay = "";
		textToDisplay += Logs [0];
		for (int i = 1; i < 5; i++) {
			if (!hubDoors [i].isDoorlocked) {
				textToDisplay += "\n\n" + Logs [i];
			}
		}
		if (Player.instance.playerProgress.level5) {
			textToDisplay +="\n\n" + Logs [5];
		}
	}

	void animateText(){
		logText.text = "";
			StartCoroutine( wait ());

	}
	IEnumerator wait(){
		exButt.image.gameObject.SetActive (false);
		foreach (char chr in textToDisplay ) {
			yield return new WaitForSeconds (0.00001f);
			logText.text += chr;
		}
		exButt.image.gameObject.SetActive (true);
	}

	public void Hide(CanvasRenderer Cr){
		Cr.SetAlpha (0f);
	}
	public void Show(CanvasRenderer Cr){
		Cr.SetAlpha (regAlpha);
	}



	public void addLogs(){
		Logs.Add ("Log 1: Transmission successful. Power failure. Transmission lost. Auxiliary power running");
		Logs.Add ("Log 2: Power restored to system. Engines offline. Missing lift access key. ");
		Logs.Add ("Log 3: Warning: fire in Engine room. Vent fire before entering.");
		Logs.Add ("Log 4: Fire extinguish successful.");
		Logs.Add ("Log 5: Power restored to engines.");
		Logs.Add ("Log 6. Transmission restored. Transmission interrupted. Transmission received from nearby source. Static on line. ---- Phobos---- Help ---- We----. Transmission lost.");
	}



}
