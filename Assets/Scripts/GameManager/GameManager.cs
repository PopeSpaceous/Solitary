using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Game Manager will always be static throughout the game and forced to not be unloaded when a new scene is loaded */
public class GameManager : MonoBehaviour {

	//make this instance static so it can be used across scripts
	public static GameManager instance = null;

    public int currentScore = 0;
    private Level_Hub hub; // this hub ref is used for the level doors. Set each element of the leveldoors array in LoadGame 

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
        //get level_hub ref
        hub = GameObject.Find("Hub").GetComponent<Level_Hub>();
        
    }

    //NOTE: this method must be called in a start method when loading the game from a file
    public void LoadGame(/* TODO: add params */) {

        //TODO: complete load game for GM
        //GM needs to set : currentScore and levelDoors when loading the game
        //You must set the values of leveldoors in the hub:  this.levelDoors = hub.levelDoors;
    }
    
    public void SaveGame() {
        //TODO: complete save game
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
        //Unlock another door
        if (i < hub.levelDoors.Length) {
            hub.levelDoors[i + 1].isDoorlocked = false;
        }

        SaveGame();
    }
	//TODO: complete GameManager class. Still need Upload to DB

}
