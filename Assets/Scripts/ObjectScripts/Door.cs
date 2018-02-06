using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Door : MonoBehaviour {

	public string levelName;
	public bool isDoorlocked = false; 


	//TODO: Complete door locking when a level is finished

	void OnTriggerStay2D(Collider2D col)
	{
		//if the player is on the trigger and is pressing the action buttion it will go to next scene
		if(col.gameObject.name.Equals("Player(Clone)") && Player.instance.actionButtion) 
		{
			//Load next scene
			NextSceneManager.instance.LoadLevelScene (levelName);
		}

	}




}
