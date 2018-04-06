using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
 * Loader class will only be used to load the static gameObjects (Player, GameManager) 
 * Loader class will be placed in the level Hub. This why the game must be ran the first time in Level->Hub
*/
public class Loader : MonoBehaviour {

	//PreFab referneces 
	public GameObject player;
	public GameObject manager;


	void Awake () {
        if (GameManager.instance == null)
        {
            Object.Instantiate(manager);
        }
        //We create the static gameObjects once.
        if (Player.instance == null){
			Object.Instantiate (player);
		}


	}

}
