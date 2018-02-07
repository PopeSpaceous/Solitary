using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;


	// Use this for initialization
	void Start () {
		//set the player spawn in the level
		GameManager.instance.SetPlayerLocation(PlayerSpawn);
	}

	//TODO: Complete level class

}
