using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Game Manager will always be static throughout the game and forced to not be unloaded when a new scene is loaded */
public class GameManager : MonoBehaviour {

	//make this instance static so it can be used across scripts
	public static GameManager instance = null;

	void Awake () {
		
		//Set the instance only once.
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//Enforces that there will always be one instance of the GameManager gameObject, this is for type errors prevention.
			Destroy (gameObject);
			Debug.LogWarning ("Another instance of GameManager have been created and destoryed!");
		}

		//Makes this gameobject not be unloaded when entering a new scene
		DontDestroyOnLoad (this);
	}

	//set the player location based on given spawn point
	public void SetPlayerLocation(Transform sp){
		Player.instance.transform.position = sp.position;
	}


	//TODO: complete GameManager class

}
