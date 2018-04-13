// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Anthony Nguyen, Nathan Misener
// Date: 04/13/2018
/* Summary: 
 *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimonSays: Puzzle {

	//all set up puzzle code goes here
	public SpriteRenderer[] colours;
    public BoxCollider2D[] coloursCollider;
	public AudioSource[] buttonSounds;

	private int colourSelect;

	//keeps track of how long colour stays lit for
	public float stayLit;
	private float stayLitCounter;

	public float waitBetweenLights;
	private float waitBetweenCounter;

	private bool shouldBeLit;
	private bool shouldBeDark;

	public List<int> activeSequence;
	private int positionInSequence;

	private bool gameActive;
	private int inputInSequence;

	public AudioSource correct;
	public AudioSource incorrect;

	//check for difficulty
	private int theDifficultyTarget;

	//set sequence targets for appropriate difficulties
	private int easy = 3;
	private int medium = 4;
	private int hard = 5;

	//counter to track responses
	public int correctGuesses;
	public int guessCounter;
	public bool firstTime= true;
	public Button replayButton;
	private Text btnText;
	public Text gCounter;
	// Sets the parent fields
	void Awake () {
		puzzleName = "SimonSays";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		Debug.Log ("Difficulty for puzzle " + puzzleName + " is: "+ this.difficulty);
		btnText = replayButton.GetComponentInChildren<Text> ();
		btnText.text = "Start";
		foreach (BoxCollider2D sr in coloursCollider) {
			sr.enabled =  false;
		}
	}
    
    

    // Update is called once per frame
    void Update()
    {
        if (shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;
            if (stayLitCounter < 0)
            {
                colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.25f);
                buttonSounds[activeSequence[positionInSequence]].Stop();
                shouldBeLit = false;

                shouldBeDark = true;
                waitBetweenCounter = waitBetweenLights;

                positionInSequence++;
            }
        }

        if (shouldBeDark)
        {
            waitBetweenCounter -= Time.deltaTime;

            if (positionInSequence >= activeSequence.Count)
            {
                shouldBeDark = false;
                gameActive = true;
            }
            else
            {
                if (waitBetweenCounter < 0)
                {
                    //light up selected colour
                    colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
                    buttonSounds[activeSequence[positionInSequence]].Play();

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }

    public void StartGame()
    {
        foreach (BoxCollider2D sr in coloursCollider)
        {
            sr.enabled = true;
        }
        btnText.text = "Replay Sequence";
		firstTime = false;
        //reset sequence
        activeSequence.Clear();

        //reset counters
        positionInSequence = 0;
        inputInSequence = 0;
        correctGuesses = 0;
        guessCounter = 3;

        gCounter.text = guessCounter.ToString();

        //check difficulty 
        if (difficulty == 1)
        {
            theDifficultyTarget = easy;
        }

        if (difficulty == 2)
        {
            theDifficultyTarget = medium;
        }

        if (difficulty == 3)
        {
            theDifficultyTarget = hard;
        }

        //add 4 to beginning sequence
        for (int i = 0; i < 4; i++)
        {
            colourSelect = Random.Range(0, colours.Length);

            //add random number to list
            activeSequence.Add(colourSelect);
        }
        

        //light up selected colour
        colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
        buttonSounds[activeSequence[positionInSequence]].Play();

        stayLitCounter = stayLit;
        shouldBeLit = true;

    }

    IEnumerator SequencePause()
    {
        yield return new WaitForSeconds(2);
        
    }

    public void ColourPressed(int whichButton)
    {
        if (gameActive)
        {
            //if button in sequence is equal to button user pressed
            if (activeSequence[inputInSequence] == whichButton)
            {
                inputInSequence++;

                    //add check to see current position equals end of list
                    if (inputInSequence >= activeSequence.Count)
                    {
                        correctGuesses++;
                      
                        positionInSequence = 0;
                        inputInSequence = 0;

                        colourSelect = Random.Range(0, colours.Length);

                        //add random number to list
                        activeSequence.Add(colourSelect);

                        //light up selected colour
                        colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
                        buttonSounds[activeSequence[positionInSequence]].Play();

                        stayLitCounter = stayLit;
                        shouldBeLit = true;

                        gameActive = false;

                        correct.Play();

                        //call pause
                        StartCoroutine("SequencePause");
                      
                }
            }
            else
            {
                incorrect.Play();
                gameActive = false;
                guessCounter--;
                gCounter.text = guessCounter.ToString();                
            }
        }

        if (correctGuesses == theDifficultyTarget)
        {
            PuzzleComplete();
        }

        //3 incorrect guesses
        if (guessCounter == 0)
        {
            PuzzleExit();
        }
    }

    public void ReplayPattern()
    {
        positionInSequence = 0;
        inputInSequence = 0;

        //light up selected colour
        colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
        buttonSounds[activeSequence[positionInSequence]].Play();

        stayLitCounter = stayLit;
        shouldBeLit = true;

        gameActive = false;
    }
    
}

