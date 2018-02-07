using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/* PuzzlePlaceholder */
public class PuzzlePlaceholder : MonoBehaviour {

	//difficultyNeed will be used to influence the PuzzlerRanomization difficulty selection. 
	//PuzzlerRanomization will ultimately decide on the puzzle's difficulty
	//For Debugging use, difficultyNeed will be the actual difficulty for the puzzle.
	public int difficultyNeed = 1; 

	public Level_Door door = null;

	[HideInInspector]
	public string puzzleGoTo;


	void OnTriggerStay2D( Collider2D col)
	{
		if(col.gameObject.name.Equals("Player(Clone)") && Player.instance.actionButtion && Player.instance.canPlayerMove){
			//Lock the player's movement
			Player.instance.canPlayerMove = false;

			//Place the difficulty values in a NextSceneManager var. So when the loaded puzzle scene
			//is loaded in its Awake() function it will get the NextSceneManager var and set that difficulity 
			NextSceneManager.instance.setPuzzledifficulty = difficultyNeed;
			//pass this current placeholder instance for actions changes when the puzzle is complete
			NextSceneManager.instance.placeholder = this;

			//Load Puzzle Scene on top the current scene
			NextSceneManager.instance.LoadPuzzleScene (puzzleGoTo);
		}

	}
		
}
