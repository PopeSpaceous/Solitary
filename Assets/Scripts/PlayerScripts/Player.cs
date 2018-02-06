using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player instance = null;

	public bool canPlayerMove = true;

	//This button will be used for player intrection on doors and puzzles
	[HideInInspector] 
	public bool actionButtion = false;
	[HideInInspector] 
	public bool jumpInput = false;
	[HideInInspector] 
	public float horizontalInput;


	void Awake () {
		
		//Set the instance only once.
		if (instance == null) {			
			instance = this;
		} else if (instance != this) {
			//Enforces that there will always be one instance of a gameObject. This is for type errors prevention
			Destroy (gameObject);
		}
			
		//Makes the gameobject not be unloaded when entering a new scene
		DontDestroyOnLoad (this);
	}

	// This Update function will be use to check user Input
	void Update(){
		if (canPlayerMove) {
			CheckInput ();
		} else {
			actionButtion = false;
			horizontalInput = 0;
			jumpInput = false;
		}
	}

	void CheckInput(){
		actionButtion = Input.GetButton ("Action");

		//Checks if the user pressed 'a' or 'b' keys based on InputManager
		horizontalInput = Input.GetAxis ("Horizontal");

		//Checks if the user pressed the Jumping Key based on InputManager
		jumpInput = Input.GetButton("Jump");
	
	}

}
