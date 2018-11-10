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

public class Tans : MonoBehaviour {
	public Tangrams collectionTan;
	public Tangrams puzzTan;
	public bool flipped = false;
    bool placed; 
	float distance = 10;
	//5:Big tri, 4:med tri, 3:small tri, 2 Paral, 1: Square
	public int type;
	public Vector2 myTanPosition;
	public int direction;
	bool hasPlayed = false;
	//variables used for border detection. Not Implimented yet
	//	float b1X,b2X,b3Y,b4Y;
	//bool moveL, moveR, moveU, moveD;

   /* void turn()
    {
        if (direction < 7) direction++;
        else direction = 0;
    }*/
    // Use this for initialization
    void Start () {

		//set the directions to whatever angle it is
		direction = Mathf.RoundToInt(this.transform.eulerAngles.z / 45);

	}
	//used for flip button
	public void Flip(){
		//rotate 180 on y axis
		transform.Rotate (new Vector3 (0, 180, 0));
		//when its flipped the direction is off by 1 unit for some reason, so we add one.
		direction = (int)((this.transform.eulerAngles.z+1.0f) / 45);
		//toggle flip variable
		flipped = (flipped)?false:true;
	}

	// Update is called once per frame
	void Update () {
		//set the value of it's stored position every frame.  
		this.myTanPosition = new Vector2(this.transform.position.x,this.transform.position.y);
	}


	//Function for Border Collision NOT IMPLIMENTED YET
//	void OnCollisionEnter2D(Collision2D Coll){
//		float lastX;
//		float lastY;
//		if (Coll.gameObject.name == "Border1") {
//			moveL = false;
//			lastX = Input.mousePosition.x;
//			lastY = Input.mousePosition.y;
//			b1X = lastX + 10.0f;
//			transform.position = new Vector2 (b1X, lastY);
//
//		}
//		//||Coll.gameObject.name == "Boarder2")	
//		if (Coll.gameObject.name == "Boarder3"||Coll.gameObject.name == "Boarder4") {
//			//this.transform.position.y = this.transform.position.y - 0.3;
//		}
//	}

	//Check to see if tangram is finished if "this" and incoming peices have same positions
	public bool checkPos(Tans inTan){
		//set the x and y boolian positions positions because we have a tolerance for each x/y position
		bool xCheck = false, yCheck= false;
		//These cut off any extra decimal points
		float xInTan = (float)((Mathf.Round(inTan.transform.position.x * 100)) / 100);
		float yInTan = (float)((Mathf.Round(inTan.transform.position.y * 100)) / 100);

//		if (this.type == inTan.type) {
//			Debug.Log (this.name + "x:" + this.myTanPosition.x + "   " + inTan.name + "x:" + inTan.myTanPosition.x );
//			Debug.Log (this.name + "y:" + this.myTanPosition.y + "   " + inTan.name + "y:" + inTan.myTanPosition.y);
//		}
		//Check if x coordinate is in the tolerance range
		if (this.myTanPosition.x * 10 < (xInTan * 10 + 3) && this.myTanPosition.x * 10 > (xInTan * 10 - 3)) {
			//Check if y coordinate is in the tolerance range
			if (this.myTanPosition.y * 10 < (yInTan * 10 + 3) && this.myTanPosition.y * 10 > (yInTan * 10 - 3)) {
				//if they're the same type of Tan
				if (this.type == inTan.type) {
					//move the current tan to the position of the Puzzle Tan. 
					this.transform.position = new Vector3 (inTan.transform.position.x, inTan.transform.position.y, 1.0f);
					//set x & y to true
					xCheck = true;	
					yCheck = true;
				}
			}

		}
		//return x & y check 
		return (xCheck && yCheck);
	}

	//checks to see if the direction of the tans are the same
	public bool checkRotation(Tans inTan){
		switch (type) {
		case 2:
			//For Paralellogram
			//Check what direction its facing
			if (flipped == inTan.flipped) {
				//if the direction is the same or 180 degrees different than return true
				return (direction == inTan.direction || direction + 4 == inTan.direction || direction - 4 == inTan.direction);
			} else {
				return false;
			}
		case 1:
			//For square
//			Debug.Log ("square Direction " + (this.direction % 2).ToString () + " VS " + (inTan.direction % 2).ToString ());
			//Since a square rotated 90 degrees fits the same way it really only has 2 directions
			//we modulos to see if its one or the other and return true
			return (direction % 2 == inTan.direction % 2);
		default:
			return(direction == inTan.direction);
		}
	}



	// Mouse Functions On Tans
	//Function for dragging Tan
	void OnMouseDrag(){
		if (!hasPlayed) {
			collectionTan.GetComponent<AudioSource> ().Play ();
			hasPlayed = true;
		}
		placed = false;
		//vars to hold mouse input
		float inX = Input.mousePosition.x;
		float inY = Input.mousePosition.y;
//		if (!moveL) {
//			inX = b1X;
//		}
		//have a vector to hold both x&y coords of mouse
		Vector3 mousePosition = new Vector3 (inX, inY , distance);
		//pass those mouse coordinates based on the camera's position
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		//change the object to those points
		this.transform.position =new Vector3((Mathf.Round(objPosition.x*10)*0.1f),
			(float)(Mathf.Round(objPosition.y*10)*0.1f),
			Mathf.Round(objPosition.z));
	}
	//For letting go of Mouse after drag
	void OnMouseUp(){
		placed = true;
		collectionTan.GetComponent<AudioSource> ().Play ();
		hasPlayed = false;
//		moveL = true;
//		moveD = true; 
//		moveU = true;
//		moveR = true;
		//Calls snap to grid function
		//snapToGrid ();
		//Debug.Log ("ACTUAL TAN LOCATION: "+ this.transform.position.ToString());
		//checks to see if puzzle is solved. 
		collectionTan.checkSolve (puzzTan);
	}

	void OnMouseOver(){
		//If the mouse button is the right button
		if (Input.GetMouseButtonDown (1)) {
			//call the rotate function to rotate it 45 degrees
			this.rotateTan (45);
		}
	}

	//Rotation function
	public void rotateTan(float i){
		//We use this rotation function instead so we can change the direction variable of the object (also called in Tangram's Start function
		this.transform.Rotate (new Vector3 (0, 0, i));
		if (flipped) {
			direction = (int)((this.transform.eulerAngles.z+1.0f) / 45);
		} else {
			direction = (int)(this.transform.eulerAngles.z / 45);
		}
		//check to see if it is solved
		if (placed)
			collectionTan.checkSolve (puzzTan);
	}

	//adjusts the position so it snaps to a spot
	public void snapToGrid(){
		var currentPos = transform.position;
		//we take the the x,y variables and round them after being * by 4. Then we divide again by 4 that snaps them to a grid
		this.transform.position =new Vector3((Mathf.Round(currentPos.x*8)*0.125f),
			(float)(Mathf.Round(currentPos.y*8)*0.125f),
			Mathf.Round(currentPos.z));
		//apply the changed coordinates
		this.myTanPosition = new Vector2(this.transform.position.x,this.transform.position.y);
	}

}
