using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;

	private GameManager gm;

	// Use this for initialization
	void Start () {

		//Get the reference of gameManager
		gm = GameManager.instance;

		//set the player spawn in the level
		gm.SetPlayerLocation(PlayerSpawn);
	}

	//TODO: complete level class

}
