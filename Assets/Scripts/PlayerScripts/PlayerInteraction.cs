using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	//This button will be used for player intrection on doors and puzzles
	[HideInInspector] 
	public bool actionButtion = false;

	void Update () {
		actionButtion = Input.GetButton ("Action");

	}
}
