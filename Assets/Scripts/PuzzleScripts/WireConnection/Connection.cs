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

	void OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject.name.Equals ("Wire") && connectedWire == null ) {
			//get the wire ref
			connectedWire = col.GetComponent<Wire> ();
			//get id and assgin the connection
			connectedWireID = connectedWire.wireIDLink;
			connectedWire.connection = this;

		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.name.Equals ("Wire")) {
			//get the wire ref
			connectedWire = col.GetComponent<Wire> ();

			//if wire has not been already connected to another connecter then leave wire's connection
			if(connectedWire.connection == this){
				connectedWire.connection = null;
			}
			connectedWire = null;
		}
	}

	public void DisconnectWireAffect(){
		connectedWireID = 0;
		AffectLock ();
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
