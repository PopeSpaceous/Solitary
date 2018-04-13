// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * A child of WorldObject for the lift sprite that can be interacted with another object that support WorldObjects
 * The lift will move up and down in a loop when activated
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : WorldObject {

    //Begin moving the lift when true
    public bool activate = false;
    //Amount of distance to move
    public float liftDistance = 5f;
    //Speed of the lift
    public float liftSpeed = 3f;
    //if false, the direction will be down
    public bool DirectionUp = true; 
    // wait time before it should start moving again after it reaches its distance
    public float waitTime = 1f; 
    // checks if the player is on the platfrom or not
    [HideInInspector]
    public bool isPlayerIn = false;

    private Animator liftSate;   
    //current distance the player is in
    private float currentDistance = 0;
    private Rigidbody2D rigdL;
	public AudioSource mySound;
    private void Awake()
    {
        rigdL = GetComponent<Rigidbody2D>();
        liftSate = GetComponent<Animator>();
		mySound = GetComponent<AudioSource> ();
    }

    // Use this for initialization
    void Start () {
        objectName = "Lift";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only activate the platfrom once
        if (collision.CompareTag("Player") && !isLocked) {
            //Once activated this will not be called again
            if (!activate)
            {
                activate = true;
                isOpen = true;
            }
            isPlayerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.CompareTag ("Player") && isPlayerIn) {
			isPlayerIn = false;
		} 
    }
    // FixedUpdate will handle the lift's movement
    void FixedUpdate () {

        if (isOpen && activate)
        {            
            //Keep applying distance
            if (currentDistance < liftDistance)
            {                
                currentDistance+= 0.1f;
                //Sound to play
				if (!mySound.isPlaying && isPlayerIn) {
					mySound.Play ();
				} else if (!isPlayerIn) {
					mySound.Stop ();
				}

                OpenMove();
            }
			else 
            {                
                isOpen = false;
                StartCoroutine(NextMove());
            }
        }
        else if(rigdL.velocity.y != 0) {
			if (isPlayerIn) { // cut the velocity of the player when the lift stops
				Player.instance.GetComponent<Rigidbody2D> ().velocity = new Vector3 (Player.instance.GetComponent<Rigidbody2D> ().velocity.x, 0, 0);
			} 
            rigdL.velocity = new Vector3(0, 0, 0);
        }

        liftSate.SetBool("IsOpen", isOpen && activate);
        liftSate.SetBool("DirectionUp", DirectionUp);
    }

    public override void Lock()
    {
        isLocked = true;
    }
    /* Note: if you unlocking a lift and want to set activate to true */
    public override void Unlock()
    {
        isLocked = false;
        isOpen = true;
    }

    public override void OpenMove()
    {
        //Depending the Direction apply the needed velocity
        if (DirectionUp) {
			
            rigdL.velocity = new Vector3(0, liftSpeed, 0);
        }
        else        
        {
            if (isPlayerIn && rigdL.velocity.y != liftSpeed * -1) // set the velocity of the player when the lift start going down
            {
                Player.instance.GetComponent<Rigidbody2D>().velocity = new Vector3(Player.instance.GetComponent<Rigidbody2D>().velocity.x, liftSpeed  * -1, 0);
            }

            rigdL.velocity = new Vector3(0, liftSpeed * -1, 0);
        }
    }

    public override void CloseMove()
    {
        //flip direction
        currentDistance = 0;        
        DirectionUp = !DirectionUp;        
        isOpen = true;
    }

    //Wait to move again
    IEnumerator NextMove() {
        yield return new WaitForSeconds(waitTime);
        CloseMove();
    }

}
