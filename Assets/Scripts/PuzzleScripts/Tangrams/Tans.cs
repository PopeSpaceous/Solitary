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
	float b1X,b2X,b3Y,b4Y;
	bool moveL, moveR, moveU, moveD;
	public Vector2 myTanPosition;
	int direction;

   /* void turn()
    {
        if (direction < 7) direction++;
        else direction = 0;
    }*/
    // Use this for initialization
    void Start () {
		Physics2D.IgnoreCollision(Player.instance.GetComponent<Collider2D>(), GetComponent<PolygonCollider2D>());
		direction = (int)(this.transform.eulerAngles.z / 45);
		moveL = true;
		moveD = true; 
		moveU = true;
		moveR = true;
	}
	//used for flip button
	public void Flip(){
		transform.Rotate (new Vector3 (0, 180, 0));
		direction = (int)((this.transform.eulerAngles.z+1.0f) / 45);
		flipped = (flipped)?false:true;
	}

	// Update is called once per frame
	void Update () {
		this.myTanPosition = new Vector2(this.transform.position.x,this.transform.position.y);
	}

	void OnCollisionEnter2D(Collision2D Coll){
		float lastX;
		float lastY;
		if (Coll.gameObject.name == "Border1") {
			moveL = false;
			lastX = Input.mousePosition.x;
			lastY = Input.mousePosition.y;
			b1X = lastX + 10.0f;
			transform.position = new Vector2 (b1X, lastY);

		}
		//||Coll.gameObject.name == "Boarder2")	
		if (Coll.gameObject.name == "Boarder3"||Coll.gameObject.name == "Boarder4") {
			//this.transform.position.y = this.transform.position.y - 0.3;
		}
	}

	//Check to see if tangram is finished and this and incoming peices have same positions
	public bool checkPos(Tans inTan){
		bool xCheck = false, yCheck= false;
		if (this.myTanPosition.x * 10 < (inTan.myTanPosition.x * 10 + 1) && this.myTanPosition.x * 10 > (inTan.myTanPosition.x * 10 - 1)) {
			if (this.type == 1)
				Debug.Log ("X pos is same");
			xCheck = true;	
		}
		if (this.myTanPosition.y * 10 < (inTan.myTanPosition.y * 10 + 1) && this.myTanPosition.y * 10 > (inTan.myTanPosition.y * 10 - 1)) {
			if (this.type == 1)
				Debug.Log ("Y pos is same");
			yCheck = true;	
		}
		return xCheck && yCheck;
	}

	//checks to see if the direction of the tans are the same
	public bool checkRotation(Tans inTan){
		switch (type) {
		case 2:
			if (flipped == inTan.flipped) {
				return (direction == inTan.direction || direction + 4 == inTan.direction || direction - 4 == inTan.direction);
				break;
			} else {
				return false;
				break;
			}
		case 1:
			Debug.Log ("square Direction " + (this.direction % 2).ToString () + " VS " + (inTan.direction % 2).ToString ());
			return (direction % 2 == inTan.direction % 2);
			break;
		default:
			return(direction == inTan.direction);
		}
	}



	// Mouse Functions On Tans
	void OnMouseDrag(){
		placed = false;
		float inX = Input.mousePosition.x;
		float inY = Input.mousePosition.y;
//		if (!moveL) {
//			inX = b1X;
//		}
		//do same for all other dirs
		Vector3 mousePosition = new Vector3 (inX, inY , distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		this.transform.position = objPosition;
//		if (Input.mousePosition.x > inX) {
//			Debug.Log (Input.mousePosition.x+"  :   "+inX);
//			moveL = true;
//		}
	}
	void OnMouseUp(){
		placed = true;
//		moveL = true;
//		moveD = true; 
//		moveU = true;
//		moveR = true;
		snapToGrid ();
		//Debug.Log ("ACTUAL TAN LOCATION: "+ this.transform.position.ToString());
		collectionTan.checkSolve (puzzTan);

	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (1)) {
			transform.Rotate (new Vector3 (0, 0, 45));
			if (flipped) {
				direction = (int)((this.transform.eulerAngles.z+1.0f) / 45);
			} else {
				direction = (int)(this.transform.eulerAngles.z / 45);
			}
			if (placed)
				collectionTan.checkSolve (puzzTan);
		}
	}


	//adjusts the position so it snaps to a spot
	public void snapToGrid(){
		var currentPos = transform.position;
		this.transform.position =new Vector3((Mathf.Round(currentPos.x*4)*0.25f),
			(float)(Mathf.Round(currentPos.y*4)*0.25f),
			Mathf.Round(currentPos.z));
		this.myTanPosition = new Vector2(this.transform.position.x,this.transform.position.y);
	}

}
