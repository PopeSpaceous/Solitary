using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

	public List<Lock> lockParents; //TODO: don't make this a list anymore just a single var
	public List<Lock> lockChilds; //TODO: don't make this a list anymore just a single var

	public bool isOpen = false;

	public int lockIDLink;
	public int neededSum; // the needed sum for the lock to move. It will move if the lock is less then
	public int currentSum; // the current caluated sum
	private int ConnWireID = 0;

	public bool isLast = false; // this bool is used to force an equal sum on the chosen last lock

	public bool isMasker = false; // isMasker will make it so that when a lock state is changed it will not shown as moved in the puzzle unless the child of that lock is changed to true
	public bool maskState = true; // whats the current mask state if isMasker is true. If not, this does not do anything


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

		//Debug.Log ("Current Sum: " + currentSum + " Needed Sum: "+neededSum + " WIreID: " + ConnWireID + " LockID: " + lockIDLink + " isMakser: " + isMasker);

		//check if the sum is correct and apply the needed changes
		if (currentSum <= neededSum && !isOpen && (currentSum == neededSum || isLast  ==  false ) && ConnWireID != 0) {
			//Debug.Log ("OPEN!");
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

	//This function will recall the CheckTheSum when the current lock has changed their state of open or closed
	void RecheckChilds(){
		foreach (var child in lockChilds) {
			child.CheckTheSum (child.ConnWireID);
		}
	}

	//Will change the mask state of the parent. Thats if thier isMasker bool is set to true by InitPuzzle()
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
