﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//For Walking
	public float movementSpeed;
	public List<AudioSource> sounds = new List<AudioSource> ();
	bool playedOnce=false;
	//For Jumping
	public float jumpForce; 
	public float jumpAmmount = 200f; // jumpAmmount is the max ammount of jump the character has.
	public float decreaseAmmountBy;
	public bool isJumping = false;
	public bool isGrounded = false;
    public float nextJumpDelay = 0.2f;
    private bool jumpStarted = false;
    private float currentJumpAmmount = 0;   // currentJumpAmmount is the current ammount left while the character is jumping. This can be use for jump height
	bool jumpEnd = true;
	bool inAir =true;
    public Transform groundCheck;
    public LayerMask whatsGround;
    public float hardLandingThreshold = -16f;
	private float groundRadius = 0.38f;

	private Rigidbody2D rigBod;
    
    private SpriteRenderer sR;

    void Awake () {
		//Gets the references
		rigBod = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
		sounds = new List<AudioSource>( GetComponents<AudioSource> ());
    }

	//FixedUpdate will handle the response from the user input for jumping and walking
	void FixedUpdate(){

        //checks if a circle that will be below our player's feet is tonching the ground
        isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatsGround);

        //Check if the character is not jumping already and is touching ground
        // If its ready for a jump, add a ammount for the jump and actualy start the jump after a delay
        if (Player.instance.jumpInput && !isJumping && isGrounded && !jumpStarted)
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

		if (!sounds [2].isPlaying && !isJumping && jumpEnd && isGrounded && rigBod.velocity.y==0 && !isJumping ) {
			sounds [2].Play();
			jumpEnd = false;
			inAir = false;
		}
        //Hard Landing check
        if (rigBod.velocity.y <= hardLandingThreshold) {
            Player.instance.animstate.SetBool("LandHard", true);

			playedOnce = false;
        }
        else if (Player.instance.animstate.GetCurrentAnimatorStateInfo(0).IsTag("Idle") || jumpStarted) { //will reset the HardLand bool if an Idle or new jump has been set
            Player.instance.animstate.SetBool("LandHard", false);
			playedOnce = false;

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

			Player.instance.animstate.SetFloat ("WalkState", Mathf.Abs (Player.instance.horizontalInput));
		if (!sounds[0].isPlaying && Player.instance.horizontalInput!=0 ) {
			sounds[0].Play ();
		}
		if (Player.instance.horizontalInput == 0||!isGrounded) {
			sounds[0].Stop ();
		}
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
			if (!sounds [1].isPlaying&&!playedOnce && !isGrounded) {
				playedOnce = true;
				sounds [1].Play();
			}
            rigBod.velocity = new Vector2(rigBod.velocity.x, jumpForce); // Add a bit of y velocity 
            isJumping = true;
            currentJumpAmmount -= decreaseAmmountBy;
			inAir = true;
        }
        else
        {
			if (!sounds [1].isPlaying&&!playedOnce && !isGrounded) {
				playedOnce = true;
				sounds [1].Play();
			}
            isJumping = false;
            currentJumpAmmount = 0;
        }
        //This if will cancel the jump if the character has hit his head on a object.
        if (isJumping && rigBod.velocity.y == 0)
        {
			if (!sounds [1].isPlaying&&!playedOnce && !isGrounded) {
				playedOnce = true;
				sounds [1].Play();
			}
            isJumping = false;
            currentJumpAmmount = 0;
        }
    }

    IEnumerator JumpDelay() {
        jumpStarted = true;

        yield return new WaitForSeconds(nextJumpDelay);
        // start jump
        RunJump(); 
		jumpEnd = true;
        jumpStarted = false;

    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

}
