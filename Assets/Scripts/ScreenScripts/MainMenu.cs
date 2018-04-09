﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Text;
using System.Data.SqlClient;
using System;
using System.Data;


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
    public GameObject manager;


	// Use this for initialization
	void Start () {

        if (GameManager.instance == null) {
            Object.Instantiate(manager);
        }
        //main menu buttons
        newGameButton.onClick.AddListener(NewGame);
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

        //panel
        //scoresTable.gameObject.SetActive = (false);
    }

    void NewGame() {
        GameManager.instance.NewGame();
        NextSceneManager.instance.LoadLevelScene("IntroScreen");        
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
       
        StartCoroutine(LoadHighScoresData());
    }

    IEnumerator LoadHighScoresData() {
        //TODO: add load high score data here

        www = new WWW("https://anthonynguyen435.000webhostapp.com/highscores_data.php");

        yield return www;
        
        string scores = www.text;

        items = scores.Split(';');
        
        //print scores into console
        foreach (string i in items)
        {
            print(i);
        }

        //print scores into scorepanel
    
    }

    //Exit Highscore table and go back to main screen
    void ExitScores() {
        main.SetActive(true);
        scoresList.SetActive(false);
    }

}
