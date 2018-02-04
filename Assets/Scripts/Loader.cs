using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	//preFab referneces 
	public GameObject  player;
	public GameObject manager;


	void Awake () {
		//We create the gameObject once.
		if(Player.instance == null && GameManager.instance == null){
			Object.Instantiate (player);
			Object.Instantiate (manager);
		}

	}

}
