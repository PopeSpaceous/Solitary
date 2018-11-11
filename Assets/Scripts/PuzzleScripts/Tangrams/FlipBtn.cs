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


//Button used to flip Parallelogram Tan
public class FlipBtn : MonoBehaviour {

	//references to The button and Parallelogram Tan
	public Button fButt;
	public Tans parTan;


	// Use this for initialization
	void Start () {
		Button btn = fButt.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);
		//Tans myTan = parTan.GetComponent<Tans> ();

	}
	//when button is clicked call the flip function if the Tangram is a paralellogram
	void TaskOnClick(){
		if (parTan.type == TangramType.Parallelogram) {
			parTan.Flip ();
		}
	}


}
