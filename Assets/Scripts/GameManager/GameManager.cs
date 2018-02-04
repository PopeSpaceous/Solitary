using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//Will place the reference of the player in the Inspector
	private Player player;
	//make a instance that will be static so it can be used across scripts
	public static GameManager instance = null;

	void Awake () {
		
		//Set the instance only once.
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//Enforces that there will always be one instance of a gameObject. This is for type errors prevention
			Destroy (gameObject);
		}

		//Makes the gameobject not be unloaded when entering a new scene
		DontDestroyOnLoad (this);

		player = Player.instance;
	}

	//set the player location based on given spawn point
	public void SetPlayerLocation(Transform sp){
		player.transform.position = sp.position;
	}

}
