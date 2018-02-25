using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WireConnection: Puzzle {

	public Transform[] wirePlaceholders;
	public Transform[] connectionPlaceholders;
	public Transform[] lockPlaceholders;

	//The puzzle canvus transfrom ref so we can still make the game object as child of Puzzle Canvus gameobject
	public Transform puzzleCan;

	//Prefabs
	public GameObject connectionPrefab;
	public GameObject lockPrefab;
	public GameObject wirePrefab;


	//These are the list of refs for all locks, connections, and wires.
	List<Wire> wires; 
	List<Lock> realLocks; 
	List<Connection> inputConnections; 

	//The number of wires, connections, and locks there will be;
	private int diffLength;

	// Sets the parent fields
	void Awake () {
		puzzleName = "WireConnection";
		#if NO_DEBUG
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;

		//Debug.Log ("Difficulty for puzzle " + puzzleName + " is: "+ this.difficulty);
		#endif

		//Debuging---
		difficulty = 2;
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
			w.wireIDLink = ctr + diffLength + 1;
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
			r.lockID = ctr + 1;
			//Add it to our list collection
			realLocks.Add (r);
		}

		//Setup Connections------------------------------------------------------------
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

		//Shuffle the locks-------------------
		Shuffle<Lock> (realLocks);
		//Shuffle the wires-------------------
		Shuffle<Wire> (wires);

		//Setup Lock influences for locks-------------------------------------------------

		//setup opening lock
		realLocks [0].neededSum = wires [0].wireIDLink + realLocks [0].lockID;
		realLocks [0].lockParents = null;
		realLocks [0].lockChilds = new List<Lock>();
		//setup other locks
		for (int ctr = 1; ctr < diffLength ; ctr++) {
			
			//calulate needed sum
			realLocks [ctr].neededSum = wires [ctr].wireIDLink +
			realLocks [ctr].lockID +
			realLocks [ctr - 1].neededSum;
			//set lock parent and add its child to the current one
			realLocks [ctr].lockParents = new List<Lock>();
			for(int count = ctr - 1; count >= 0; count--){
				realLocks [ctr].lockParents.Add (realLocks [count]);
				realLocks [count].lockChilds.Add (realLocks [ctr]);
			}
			//Make it equal
			if(ctr % 2 == 1){
				realLocks [ctr].mustEqual = true;
			}
			//setup lock child
			realLocks [ctr].lockChilds = new List<Lock> ();
		}
	}

	//This update function will check if all locks are open.If they are it will call PuzzleComplete()
	//Leo Note: maybe do calls when the a lock has been open
	void Update(){

		bool hasAllOpen = true;
		foreach (var aLock in realLocks) {
			if(!aLock.isOpen){
				hasAllOpen = false;
			}
		}

		if(hasAllOpen == true){
			Debug.Log ("YOU WIN!");
			//PuzzleComplete ();
		}
	}

	private void Shuffle<T>(List<T> array) {
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
