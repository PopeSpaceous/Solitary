using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*This Door class will only be used for door that switch to another level*/
public class Door : MonoBehaviour {

	public string levelName;
	public bool isDoorlocked = false; 


	//TODO: Complete door locking / unlocking

	void OnTriggerStay2D(Collider2D col)
	{
		//if a player is on the trigger and is pressing the action buttion it will go to next scene
		if(col.gameObject.name.Equals("Player(Clone)") && Player.instance.actionButtion) 
		{
			//Load next scene
			NextSceneManager.instance.LoadLevelScene (levelName);
		}

	}




}
