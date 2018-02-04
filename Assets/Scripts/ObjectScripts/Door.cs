using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	//if this door goes to a another level populate this field
	public string levelName;

	private PlayerInteraction playerInter;

	void Start(){
		playerInter = Player.instance.GetComponent<PlayerInteraction> ();
	}
	 
	void OnTriggerStay2D(Collider2D col)
	{
		//if the player is on the trigger and is pressing the action buttion it will go to next scene
		//TODO: there needs to be a door lock
		if(levelName != "" && col.gameObject.name.Equals("Player(Clone)") && playerInter.actionButtion) 
		{
			//Load next scene
			NextSceneManager.instance.LoadLevelScene (levelName);
		}
	}
}
