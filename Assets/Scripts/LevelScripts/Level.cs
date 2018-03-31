using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;
	public float scoreSum=0;
    private int levelID = 0;

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
    //time in the level
    private float time;

	// Use this for initialization
	void Start () {
		//set the player spawn in the level
		GameManager.instance.SetPlayerLocation(PlayerSpawn);
        //set timer
        startTime = Time.time;
        //set the current score to levelScore 
        currentLevelScore = 0;
        //turn on some UIs
        timerText.gameObject.SetActive(true);
        levelID = backToHub.levelID;
    }


    //UI  / Time
    private void Update()
    {
        //Timer updates
        time = Time.time - startTime;
        currentTimeMin = ((int)time / 59);
        currentTimeSecs = (time % 59);

        //Update UI timer text
        timerText.text = "Time\n" +  currentTimeMin.ToString("00") + ":" + currentTimeSecs.ToString("00");
    }
    public void LevelComplete()
    {
        
        CalulateLevelScore();
        //Level completion call
        GameManager.instance.LevelCompleted(levelID, currentLevelScore);

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
		scoreSum+=(diff *150);
    }
    public void CalulateLevelScore()
    {
		//FOREACH PUZZLE(puzzle difficulty *1500)
		//(SUM/TIME IN MINUTES)*100 (MAKE WHOLE NUMBER)
		float timeInMin = (time/60f);
		scoreSum = ((float)scoreSum / timeInMin)*10;
		currentLevelScore = (int)scoreSum;

        //TODO: calulate score based on time here. Use: currentTimeMin, currentTimeSecs. 
        //Update the currentLevelScore field
    }

}
