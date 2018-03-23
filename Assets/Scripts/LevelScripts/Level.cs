using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;

    public int levelID = 0;

    public Text timerText;
    public Text levelCompleteMessage;

    public Door backToHub;// Door will be used when the level is completed to exit the level

    private int currentLevelScore;
    private float startTime;

    //total time fields 
    private float currentTimeMin = 0;
    private float currentTimeSecs = 0;

    //wait time until back to hub is called when the level is complete
    private float delayTime = 2f;

	// Use this for initialization
	void Start () {
		//set the player spawn in the level
		GameManager.instance.SetPlayerLocation(PlayerSpawn);
        //set timer
        startTime = Time.time;
        //set the current score to levelScore 
        currentLevelScore = GameManager.instance.currentScore;
        //turn on some UIs
        timerText.gameObject.SetActive(true);
    }


    //UI  / Time
    private void Update()
    {
        //Timer updates
        float t = Time.time - startTime;
        currentTimeMin = ((int)t / 59);
        currentTimeSecs = (t % 59);
        
        //Update UI timer text
        timerText.text = "Time\n" +  currentTimeMin.ToString() + ":" + currentTimeSecs.ToString("f0");
    }
    public void LevelComplete()
    {

        CalulateLevelScore();
        //Update the player's score
        GameManager.instance.UpdateScore(currentLevelScore);
        GameManager.instance.LevelCompleted(levelID);

        //Level complete message
        levelCompleteMessage.text = "Level " + levelID.ToString() + " Complete!";
        levelCompleteMessage.gameObject.SetActive(true);
        //Lock player's movement
        Player.instance.ChangeMovementLock(false);
        //Exit Level
        StartCoroutine(WaitBackToHub());
    }



    //This method will wait a few secounds until the player returns back to hub when the level is completed
    IEnumerator WaitBackToHub()
    {
        yield return new WaitForSeconds(delayTime);
        levelCompleteMessage.gameObject.SetActive(false);
        backToHub.DoorTigger();
    }

    public void LevelExit()
    {
        //turn off some UIs
        timerText.gameObject.SetActive(false);
    }
    //When a puzzle is completed this function will be called for score calulation
    public void PuzzleUpdateScore(int diff) {
        //TODO: Do partial score calulation here, when a puzzle is completed. Update currentLevelScore 
    }
    public void CalulateLevelScore()
    {
        //TODO: calulate score based on time here. Use: currentTimeMin, currentTimeSecs. 
        //Update the currentLevelScore field
    }

}
