using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Data class that can be stored as a serialized save file
[System.Serializable]
public class GameData {
	//reference to itself
	public static GameData current;

    //Data members that mirror playerProgress class

    //Game completion bool
    public bool isGameCompleted;

    public  int score = 0;


	public  bool[] doorLocks = null;

	//bools to store which level has been completed
	public  bool level1;
	public  bool level2;
	public  bool level3;
	public  bool level4;
	public  bool level5;

	//Show instructions bools
	public  bool i_PuzzleTemplate_100;
	public  bool i_WordPasscode_101;
	public  bool i_SimonSays_102;
	public  bool i_Tangrams_103;
	public  bool i_Cryptogram_104;
	public  bool i_WireConnection_105;
	public  bool i_anagrams_106;
	public  bool i_ImageScramble_107;

	//default constructor
	public GameData(){

	}
}
