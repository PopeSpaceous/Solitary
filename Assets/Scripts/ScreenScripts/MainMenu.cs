using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject main;
    public GameObject credits;
    public GameObject scoresList;

    public Button loadGameButton;
    public Button newGameButton;
    public Button exitButton;
    public Button highScoresButton;
    public Button highScoresexitButton;
    public Button creditsButton;
    public Button creditsExitButton;

	// Use this for initialization
	void Start () {
        //main menu buttons
        newGameButton.onClick.AddListener(delegate { NextSceneManager.instance.LoadLevelScene("Hub");  });
        loadGameButton.onClick.AddListener(RunLoadGame);
        exitButton.onClick.AddListener(delegate { Application.Quit(); });
        highScoresButton.onClick.AddListener(EnterHighScores);
        creditsButton.onClick.AddListener(EnterCredits);
        //Highscores exit button
        highScoresexitButton.onClick.AddListener(ExitScores);
        //Show load game button if there is a save file
        if (CheckLoadGame()) {
            loadGameButton.gameObject.SetActive(true);            
        }
        //credits exit button        
        creditsExitButton.onClick.AddListener(ExitCredits);

    }
    //Check if there is a save file
    bool CheckLoadGame() {
        return File.Exists(Application.persistentDataPath + "/savedGames.gd");
    }
    //Load the game
    void RunLoadGame() {
        GameManager.instance.loadGameFile = true;
        NextSceneManager.instance.LoadLevelScene("Hub");
    }
    //Show credits
    void EnterCredits() {
        main.SetActive(false);
        credits.SetActive(true);

    }
    //Exit credits and go back to main screen
    void ExitCredits() {
        main.SetActive(true);
        credits.SetActive(false);
    }

    //Switch to highscore table
    void EnterHighScores() {

        main.SetActive(false);

        scoresList.SetActive(true);
    }

    void LoadHighScoresData() {
        //TODO: add load high score data here
    }
    //Exit Highscore table and go back to main screen
    void ExitScores() {
        main.SetActive(true);
        scoresList.SetActive(false);
    }

}
