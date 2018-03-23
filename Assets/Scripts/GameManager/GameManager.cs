using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Game Manager will always be static throughout the game and forced to not be unloaded when a new scene is loaded */
public class GameManager : MonoBehaviour {

	//make this instance static so it can be used across scripts
	public static GameManager instance = null;

    public int currentScore = 0;

	void Awake () {
		
		//Set the instance only once.
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//Enforces that there will always be one instance of the GameManager gameObject, this is for type errors prevention.
			Destroy (gameObject);
			Debug.LogWarning ("Another instance of GameManager has been created and destoryed!");
		}

		//Makes this gameobject not be unloaded when entering a new scene
		DontDestroyOnLoad (this);
	}

	//set the player location based on given spawn point
	public void SetPlayerLocation(Transform sp){
		Player.instance.transform.position = sp.position;
	}

    public void UpdateScore(int addScore) {
        currentScore += addScore;
        //TODO: update score in player progress
    }

    
    public void LevelCompleted(int i)
    {
        //TODO: code to to unlock another door
    }
	//TODO: complete GameManager class. Still need Upload to DB

}
