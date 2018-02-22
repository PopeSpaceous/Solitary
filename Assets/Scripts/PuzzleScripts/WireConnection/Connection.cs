using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour {


	[HideInInspector] 
	public Lock realLock = null; //Assgined by InitPuzzle()

	public int connectedWireID = 0;
	public Wire connectedWire = null;

	void OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject.name.Equals ("Wire") && connectedWire == null ) {
			//get the wire ref
			connectedWire = col.GetComponent<Wire> ();
				//get id and assgin the connection
				connectedWireID = connectedWire.wireIDLink;
				connectedWire.connection = this;
				//Debug.Log ("Entered");
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.name.Equals ("Wire")) {
			//get the wire ref
			connectedWire = col.GetComponent<Wire> ();

			//if wire has not been already connected to another connecter then leave wire's connection
			if(connectedWire.connection == this){
				connectedWire.connection = null;

				//Debug.Log ("Exited");
			}
			connectedWire = null;
		}
	}

	//will be called in the Wire script when the MouseDown event has been called
	public void DisconnectWireAffect(){
		connectedWireID = 0;
		AffectLock ();
	}
	//will be called by the Wire script when the MouseUp event has been called
	public void SnapWire(){
		//allow the wire to follow the target of the connection and not the mouse
		connectedWire.FollowTarget (GetComponent<Transform>().position);
		//affect the lock
		AffectLock ();
	}

	public void AffectLock(){
		//Affect real lock
		realLock.CheckTheSum(connectedWireID);
	}
}
