using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimonSays: Puzzle {

    
	// Sets the parent fields
	void Awake () {
		puzzleName = "SimonSays";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		Debug.Log ("Difficulty for puzzle " + puzzleName + " is: "+ this.difficulty);
	}
    
    /* Your wonderful startup puzzle code here :3 */
    //all set up puzzle code goes here
    public SpriteRenderer[] colours;
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

    //counter to track correct responses
    public int correctGuesses;

    // Update is called once per frame
    void Update()
    {
        if (shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;
            if (stayLitCounter < 0)
            {
                colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.25f);
                //buttonSounds[activeSequence[positionInSequence]].Stop();
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
                    //buttonSounds[activeSequence[positionInSequence]].Play();

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }

    public void StartGame()
    {
        activeSequence.Clear();

        positionInSequence = 0;
        inputInSequence = 0;

        //reset counter
        correctGuesses = 0;

        colourSelect = Random.Range(0, colours.Length);

        //add random number to list
        activeSequence.Add(colourSelect);

        //light up selected colour
        colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
        //buttonSounds[activeSequence[positionInSequence]].Play();

        stayLitCounter = stayLit;
        shouldBeLit = true;
    }

    public void ColourPressed(int whichButton)
    {
        if (gameActive)
        {
            //if button in sequence is equal to button user pressed
            if (activeSequence[inputInSequence] == whichButton)
            {
                Debug.Log("Correct");
                inputInSequence++;

                    //add check to see current position equals end of list
                    if (inputInSequence >= activeSequence.Count)
                    {
                        correctGuesses++;
                        Debug.Log("Correct number of sequences: " + correctGuesses);

                        positionInSequence = 0;
                        inputInSequence = 0;

                        colourSelect = Random.Range(0, colours.Length);

                        //add random number to list
                        activeSequence.Add(colourSelect);

                        //light up selected colour
                        colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
                        //buttonSounds[activeSequence[positionInSequence]].Play();

                        stayLitCounter = stayLit;
                        shouldBeLit = true;

                        gameActive = false;

                        //correct.Play();
                }
            }
            else
            {
                Debug.Log("WRONG!");
                //incorrect.Play();
                gameActive = false;
            }
        }

        if (correctGuesses == 5)
        {
            PuzzleComplete();
        }
    }

    public void ReplayPattern()
    {
        positionInSequence = 0;
        inputInSequence = 0;

        //light up selected colour
        colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
        //buttonSounds[activeSequence[positionInSequence]].Play();

        stayLitCounter = stayLit;
        shouldBeLit = true;

        gameActive = false;
    }
    
}

