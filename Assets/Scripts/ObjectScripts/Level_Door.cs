using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Door : MonoBehaviour {

	//TODO: Complete door unlocking when a puzzle is finished
	public bool isDoorlocked = false;

	//private PlayerInteraction playerInter;

	// Use this for initialization
	void Start () {
		//playerInter = Player.instance.GetComponent<PlayerInteraction> ();
	}

	/*Level door trigger and lock and unlock code here */
	
	//the passed bool will either lock or unlock the door
	public void DoorLockChange(bool key){
		isDoorlocked = key;
	}
}
