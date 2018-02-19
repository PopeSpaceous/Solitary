using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WireConnection: Puzzle {

	public Transform[] wirePlaceholders;
	public Transform[] connectionPlaceholders;
	public Transform[] lockPlaceholders;
	public Transform puzzleCan;

	public GameObject connectionPrefab;
	public GameObject lockPrefab;
	public GameObject wirePrefab;

	//TODO: maybe make a collection
	List<Wire> wires; //Wires are linear
	List<Lock> realLocks; 
	List<Connection> inputConnections; //connections are linear

	//The number of wires, connections, and locks there will be;
	private int diffLength;

	// Sets the parent fields
	void Awake () {
		puzzleName = "WireConnection";
		#if No_Debug_Puzzle
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;

		Debug.Log ("Difficulty for puzzle " + puzzleName + " is: "+ this.difficulty);
		#endif
		//Debuging---
		difficulty = 3;
		//
		diffLength = difficulty + 2;

		//init Lists
		wires = new List<Wire>();
		inputConnections = new List<Connection>();
		realLocks = new List<Lock>();

		InitPuzzle ();
	}


	void InitPuzzle(){


		//SetupWires------------------------------------------------------------
		for(int ctr = 0; ctr < diffLength; ctr++){
			//Create Object and place it in the given placeholder
			GameObject obj = Instantiate (wirePrefab,  puzzleCan) as GameObject;
			//Set the position
			obj.transform.position = wirePlaceholders [ctr].position;
			//Get the Wire Script
			Wire w = obj.GetComponentInChildren<Wire>();
			//Assgin the var id in the wire script
			w.wireIDLink = ctr + 1;
			//Add it to our list collection
			wires.Add(w);

		}
		//Setup Locks--------------------------------------------------------
		for (int ctr = 0; ctr < diffLength; ctr++) {
			GameObject obj = Instantiate (lockPrefab,  puzzleCan) as GameObject;
			//Set the position
			obj.transform.position = lockPlaceholders [ctr].position;
			//Get scripts attached to the gameobject
			Lock r = obj.GetComponent<Lock> ();
			//Set the ids
			r.lockIDLink = ctr + 1;
			//Add it to our list collection
			realLocks.Add (r);
		}
		//Shuffle the locks
		Shuffle<Lock> (realLocks);
		Shuffle<Wire> (wires);
		//Maybe shuffle wires
		//---

		//TODO MAJOR WIP
		//Setup Lock influences for locks-------------------------------------------------

		//setup opening lock
		realLocks [0].neededSum = wires [0].wireIDLink + realLocks [0].lockIDLink;
		realLocks [0].lockParents = null;
		realLocks [0].lockChilds = new List<Lock>();
		//setup other locks
		for (int ctr = 1; ctr < diffLength ; ctr++) {
			//calulate needed sum
			realLocks [ctr].neededSum = wires [ctr].wireIDLink +
			realLocks [ctr].lockIDLink +
			realLocks [ctr - 1].neededSum;
			//set lock parent
			realLocks [ctr].lockParents = new List<Lock>() {realLocks [ctr - 1]};
			//set lock child
			realLocks[ctr - 1].lockChilds.Add(realLocks[ctr]);
			//instat current lock child list
			realLocks [ctr].lockChilds = new List<Lock> ();

		}
		//set mask
		//realLocks [UnityEngine.Random.Range(2, diffLength - 2)].isMasker = true;
		//realLocks [1].isMasker = true;
		//suffle locks again
		Shuffle<Lock> (realLocks);
		//------------------------------------
		//Setup Connections
		for(int ctr = 0; ctr < diffLength; ctr++){
			//Create Object and place it in the given placeholder
			GameObject obj = Instantiate (connectionPrefab,  puzzleCan) as GameObject;
			//Set the position
			obj.transform.position = connectionPlaceholders [ctr].position;
			//Get the Connection Script
			Connection c = obj.GetComponentInChildren<Connection>();
			//set values
			c.realLock = realLocks [ctr];
			//add it to collection
			inputConnections.Add (c);
		}

	}
		
	public void Shuffle<T>(List<T> array) {
		var count = array.Count;
		var last = count - 1;
		for (var i = 0; i < last; ++i) {
			var r = UnityEngine.Random.Range(i, count);
			var tmp = array[i];
			array[i] = array[r];
			array[r] = tmp;
		}
	}



}























