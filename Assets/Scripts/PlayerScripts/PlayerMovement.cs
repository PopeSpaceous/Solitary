using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	//For Walking
	public float movementSpeed;

	//For Jumping
	public float jumpForce; 
	public Transform groundCheck;
	public LayerMask whatsGround;
	public float jumpAmmount = 200f; // jumpAmmount is the max ammount of jump the character has.
	public float decreaseAmmountBy;
	public bool isJumping = false;
	public bool isGrounded = false;

	private float currentJumpAmmount = 0; 	// currentJumpAmmount is the current ammount left while the character is jumping
	private float groundRadius = 0.4f; // TODO: value needs to be adjusted for our spirte's feet in order to get an accrute edge to edge ground detection.

	private Rigidbody2D rigBod;

	void Awake () {
		//Gets the reference of the attached Rigidbody2D componment from the object
		rigBod = GetComponent<Rigidbody2D>();
	}

	//FixedUpdate will handle the response from the user input
	/*   Note for jumping animation: 
	* 		isJumping can be use from the start of the jump to the apex.
	*		isGrounded can be use when the character is just falling.
	*		If there is no falling animation just use isJumping.
	**/
	void FixedUpdate(){
		
		//checks if a circle that will be below our player's feet is tonching the ground
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatsGround);

		//Check if the character is not jumping already and is touching ground
		// If its ready for a jump, add a ammount for the jump
		if(!isJumping && isGrounded){
			currentJumpAmmount = jumpAmmount;
		}
		//This if will cancel the jump if the character has hit his head on a object.
		if(isJumping && rigBod.velocity.y == 0){
			isJumping = false;
			currentJumpAmmount = 0; 
		}

		//Response for jumping. If the user is pressing the jump buttion and there is an ammount for a jump, the actual jump will trigger.
		if (Player.instance.jumpInput && currentJumpAmmount > 0) {
			rigBod.velocity = new Vector2(rigBod.velocity.x, jumpForce); // Add a bit of y velocity 
			isJumping = true;
			currentJumpAmmount -= decreaseAmmountBy;
		} 
		else {
			isJumping = false;
			currentJumpAmmount = 0;
		}
			
		/* Jumping animation flag, Place Here.*/	

		//Response for horizontal movement
		rigBod.velocity = new Vector2(Player.instance.horizontalInput * movementSpeed, rigBod.velocity.y);

		/* Walking animation flag, Place Here */

	}




}
