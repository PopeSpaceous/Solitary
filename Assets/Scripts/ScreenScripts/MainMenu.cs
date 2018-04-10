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

    //Toggle SFX
    public Button sfxCreditsButton;
    public GameObject SFXcredits;
    

    public GameObject manager;

    //HighScores Fields
    string[] items;
    WWW www;

	bool showSFX = true;
    //Highscores connecting panel
    public GameObject connectingOB;
    //Highscores data
    public Text[] names;
    public Text[] scores;

    //View for uploading highscores
    public GameObject UploadHighScoreView;
    //Input field for inputing username
    public GameObject UploadHighScoreNewView;
    //View for updaing higscores
    public GameObject UpdateHighScoresView;
    //show the player's highscores
    public Text highScoreNumber;


    public InputField nameInput;

    public Button buttonUploadDB;
    public Button buttonUploadCancel;
    //allow the upload button to reg once!
    private bool hasClickedUpload = false;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(manager);
        }
    }

    // Use this for initialization
    void Start () {
        //main menu buttons
        newGameButton.onClick.AddListener(NewGame);
        loadGameButton.onClick.AddListener(RunLoadGame);
        exitButton.onClick.AddListener(delegate { Application.Quit(); });
        highScoresButton.onClick.AddListener(EnterHighScores);
        creditsButton.onClick.AddListener(EnterCredits);
        //Highscores exit button
        highScoresexitButton.onClick.AddListener(ExitScores);       
        creditsExitButton.onClick.AddListener(ExitCredits);
        sfxCreditsButton.onClick.AddListener(TogSFX);

        //Upload highScores View-----
        if (GameManager.instance.gameEnded && GameData.current.score >= GameManager.instance.highScore) {
            //add the listeners for the buttons
            buttonUploadDB.onClick.AddListener(CheckCanUpload);
            buttonUploadCancel.onClick.AddListener(ExitScores);
            //Show upload highscore view
            ShowUploadHighScoresView();

            hasClickedUpload = false;
        }

        //Show load game button if there is a save file
        if (CheckLoadGame())
        {
            loadGameButton.gameObject.SetActive(true);
        }
        //credits exit button 

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
    //Load in HighScores table
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

	void TogSFX(){
		SFXcredits.gameObject.SetActive (showSFX);
		showSFX = !showSFX;
	}

    //Exit Highscore upload
    void ExitUploadScores()
    {
        main.SetActive(true);
        UploadHighScoreView.SetActive(false);
    }

    //Upload highscores. will show in main meni first if game iscompleted
    void ShowUploadHighScoresView() {
        //turn off the main view
        main.SetActive(false);

        //turn on the upload view
        UploadHighScoreView.SetActive(true);
        //show highscore number
        highScoreNumber.text = GameManager.instance.highScore.ToString();

        //Check if user has id
        //TODO: fix this, so a fetch happens first to check if there is data in the db
        if (GameManager.instance.playerId != 0)
        {
            UpdateHighScoresView.SetActive(true);
            UploadHighScoreNewView.SetActive(false);
        }

    }
    //check if the input feild is filled to start an upload
    void CheckCanUpload() {
        if (!hasClickedUpload)
        {
            hasClickedUpload = true;
            //TODO: fix this, so a fetch happens first to check if there is data in the db
            if (nameInput.text != "" && GameManager.instance.playerId == 0)
            {
                UploadStart(nameInput.text);
            }
            else if (GameManager.instance.playerId != 0)
            {
                UploadStart();
            }
            else
            {
                //TODO: do feed back to UI that the input field is empty
            }
        }

    }
    //Start upload to DB
    void UploadStart( string name = null) {
        GameManager.instance.UploadToDB(name);
        ExitUploadScores();        
    }

}
