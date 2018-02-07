using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Hub : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;

	void Start(){
		//place player at spawn point
		GameManager.instance.SetPlayerLocation (PlayerSpawn);
	}
	//TODO: Complete Level_hub class
}
