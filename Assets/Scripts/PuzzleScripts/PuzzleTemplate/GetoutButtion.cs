using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetoutButtion : MonoBehaviour {

	public Button outButton;

	void Start(){

		Button btn = outButton.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);


	}
		
	void TaskOnClick(){
		//go thru puzzle first!
		//Debuging...
		Debug.Log("Clicked! outta here");
		NextSceneManager.instance.UnloadPuzzleScene("PuzzleTemplate");
	}
}
