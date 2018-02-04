using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInteraction : MonoBehaviour {

	public string goToPuzzle;

	private PlayerInteraction playerInter;

	// Use this for initialization
	void Start () {
		playerInter = Player.instance.GetComponent<PlayerInteraction>();
	}
	
	void OnTriggerStay2D( Collider2D col)
	{
		if(col.gameObject.name.Equals("Player(Clone)") && playerInter.actionButtion){

			//Load Puzzle Scene on top the current scene
			NextSceneManager.instance.LoadPuzzleScene (goToPuzzle);
		}

	}
}
