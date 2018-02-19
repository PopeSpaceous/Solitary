using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

	public List<Lock> lockParents; 
	public List<Lock> lockChilds;
	//[HideInInspector]
	public bool isOpen = false;
	public int lockIDLink;
	public int neededSum; // the needed sum for the lock to move
	public int currentSum; // the current caluated sum
	private int ConnWireID = 0;

	public bool isMasker = false; // must not be changed!
	public bool maskState = true;


	public void  CheckTheSum(int wireID){ // will affect and change its lock state of it infuceners 
		int sum = 0;

		currentSum = 0;
		ConnWireID = wireID;

		//see if its influcene has the need currentSum for this lock
		if(lockParents != null){
			foreach (var lockIn in lockParents) {
				sum += lockIn.currentSum;
				if(lockIn.currentSum > lockIn.neededSum){
					sum -= lockIn.ConnWireID;
				}
			}
		}
		sum += ConnWireID + lockIDLink;
		//add it to current sum
		currentSum = sum;
		Debug.Log ("Current Sum: " + currentSum + " Needed Sum: "+neededSum + " WIreID: " + ConnWireID + " LockID: " + lockIDLink + " isMakser: " + isMasker);

		//check if the sum is correct and apply the needed changes
		if (currentSum >= neededSum && !isOpen) { // maybe check this some where else
			Debug.Log ("OPEN!");
			isOpen = true;

			ChangeParentMaskState (false);

			//recheck the child
			RecheckChilds ();

			if(!isMasker){
				MoveLock (true);
			}
				
		} else if( currentSum < neededSum  && isOpen ) {
			isOpen = false;

			ChangeParentMaskState (true);

			//recheck the child 
			RecheckChilds();

			if (!isMasker) {
				MoveLock (false);
			}
			
		}
	}

	void RecheckChilds(){
		foreach (var child in lockChilds) {
			child.CheckTheSum (child.ConnWireID);
		}
	}

	void ChangeParentMaskState(bool change){
		//check mask
		if(lockParents != null){
			foreach (var lockIn in lockParents) {
				if (lockIn.isMasker) {
					lockIn.maskState = change;
					lockIn.MoveLock (!change);
				}
			}
		}
	}

	private void MoveLock (bool state){// open or close the lock
		
		Vector3 ChangePos = GetComponent<Transform> ().position;
		if (state) {
			//Change to an open state
			ChangePos.y += 5; // TODO: check if this works for changing in reference
		} else {
			//CHange to a close state
			ChangePos.y -= 5; // TODO: check if this works for changing in reference
		}


		GetComponent<Transform> ().position = ChangePos;


		//Debug.Log ("ID: " + lockIDLink + " changed open to: " + isOpen);
	} 
		
}
