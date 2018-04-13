// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Authors: Nathan Misener, Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * This class will keep track of player progress data. 
 * It also will trigger a save or load by the SaveLoad Class
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour {

	public GameData myGame;


    //Game completion bool
    public bool isGameCompleted = false;
	public bool goToPhobos = false;
    public  int score;

	public bool[] doorLocks = null;

    //bools to store which level has been completed
    public bool level1 = false;
    public bool level2 = false;
    public bool level3 = false;
    public bool level4 = false;
    public bool level5 = false;

    //Level scores
    public int[] Level_Scores;

    //Show instructions bools
    public bool i_PuzzleTemplate_100 = true;
    public bool i_WordPasscode_101 = true;
    public bool i_SimonSays_102 = true;
    public bool i_Tangrams_103 = true;
    public bool i_Cryptogram_104 = true;
    public bool i_WireConnection_105 = true;
    public bool i_anagrams_106 = true;
    public bool i_ImageScramble_107 = true;


    private void Awake()
    {
        Level_Scores = new int[5];

        for (int ctr =0; ctr < Level_Scores.Length; ctr++) {
            Level_Scores[0] = 0;
        }
        //Door lock ref from GM
        doorLocks = GameManager.instance.doorLocks;
        //Check if the game needs to load. Set by the main menu
        if (GameManager.instance.loadGameFile) {
            LoadGame();
        }
    }
    public void UpdatePlayerStats(int s) {
        score = s;      
    }

    public void SaveGame() {
		//set game data to the current data set
		GameData.current = myGame;

        GameData.current.isGameCompleted = isGameCompleted;
		GameData.current.score =score;

		GameData.current.doorLocks =doorLocks;
		GameData.current.level1 = level1;
		GameData.current.level2 =level2;
		GameData.current.level3= level3;
		GameData.current.level4=level4;
		GameData.current.level5=level5;

        GameData.current.Level_Scores = Level_Scores;

		GameData.current.i_anagrams_106=i_anagrams_106;
		GameData.current.i_Cryptogram_104 =i_Cryptogram_104;
		GameData.current.i_ImageScramble_107=i_ImageScramble_107;
		GameData.current.i_PuzzleTemplate_100 = i_PuzzleTemplate_100;
		GameData.current.i_SimonSays_102= i_SimonSays_102;
		GameData.current.i_Tangrams_103 =i_Tangrams_103;
		GameData.current.i_WireConnection_105 =i_WireConnection_105;
		GameData.current.i_WordPasscode_101 =i_WordPasscode_101;

		//call save method
		SaveLoad.Save ();
		     
    }

    public void LoadGame() {
		//load the data
		SaveLoad.Load ();
        //sets the player's progress to what the saved data was

        isGameCompleted = GameData.current.isGameCompleted;

        score = GameData.current.score;

		doorLocks = GameData.current.doorLocks;
		level1 = GameData.current.level1;
		level2 = GameData.current.level2;
		level3 = GameData.current.level3;
		level4 = GameData.current.level4;
		level5 = GameData.current.level5;

        Level_Scores = GameData.current.Level_Scores;

		i_anagrams_106 = GameData.current.i_anagrams_106;
		i_Cryptogram_104 = GameData.current.i_Cryptogram_104;
		i_ImageScramble_107 = GameData.current.i_ImageScramble_107;
		i_PuzzleTemplate_100 = GameData.current.i_PuzzleTemplate_100;
		i_SimonSays_102 = GameData.current.i_SimonSays_102;
		i_Tangrams_103 = GameData.current.i_Tangrams_103;
		i_WireConnection_105 = GameData.current.i_WireConnection_105;
		i_WordPasscode_101 = GameData.current.i_WordPasscode_101;
        
		GameManager.instance.doorLocks = doorLocks;
		//updates the current score and game status in gameManager
        GameManager.instance.LoadStats(score, isGameCompleted);
    }
}
