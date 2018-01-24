using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	//For Walking
	public float movementSpeed = 20f;

	private float horizontalInput;

	//For Jumping
	public bool isGrounded = false;
	public float jumpForce; 
	public Transform groundCheck;
	public LayerMask whatsGround;

	private float groundRadius = 0.4f; // TODO: value needs to be adjusted for our spirte's feet in order to get an accrute edge to edge ground detection.
	private bool jumpInput;
	private bool isJumping = false;
	private Rigidbody2D rigBod;

	void Awake () {
		//Gets the reference of the attached Rigidbody2D componment from the object
		rigBod = GetComponent<Rigidbody2D>();
	}


	// This Update function will be use to check user Input
	void Update () {
		//Checks if the user pressed 'a' or 'b' keys based on InputManager
		horizontalInput = Input.GetAxis ("Horizontal");

		//Checks if the user pressed the Jumping Key based on InputManager
		jumpInput = Input.GetButton("Jump");

	}

	//FixedUpdate will handle the response from the user input
	void FixedUpdate(){
		
		//checks if a circle that will be below our player's feet is tonching the ground
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatsGround);

		//Check if the player is not jumping already
		if(rigBod.velocity.y == 0){
			isJumping = false;
		}

		//Response for jumping key input
		//This checks for jump input, and both if the player is not already jumping, and if he is on the ground.
		if(!isJumping && isGrounded && jumpInput ){
			rigBod.AddForce (new Vector2(0, jumpForce)); //Jump height is affected by added foce, mass, and gravity scale from Rigidbody 
			isJumping = true;
		}
		/* Jumping animation flag, Place Here. Note: isGrounded and isJumping are both used to determind if the player is jumping  */	


		//Response for horizontal movement
		rigBod.velocity = new Vector2(horizontalInput * movementSpeed, rigBod.velocity.y);

		/* Walking animation flag, Place Here */
	}


}
