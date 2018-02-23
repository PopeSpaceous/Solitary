using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FlipBtn : MonoBehaviour {


	public Button fButt;
	public Tans parTan;


	// Use this for initialization
	void Start () {
		Button btn = fButt.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);
		//Tans myTan = parTan.GetComponent<Tans> ();

	}

	void TaskOnClick(){
		if (parTan.type == 2) {
			parTan.Flip ();
		}
	}


}
