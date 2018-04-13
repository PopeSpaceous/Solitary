// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * For Debugging use only.
 * Loader class will only be used to load the static gameObjects(Player, GameManager)
 * Loader class will be placed in the level Hub. Acutal deployment will be the main menu screen
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	//PreFab referneces 
	public GameObject player;
	public GameObject manager;


	void Awake () {
        //We create the static gameObjects once.
        if (GameManager.instance == null)
        {
            Object.Instantiate(manager);
        }
        
        if (Player.instance == null){
			Object.Instantiate (player);
		}


	}

}
