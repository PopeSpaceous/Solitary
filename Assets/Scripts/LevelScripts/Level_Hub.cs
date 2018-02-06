using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Hub : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;

	private GameManager gm;

	void Start(){
		gm = GameManager.instance;
		//place player at spawn point
		gm.SetPlayerLocation (PlayerSpawn);
	}

}
