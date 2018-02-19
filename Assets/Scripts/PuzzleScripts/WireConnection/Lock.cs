using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

	public Lock[] lockInfluence; 
	//[HideInInspector]
	public bool isOpen = false;
	public int lockIDLink;
	public int neededSum; // the needed sum for the lock to move
	public int currentSum; // the current caluated sum

	public bool isMasker = false; // must not be changed!
	private bool maskState = true;


	public void  CheckTheSum(int wireID){ // will affect and change its lock state of it infuceners 
		int sum = 0;
		currentSum = 0;
		//see if its influcene has the need currentSum for this lock
		if(lockInfluence != null){
			foreach (var lockIn in lockInfluence) {
				sum += lockIn.currentSum;
			}
		}
		sum += wireID + lockIDLink;
		//add it to current sum
		currentSum = sum;
		Debug.Log ("Current Sum: " + currentSum + " Needed Sum: "+neededSum + " WIreID: " + wireID + " LockID: " + lockIDLink + " isMakser: " + isMasker);
		if (currentSum == neededSum && isOpen == false) { // maybe check this some where else
			isOpen = true;
			MaskChangeState (false);
			if(!maskState || !isMasker){
				MoveLock (true);
			}

			Debug.Log ("OPEN!");
		} else if( currentSum != neededSum  && isOpen == true) {
			isOpen = false;
			MaskChangeState (true);
			if(!maskState || !isMasker){
				MoveLock (false);
			}
			
		}



	}

	void MaskChangeState(bool change){
		//check mask
		if(lockInfluence != null){
			foreach (var lockIn in lockInfluence) {
				if (lockIn.isMasker && change != lockIn.maskState) {
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


		Debug.Log ("ID: " + lockIDLink + " changed open to: " + isOpen);
	} 
		
}
