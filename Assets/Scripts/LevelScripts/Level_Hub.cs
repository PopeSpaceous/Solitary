using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Hub : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;

    public Text score; // score to display

    public Door[] levelDoors; // for now, the order of this array must be the same to the order to the levels unlocked


    private void Awake()
    {
        //set Doors
        //for now we will assume the array order is the same to levels numbers
        for (int ctr = 0; ctr < levelDoors.Length - 1; ctr++)
        {
            levelDoors[ctr].isDoorlocked = true;
        }
    }
    void Start()
    {
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
