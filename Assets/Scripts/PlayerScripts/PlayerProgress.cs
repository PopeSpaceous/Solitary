using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour {
	public GameData myGame;
    private int score;
    private int highScore;

	public bool[] doorLocks = null;

    //bools to store which level has been completed
    public bool level1 = false;
    public bool level2 = false;
    public bool level3 = false;
    public bool level4 = false;
    public bool level5 = false;

    //Show instructions bools
    public bool i_PuzzleTemplate_100 = true;
    public bool i_WordPasscode_101 = true;
    public bool i_SimonSays_102 = true;
    public bool i_Tangrams_103 = true;
    public bool i_Cryptogram_104 = true;
    public bool i_WireConnection_105 = true;
    public bool i_anagrams_106 = true;
    public bool i_ImageScramble_107 = true;


	private void Awake(){
	}

    private void Start()
    {
		doorLocks = GameManager.instance.doorLocks;  // get a ref by the gm
        //load game gets called here
    }

    public void UpdatePlayerStats(int s) {
        if (s >= highScore)
        {
            highScore = s;
        }
        score = s;      
    }

    public void SaveGame() {
		//set game data to the current data set
		GameData.current = myGame;
		GameData.current.score =score;
		GameData.current.highScore =highScore;

		GameData.current.doorLocks =doorLocks;
		GameData.current.level1 = level1;
		GameData.current.level2 =level2;
		GameData.current.level3= level3;
		GameData.current.level4=level4;
		GameData.current.level5=level5;

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
		score = GameData.current.score;
		highScore = GameData.current.highScore;

		doorLocks = GameData.current.doorLocks;
		level1 = GameData.current.level1;
		level2 = GameData.current.level2;
		level3 = GameData.current.level3;
		level4 = GameData.current.level4;
		level5 = GameData.current.level5;

		i_anagrams_106 = GameData.current.i_anagrams_106;
		i_Cryptogram_104 = GameData.current.i_Cryptogram_104;
		i_ImageScramble_107 = GameData.current.i_ImageScramble_107;
		i_PuzzleTemplate_100 = GameData.current.i_PuzzleTemplate_100;
		i_SimonSays_102 = GameData.current.i_SimonSays_102;
		i_Tangrams_103 = GameData.current.i_Tangrams_103;
		i_WireConnection_105 = GameData.current.i_WireConnection_105;
		i_WordPasscode_101 = GameData.current.i_WordPasscode_101;
        
		GameManager.instance.doorLocks = doorLocks;
		//updates the current score in gameManager
        GameManager.instance.LoadGame(score);
    }
}
