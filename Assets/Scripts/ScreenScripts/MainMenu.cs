using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject main;
    public GameObject credits;

    public Button loadGameButton;
    public Button newGameButton;
    public Button exitButton;
    public Button highScoresButton;
    public Button creditsButton;
    public Button creditsExitButton;

	// Use this for initialization
	void Start () {
        //main menu buttons
        newGameButton.onClick.AddListener(delegate { NextSceneManager.instance.LoadLevelScene("Hub");  });
        exitButton.onClick.AddListener(delegate { Application.Quit(); });
        //TODO: complete loadgamebutton highscorebutton

        //highScoresButton load high score scene
        //Maybe add a bool to true when loading the game. Have Playerprogress check it and load the game if true 

        //credits buttons
        creditsButton.onClick.AddListener(EnterCredits);
        creditsExitButton.onClick.AddListener(ExitCredits);

    }

    void EnterCredits() {
        main.SetActive(false);
        credits.SetActive(true);

    }
    void ExitCredits() {
        main.SetActive(true);
        credits.SetActive(false);
    }

}
