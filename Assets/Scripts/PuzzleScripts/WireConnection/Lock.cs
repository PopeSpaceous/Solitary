using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Leonel Jara
public class Lock : MonoBehaviour {

	public static List<Lock> unlocked = new List<Lock>(); // this will hold the list of currently open locks

	public bool isOpen = false;

	public int lockID; // the lock Id will be used to calulate the sum
	public int neededSum; // the needed sum for the lock to move. It will move if the lock is less then
	public int currentSum = 0; // the current caluated sum
	public Text textCSum; // a ref to be used for updating the current sun on the text UI

	private int ConnWireID = 0; // the current connected wire id. This will be used to caluated the current sum 


	private Connection connection;

	//this update function will serve to update the UI text field
	public void Update(){
		if(textCSum != null){
			textCSum.text = currentSum.ToString ();
			if(currentSum == neededSum){ // if its right
				textCSum.color = Color.green;
			}
			else if(currentSum == 0){ // if its nothing
				textCSum.color = Color.black;
			}
			else if(currentSum != neededSum){ // if its wrong
				textCSum.color = Color.red;
			}
		}
	}

	//This function will check if its close or not based on the givin wireID. If so it will set/unset the appropriate vars
	//If the lock has been open it will casues an affect to already open locks and closes it and snap back the wires
	public void  CheckTheSum(int wireID, Connection cw = null){ // will affect and change its lock state of it infuceners 

		int sum = 0;
		currentSum = 0;
		ConnWireID = wireID;

		//calculate the sum
		sum += ConnWireID + lockID;

		//add it to current sum
		currentSum = sum;

		//check if the sum is correct and apply the needed changes
		if (sum == neededSum && !isOpen && wireID != 0) {
			//Debug.Log ("OPEN!");
			isOpen = true;

			MoveLock (true);

			//set vars----
			connection = cw;

			//Add it to the list of current unlocked locks
			unlocked.Add (this);

		} else if (isOpen ) { // if the lock is already open close it
			//Debug.Log ("Closed!");

			isOpen = false;
			MoveLock (false);


			//snap back the wire. if it was caused by an affect
			if(connection.connectedWire != null){
				connection.FullySnapDisconnectWire (); 
			}
			//clean vars----
			connection = null;
			currentSum = 0;

			//remove it to the current unlocked list
			unlocked.Remove (this);
			
		} else if (!isOpen ) { //if everything is wrong affect the already unlocked
			//Debug.Log ("Unlock Affect");
			//Unlock Affect
			unlockAffect();
		}

		if(wireID == 0){
			currentSum = 0;
		}

		//Debug.Log ("Current Sum: " + currentSum + " Needed Sum: "+neededSum + " WIreID: " + ConnWireID + " LockID: " + lockID + " IsOpen: " + isOpen);
	}

	//this function will cause to close already open locks
	public void unlockAffect(){
		for (int i = unlocked.Count - 1; i >= 0; i--){
			//Debug.Log ("Affected :"+unlocked[i].lockID);
			unlocked[i].CheckTheSum (unlocked[i].ConnWireID);

		}
	}
	//Move the lock to show it has been open or closed
	private void MoveLock (bool state){
		
		Vector3 ChangePos = this.GetComponent<Transform> ().position;
		if (state) {
			//Change to an open state
			ChangePos.y += 2;
		} else {
			//Change to a close state
			ChangePos.y -= 2; 
		}
		GetComponent<Transform> ().position = ChangePos;
	} 
		
}
