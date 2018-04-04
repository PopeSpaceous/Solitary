using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMang : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;
            if (stayLitCounter < 0)
            {
                colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.5f);
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
            } else
            {
                if (waitBetweenCounter < 0)
                {
                    //light up selected colour
                    colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.5f);
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

                for (int i = correctGuesses; i < 7; i++)
                {
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
            }
            else
            {
                Debug.Log("WRONG!");
                //incorrect.Play();
                gameActive = false;
            }
        }
    }

    /*
     * Stil working on the button to replay the sequence if the user is stuck
    public void ReplayPattern()
    {
        for (int i = 0; i < activeSequence.Count; i++)
        {
            colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.75f);
        }
    }
    */
}
