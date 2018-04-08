using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level : MonoBehaviour {

	//A gameobject place marker that we will reference in the Inspector
	public Transform PlayerSpawn = null;

    private int levelID = 0;

    public Text timerText;
    public Text levelCompleteMessage;

    public Door backToHub;// Door will be used when the level is completed to exit the level

    private int currentLevelScore;
    private float startTime;

    //total time fields 
    private float currentTimeMin = 0;
    private float currentTimeSecs = 0;
	//private AudioSource backgroundMusic;

    //wait time until back to hub is called when the level is complete
    private float delayTime = 2f;
    //time in the level
    private float time;
    //timer stop
    private bool timeStop = false;

	// Use this for initialization
	void Start () {
		//backgroundMusic = GameObject.FindGameObjectWithTag ("music").GetComponent<AudioSource> ();
		//StartCoroutine(AudioFadeIn.FadeIn(backgroundMusic, 3f));
		//set the player spawn in the level
		GameManager.instance.SetPlayerLocation(PlayerSpawn);
        //set timer
        startTime = Time.time;
        //set the current score to levelScore 
        currentLevelScore = 0;
        //turn on some UIs
        timerText.gameObject.SetActive(true);
		//set audio transition fade in
        //set level ID based on the door
        levelID = backToHub.levelID;
    }


    //UI  / Time
    private void Update()
    {
        if (!timeStop)
        {
            //Timer updates
            time = Time.time - startTime;
            currentTimeMin = ((int)time / 59);
            currentTimeSecs = ((int)time % 59);
        }
        //Update UI timer text
        timerText.text = currentTimeMin.ToString("00") + ":" + currentTimeSecs.ToString("00");
    }
    public void LevelComplete()
    {
        //stop the timer
        timeStop = true;
        CalulateLevelScore();
        //Level completion call
        GameManager.instance.LevelCompleted(levelID, currentLevelScore);

        //Level complete message
        levelCompleteMessage.text = "Level " + levelID.ToString() + " Complete!" + "\n" + "Score: " + currentLevelScore;
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
		//set transition
		//StartCoroutine(AudioFadeOut.FadeOut(backgroundMusic, 5f));
        //turn off some UIs
        timerText.gameObject.SetActive(false);
    }

    //When a puzzle is completed this function will be called for score calulation
    public void PuzzleUpdateScore(int diff)
    {
		currentLevelScore+=(diff *150);
    }
    public void CalulateLevelScore()
    {
		//FOREACH PUZZLE(puzzle difficulty *1500)
		//(SUM/TIME IN MINUTES)*100 (MAKE WHOLE NUMBER)
        //WHATS WITH THE CAPS, CILL MAN. - Love, Leo
		currentLevelScore = (int)((currentLevelScore / (currentTimeMin + 1))*10);
    }

}
