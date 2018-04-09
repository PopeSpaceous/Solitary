using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Text;
using System;


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

    string[] items;
    WWW www;

    //scorestable panel
    public GameObject connectingOB;
    //scorestable text
    public Text topscores;

    public Text[] names;
    public Text[] scores;

    // Use this for initialization
    void Start () {

        if (GameManager.instance == null) {
            Instantiate(manager);
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

        www = new WWW("https://anthonynguyen435.000webhostapp.com/highscores_data.php");

        yield return www;
        string tableData = www.text;

        if (www.text != "") {
            connectingOB.SetActive(false);
            

            items = tableData.Split(';');

            int ctr = 0;

            foreach (string row in items)
            {
                if (row != "") {
                    string[] cols = row.Split(':');
                    if (cols != null) {
                        names[ctr].text = cols[0];
                        scores[ctr].text = cols[1];
                    }
                    ctr++;
                }
            }
        }


    
    }

    //Exit Highscore table and go back to main screen
    void ExitScores() {
        main.SetActive(true);
        scoresList.SetActive(false);
    }

}
