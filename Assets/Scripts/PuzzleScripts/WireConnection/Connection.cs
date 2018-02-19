using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour {

	//Assgined by randomizer
	[HideInInspector] 
	public Lock realLock = null; // setup by randomzier

	//privates
	public int connectedWireID = 0;
	public Wire connectedWire = null;

	void OnTriggerStay2D(Collider2D col){
		
		if (col.gameObject.name.Equals ("Wire") && connectedWire == null ) {
			//get the wire ref
			connectedWire = col.GetComponent<Wire> ();
			//get id and assgin the connection
			connectedWireID = connectedWire.wireIDLink;
			connectedWire.connection = this;


			//Debug.Log ("Trigged: " + connectedWire.wireIDLink);
		}


	}
	void OnTriggerExit2D(Collider2D col){

		if (col.gameObject.name.Equals ("Wire")) {
			Wire colWire = col.GetComponent<Wire> ();
			if(colWire.wireIDLink == connectedWireID){

				//Debug.Log (" Exit, its a wire: " + connectedWireID);
				connectedWireID = 0;
				connectedWire.connection = null;
				connectedWire = null;

				AffectLock ();
			}

		}

	}

	public void SnapWire(){
		
		connectedWire.FollowTarget (GetComponent<Transform>().position);
		AffectLock ();
	}

	public void AffectLock(){

		//Affect real lock
		realLock.CheckTheSum(connectedWireID);
		//Debug.Log ("Affected LocK in connection");


	}
}
