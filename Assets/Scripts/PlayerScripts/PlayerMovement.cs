// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * Logic for the player's movement with phyiscs.
 * Handles the walking, and jumping of the player
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	//Movement speed ammount
	public float movementSpeed;

	//For Jumping
	public float jumpForce;
    //Max ammount of jump the character has. This can be use for jump height
    public float jumpAmmount = 200f; 
    //Ammount to decrease by when jumping
	public float decreaseAmmountBy;
    //Bool to check if the player is jumping. Its true when the player is in the start to its apex of a jump
	public bool isJumping = false;
    //Bool to check if his feet is touching the ground
	public bool isGrounded = false;
    //Ammount in secounds the player can do another jump
    public float nextJumpDelay = 0.2f;
    //bool to check if the player has started the jump but is not in the air yet
    private bool jumpStarted = false;
    // currentJumpAmmount is the current ammount left while the character is jumping. 
    private float currentJumpAmmount = 0;   

    public Transform groundCheck;
    //Layers that are allowed to trigger a isGround true
    public LayerMask whatsGround;
    //Ammount to trigger a hard landing animation
    public float hardLandingThreshold = -16f;
    //Detection radius ammount for checking if the player is touching the ground
	private float groundRadius = 0.38f;

	private Rigidbody2D rigBod;    
    private SpriteRenderer sR;

    void Awake () {
		//Gets the references
		rigBod = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
    }

	//FixedUpdate will handle the response from the user input for jumping and walking
	void FixedUpdate(){

        //checks if a circle that will be below our player's feet is tonching the ground
        isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatsGround);

        //Check if the character is not jumping already and is touching ground
        // If its ready for a jump, add a ammount for the jump and actualy start the jump after a delay
        if (Player.instance.jumpInput && !isJumping && isGrounded && !jumpStarted && !Player.instance.animstate.GetBool("LandHard"))
        {
            currentJumpAmmount = jumpAmmount;
            StartCoroutine(JumpDelay());

        }
        else if (isJumping) // keep calling jump until the jump has ended
        {
            RunJump();
        }

        //Jump and fall flags
        Player.instance.animstate.SetBool("Grounded", isGrounded);
        Player.instance.animstate.SetFloat("JPotential", currentJumpAmmount + rigBod.velocity.y);
        Player.instance.animstate.SetBool("JumpStarted", jumpStarted);
        Player.instance.animstate.SetBool("IsJumping", isJumping);
        //Hard Landing check
        if (rigBod.velocity.y <= hardLandingThreshold) {
            Player.instance.animstate.SetBool("LandHard", true);
        }
        //will reset the HardLand bool if an Idle state has been triggered
        else if (Player.instance.animstate.GetCurrentAnimatorStateInfo(0).IsTag("Idle") ) { 

            Player.instance.animstate.SetBool("LandHard", false);
        }

        //Response for horizontal movement
        if (!jumpStarted )
        {
            rigBod.velocity = new Vector2(Player.instance.horizontalInput * movementSpeed, rigBod.velocity.y);

        }
        else {
            //cut the movement speed by half when a jump has started
            rigBod.velocity = new Vector2((Player.instance.horizontalInput) * (movementSpeed/2), rigBod.velocity.y);
        }
        //Walking Animation flags sets
        if (Player.instance.horizontalInput < 0)
        {
            sR.flipX = true;
        }
        else if (Player.instance.horizontalInput > 0)
        {
            sR.flipX = false;
        }
        Player.instance.animstate.SetFloat("WalkState", Mathf.Abs(Player.instance.horizontalInput));

        //Debuging Only
        //if (rigBod.velocity.y != 0) {
            //Debug.Log("Potential: " + currentJumpAmmount + rigBod.velocity.y + " Vel: " + rigBod.velocity.y);
        //}

    }

    void RunJump()
    {
        //Response for jumping. If the user is pressing the jump buttion and there is an ammount for a jump, the actual jump will trigger.
        if (Player.instance.jumpInput && currentJumpAmmount > 0)
        {
            rigBod.velocity = new Vector2(rigBod.velocity.x, jumpForce); // Add a bit of y velocity 
            isJumping = true;
            currentJumpAmmount -= decreaseAmmountBy;
        }
        else
        {
            //Stops adding force of the jump
            isJumping = false;
            currentJumpAmmount = 0;
        }
        //This if will cancel the jump if the character has hit his head on a object.
        if (isJumping && rigBod.velocity.y == 0)
        {
            isJumping = false;
            currentJumpAmmount = 0;
        }
    }

    IEnumerator JumpDelay() {
        jumpStarted = true;
        yield return new WaitForSeconds(nextJumpDelay);
        // start jump
        RunJump(); 
        jumpStarted = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

}
