using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Player will always be static throughout the game and forced to not be unloaded when a new scene is loaded */
public class Player : MonoBehaviour {

	//make this instance static so it can be used across scripts
	public static Player instance = null;

	//Used for locking the player movement
	public bool canPlayerMove = true;

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
			Debug.LogWarning ("Another instance of Player have been created and destoryed!");
		}
			
		//Makes the gameobject not be unloaded when entering a new scene
		DontDestroyOnLoad (this);
	}

	// This Update function will be use to check user Input
	void Update(){
		//Check if the player can move
		if (canPlayerMove) {
			
			CheckInput ();

		} else {
			
			actionButtion = false;
			jumpInput = false;
			horizontalInput = 0;

		}
	}

	void CheckInput(){
		//Action buttion will be used for any interactions
		actionButtion = Input.GetButton ("Action");

		//Checks if the user pressed 'a' or 'b' keys based on InputManager
		horizontalInput = Input.GetAxis ("Horizontal");

		//Checks if the user pressed the Jumping Key based on InputManager
		jumpInput = Input.GetButton("Jump");
	
	}

}
