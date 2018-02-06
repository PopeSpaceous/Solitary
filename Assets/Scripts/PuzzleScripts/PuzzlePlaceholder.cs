using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzlePlaceholder : MonoBehaviour {

	public int difficulty = 1;
	public Level_Door door = null;

	[HideInInspector]
	public string puzzleGoTo;


	void OnTriggerStay2D( Collider2D col)
	{
		if(col.gameObject.name.Equals("Player(Clone)") && Player.instance.actionButtion){
			//Lock the player's movement
			Player.instance.canPlayerMove = false;

			//Place the difficulty values in a NextSceneManager var. So when the loaded puzzle scene
			//is loaded in its Awake() function it will get the NextSceneManager var and set that difficulity 
			NextSceneManager.instance.setPuzzledifficulty = difficulty;
			//pass this current placeholder instance for actions changes when the puzzle is complete
			NextSceneManager.instance.placeholder = this;

			//Load Puzzle Scene on top the current scene
			NextSceneManager.instance.LoadPuzzleScene (puzzleGoTo);
		}

	}
		
}
