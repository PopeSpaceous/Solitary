using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/*Game Manager will always be static throughout the game and forced to not be unloaded when a new scene is loaded */
public class GameManager : MonoBehaviour {

	//make this instance static so it can be used across scripts
	public static GameManager instance = null;

    public int currentScore = 0;
    //This array will hold what doors should be locked or not for each level
    public bool[] doorLocks;
    //Bool to check if game is complete
    public bool isGameComplete = false;
    //used to trigger a load game sequence 
    public bool loadGameFile = false;
    //Id Data static class
    public DBData dBData;
    //Player data
    public int playerId = 0;
    public int highScore = 0;
    IdentifyData idData = null;
    //the bool for when the game has fully ended
    public bool gameEnded = false;

    void Awake () {
		
		//Set the instance only once.
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//Enforces that there will always be one instance of the GameManager gameObject, this is for type errors prevention.
			Destroy (gameObject);
			Debug.LogWarning ("Another instance of GameManager has been created and destoryed!");
		}
        //Get the DB data script ref
        dBData = GetComponent<DBData>();

        //Makes this gameobject not be unloaded when entering a new scene
        DontDestroyOnLoad (this);

        idData = new IdentifyData();

        //Load in idData id there is any
        LoadIdentify();
    }
    //Clean vars and starts a new game
    public void NewGame() {
        isGameComplete = false;
        currentScore = 0;
        loadGameFile = false;
        //Starting locks for each level doors
        doorLocks = new bool[5] { false, true, true, true, true };
    }

    //Set the states of the player
    //this method will only be called when loading
    public void LoadStats(int score, bool gameCompleted) {

        currentScore = score;
        gameCompleted = isGameComplete;
    }
    
    public void SaveGame() {
        SaveIdentify();
        Player.instance.playerProgress.SaveGame();
    }

    //set the player location based on given spawn point
    public void SetPlayerLocation(Transform sp){
		Player.instance.transform.position = sp.position;
	}

    public void UpdateScore(int addScore) {
        currentScore += addScore;                
    }
    //Check game completion
    public void CheckCompletion() {
        //When level 5 is completed the game is completed
        if (Player.instance.playerProgress.level5)
        {
            isGameComplete = true;
            Player.instance.playerProgress.isGameCompleted = isGameComplete;

            //Like level 1 and level 2 door when the game is completed
            doorLocks[0] = true;
            doorLocks[1] = true;
        }

    }
    
    public void LevelCompleted(int i, int addScore) {
        UpdateScore(addScore);
        
        //Update completed levels and door locks
        switch (i) {

            case 1:
                
                Player.instance.playerProgress.level1 = true;
                doorLocks[i] = false;
                break;
            case 2:
                Player.instance.playerProgress.level2 = true;
                doorLocks[i] = false;
                break;
            case 3:
                Player.instance.playerProgress.level3 = true;
                doorLocks[i] = false;
                break;
            case 4:
                Player.instance.playerProgress.level4 = true;
                doorLocks[i] = false;
                break;
            case 5:
                Player.instance.playerProgress.level5 = true;
                break;
        }

        //Update highscore
        if (currentScore >= highScore)
        {
            highScore = currentScore;
        }

        //Update player's score
        Player.instance.playerProgress.UpdatePlayerStats(currentScore);
        CheckCompletion();
        SaveGame();
        
    }
    //This method will clean up the static objects and take the player back to a givin scene
    //This will casue any unsaved progress to be lost
    public void ExitGame(string sceneToLeave) {
        //Get the fader
        GameObject fader = GameObject.Find("Fade");
        Animator a = fader.GetComponent<Animator>();
        Image i = fader.GetComponent<Image>();
        //Load back to main menu with fade
        NextSceneManager.instance.LoadLevelScene(sceneToLeave, a, i);
        //Destroy the player. Playerprogress will also be gone
        Destroy(Player.instance.gameObject);
    }

    //Load in the Identification Data (id, and high score)
    public void LoadIdentify() {
        if (SaveLoad.LoadIdentification())
        {
            playerId = IdentifyData.current.id;
            highScore = IdentifyData.current.highScore;
        }
    }
    //Save in the Identification Data (id, and high score)
    public void SaveIdentify() {
        IdentifyData.current = idData;

        IdentifyData.current.id = playerId;
        IdentifyData.current.highScore = highScore;

        SaveLoad.SaveIdentification();
    }

    public void UploadToDB(string username = null){
        dBData.UploadHighScores(username);
    }

    //Game End will wipe the save data of player progress.
    //But it will keep the id and highscore data
    public void GameEnd() {
        
        //Save id info / highscore
        SaveIdentify();

        gameEnded = true;
        //Dump save file
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            File.Delete(Application.persistentDataPath + "/savedGames.gd");

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}
