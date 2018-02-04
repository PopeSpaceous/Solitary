using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player instance = null;
	void Awake () {
		
		//Set the instance only once.
		if (instance == null) {
			
			instance = this;
			Debug.Log ("added player instance");
		} else if (instance != this) {
			Debug.Log ("Removed player instance");
			//Enforces that there will always be one instance of a gameObject. This is for type errors prevention
			Destroy (gameObject);
		}
			
		//Makes the gameobject not be unloaded when entering a new scene
		DontDestroyOnLoad (this);
	}

}
