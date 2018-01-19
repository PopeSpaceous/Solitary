using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed = 20f;

	private Rigidbody2D rigBod;
	private float horizontalInput;


	void Awake () {
		//Gets the reference of the attached Rigidbody2D componment from the object
		//Using Rigidbody2D will apply the object to Unity's phyiscs engine.
		rigBod = GetComponent<Rigidbody2D>();
	}


	// This Update function will be use to check user Input
	void Update () {
		//Checks if the user pressed 'a' or 'b' keys.
		//If 'a' horizontalInput will be -1, if 'b' horizontalInput will be 1
		horizontalInput = Input.GetAxis ("Horizontal");
	}

	//FixedUpdate will handle the response from the user input
	void FixedUpdate(){
		//Response for horizontal movement
		rigBod.velocity = new Vector2(horizontalInput * movementSpeed, rigBod.velocity.y);

	}


}
