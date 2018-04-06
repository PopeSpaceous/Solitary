using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Hub : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;
	//private AudioSource backgroundMusic;
    public Text score; // score to display

    public Door[] levelDoors; // for now, the order of this array must be the same to the order to the levels unlocked

    
    void Start()
    {
		//backgroundMusic = GameObject.FindGameObjectWithTag ("music").GetComponent<AudioSource> ();
		//StartCoroutine(AudioFadeIn.FadeIn(backgroundMusic, 3f));
        //set up door locks every time the hub scene is loaded
        for (int ctr = 0; ctr < levelDoors.Length; ctr++) {
            levelDoors[ctr].isDoorlocked = GameManager.instance.doorLocks[ctr];
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

		//music transition
		//StartCoroutine(AudioFadeOut.FadeOut(backgroundMusic, 5f));
        score.gameObject.SetActive(false);
    }
}
