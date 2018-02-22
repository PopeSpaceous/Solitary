﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour {



	//[HideInInspector] 
	public int wireIDLink = 0; // will be assgined by InitPuzzle()
	[HideInInspector]
	public Connection connection = null;
	public Transform wireTrans; //public GameObject connection;
	public float scaleLimit = 0.95f; // solution for now. Ideally have another collider at the tip, that does not scale!

	private Vector3 orignalScale;
	private Vector3 scaleChange;
	private bool hasChanged = false;

	// Use this for initialization
	void Start () {
		scaleChange = wireTrans.localScale;
		orignalScale = new Vector3 (wireTrans.localScale.x, wireTrans.localScale.y, wireTrans.localScale.z);
	}


	void OnMouseDrag(){
		//the wire has changed
		hasChanged = true;
		//get mouse position that reps in the game world
		Vector3 mousePoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//Follow the mouse
		FollowTarget (mousePoint);
	}

	void OnMouseUp(){
		//if the wire is not connected it will snap back to its orignal scale and rotation
		if( connection == null && hasChanged ){
			hasChanged = false;
			wireTrans.localScale = new Vector3 (orignalScale.x ,orignalScale.y, orignalScale.z);
			wireTrans.rotation = Quaternion.Euler (new Vector3(0 , 0, 0));
		}
		else {
			//if the wire is connected and has not been disconnected; snap to the connection block.
			connection.SnapWire ();
		}
	}
	//When the player mouse down on the wire that is already connected this will causes it to disconnect	
	void OnMouseDown(){
		if(connection != null){
			connection.DisconnectWireAffect ();
		}
	}

	//Given the position, the wire will scale on the x axis and rotate to that position
	public void FollowTarget(Vector3 targetPos){
		
		//rotation to follow 
		Vector3 toTargetVector = targetPos - wireTrans.position;
		float zRotation = Mathf.Atan2( toTargetVector.y, toTargetVector.x ) * Mathf.Rad2Deg;
		wireTrans.rotation = Quaternion.Euler(new Vector3 ( 0, 0, zRotation));

		//Scale to follow
		float distance = Vector2.Distance(wireTrans.position,  targetPos); // calculates the magnitude between the mouse and this gameobject
		scaleChange.x = Mathf.Abs(distance) / 11.5f ; // offset the value by 11.5f. This is so it can scale with the distance.

		if(scaleChange.x <= scaleLimit){ //Temp solution for snaping 
			//Apply scale
			wireTrans.localScale = scaleChange;
		}
	}
		
}
