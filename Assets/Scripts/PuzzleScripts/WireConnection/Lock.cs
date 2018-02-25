using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

	public List<Lock> lockParents;
	public List<Lock> lockChilds;

	public bool isOpen = false;

	public int lockID;
	public int neededSum; // the needed sum for the lock to move. It will move if the lock is less then
	public int currentSum; // the current caluated sum
	private int ConnWireID = 0;

	public bool mustEqual = false;

	private static bool isBeingChecked = false; // makes sure that only one check can be done in parent / child at a time

	public void  CheckTheSum(int wireID, bool isBeCheck = false){ // will affect and change its lock state of it infuceners 

		int sum = 0;
		currentSum = 0;
		ConnWireID = wireID;
		bool previousState = isOpen;

		//see if its influcene has the needed currentSum for this lock
		if(lockParents != null){
			sum += lockParents[0].currentSum;
		}
		sum += ConnWireID + lockID;

		//add it to current sum
		currentSum = sum;

		//check if the sum is correct and apply the needed changes
		//if (currentSum <= neededSum && !isOpen && (currentSum == neededSum || mustEqual == false ) && ConnWireID != 0) {
		if (currentSum == neededSum && !isOpen) {
			Debug.Log ("OPEN!");
			isOpen = true;
			MoveLock (true);
				
		} else if (isOpen /*&& currentSum != neededSum */ ) {
			Debug.Log ("Closed!");
			isOpen = false;
			MoveLock (false);
			
		} else if (!isOpen && !isBeingChecked) { 
			Debug.Log ("Parent Affect");
			isBeingChecked = true;
			//check parent
			RecheckParent (); /**/
			isBeingChecked = false;
			Debug.Log ("Parent Affect END");
		}

		//check the childs if the state had been changed and its not being checked by a parent/child already
		if(previousState != isOpen && !isBeingChecked){
			isBeingChecked = true;
			Debug.Log ("Child Affect");
			//recheck the childs 
			RecheckChilds ();
			isBeingChecked = false;
			Debug.Log ("Child Affect END");
		}

		Debug.Log ("Current Sum: " + currentSum + " Needed Sum: "+neededSum + " WIreID: " + ConnWireID + " LockID: " + lockID + " IsOpen: " + isOpen + "Must Equal: " + mustEqual);
	}

	//This function will recall the CheckTheSum when the current lock has changed their state of open or closed
	void RecheckChilds(){
		foreach (var child in lockChilds) {
			child.CheckTheSum (child.ConnWireID);
		}
	}
	void RecheckParent(){
		if(lockParents != null){
			foreach (var lockIn in lockParents) {
					lockIn.CheckTheSum (lockIn.ConnWireID, true);
			}
		}
	}

	//Move the lock to show it has been open or closed
	private void MoveLock (bool state){
		
		Vector3 ChangePos = GetComponent<Transform> ().position;
		if (state) {
			//Change to an open state
			ChangePos.y += 5;
		} else {
			//Change to a close state
			ChangePos.y -= 5; 
		}
		GetComponent<Transform> ().position = ChangePos;
	} 
		
}
