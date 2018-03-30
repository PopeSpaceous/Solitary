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
        highScoresexitButton.onClick.AddListener(ExitScores);

        if (CheckLoadGame()) {
            loadGameButton.gameObject.SetActive(true);            
        }

        //credits buttons
        creditsButton.onClick.AddListener(EnterCredits);
        creditsExitButton.onClick.AddListener(ExitCredits);

    }

    bool CheckLoadGame() {
        return File.Exists(Application.persistentDataPath + "/savedGames.gd");
    }

    void RunLoadGame() {
        GameManager.instance.loadGameFile = true;
        NextSceneManager.instance.LoadLevelScene("Hub");
    }

    void EnterCredits() {
        main.SetActive(false);
        credits.SetActive(true);

    }
    void ExitCredits() {
        main.SetActive(true);
        credits.SetActive(false);
    }

    void EnterHighScores() {
        main.SetActive(false);
        scoresList.SetActive(true);
    }
    void ExitScores() {
        main.SetActive(true);
        scoresList.SetActive(false);
    }

}
