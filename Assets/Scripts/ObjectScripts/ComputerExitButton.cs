using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerExitButton : MonoBehaviour {
	public Button myButt;
	public ComputerScreen comp;
		
	// Use this for initialization
	void Start () {
		Button btn = myButt.GetComponent<Button> ();
		btn.onClick.AddListener (taskOnClick);
	}
	
	void taskOnClick(){
		if (!Player.instance.playerProgress.isGameCompleted) {
			comp.toggleView ();
		} else {
			comp.endChoice.gameObject.SetActive (true);
		}
	}

}
