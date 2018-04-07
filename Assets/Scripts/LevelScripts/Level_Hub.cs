using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Hub : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;
    // score UI to display
    public Text score;
    // Hub doors ref (volatile of values after every new scene load). 
    //For now, the order of this array must be the same to the order to the levels unlocked
    public Door[] levelDoors; 

    public GameObject ladderLevel5;

    void Start()
    {
        //set up door locks every time the hub scene is loaded
        for (int ctr = 0; ctr < levelDoors.Length; ctr++) {
            levelDoors[ctr].isDoorlocked = GameManager.instance.doorLocks[ctr];
            levelDoors[ctr].SetWorldObject();
        }
        if (!GameManager.instance.doorLocks[4]) {
            ladderLevel5.SetActive(true);
            ladderLevel5.GetComponent<Animator>().SetBool("Freeze", GameManager.instance.isGameComplete);
        }

        //place player at spawn point
        GameManager.instance.SetPlayerLocation(PlayerSpawn);
        //show score
        score.gameObject.SetActive(true);
        //unlock the player's movement
        Player.instance.ChangeMovementLock(true);
    }

    private void Update()
    {
        //update Score UI
        score.text = "Score " + GameManager.instance.currentScore.ToString();
    }

    public void Exit() {
        //don't show score when we leave the hub
        score.gameObject.SetActive(false);
    }
}
