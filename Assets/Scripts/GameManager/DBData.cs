using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBData : MonoBehaviour {

 
    public string inputUsername;

    string CreateUserURL = "https://anthonynguyen435.000webhostapp.com/insertUser.php";
    string GetIDURL = "https://anthonynguyen435.000webhostapp.com/GetId.php";
    string updateScoreURL = "https://anthonynguyen435.000webhostapp.com/UpdateUser.php";

    bool hasIDInDB = false;
    // Use this for initialization
    void Start () {
        //Debugging------------
        //StartCoroutine(NewScoreEntry());

    }

    public void UploadHighScores(string username) {
        inputUsername = username;
        Debug.Log("Upload Started....");
        if (GameManager.instance.playerId != 0) {
            StartCoroutine(LoadID());

        }
        else {
            StartCoroutine(NewScoreEntry());
        }
    }

    IEnumerator LoadID() {
        WWWForm form = new WWWForm();
        Debug.Log("Checking ID.....");
        form.AddField("idPost", GameManager.instance.playerId);

        WWW www = new WWW(GetIDURL, form);

        yield return www;


        if (www.text != "") {
            //Element 0 = id, 1 = name
            string[] feedBackData = www.text.Split(';');
            if (feedBackData[1] != null) {
                int feedBackId;
                if (int.TryParse(feedBackData[0], out feedBackId)) {

                    if (feedBackId == GameManager.instance.playerId)
                    {
                        Debug.Log("ID Found! Username: " + feedBackData[1] + " Updating...");
                        hasIDInDB = true;
                        //TODO: fix this so the Update happens after some UI update happends
                        StartUpdateHighScores();


                    }
                }
            }
        }
    }

    public void StartUpdateHighScores() {
        if(hasIDInDB)
            StartCoroutine(UpdateScore());
    }

    IEnumerator UpdateScore()
    {
        WWWForm form = new WWWForm();
        Debug.Log("Updating....");
        form.AddField("idPost", GameManager.instance.playerId);
        form.AddField("scorePost", GameManager.instance.highScore);

        WWW www = new WWW(updateScoreURL, form);

        yield return www;
        Debug.Log("Updated data? :" + www.text);
        //TODO: need feedback if the update worked
    }


    IEnumerator NewScoreEntry() {

        WWWForm form = new WWWForm();
        Debug.Log("Starting New Score Entry: " + inputUsername);
        //make string first letter upper case
        inputUsername = FirstCharToUpper(inputUsername);

        form.AddField("playerPost", inputUsername);
        form.AddField("scorePost", GameManager.instance.highScore);

        WWW www = new WWW(CreateUserURL, form);

        yield return www;

        if (www.text != "") {
            string[] feedBackID = www.text.Split(';');
            if (feedBackID[0] != null) {
                Debug.Log("New Entry ID:" + feedBackID[0] + " Username: " + inputUsername);
                //TODO: make the player wait until the uploading and saving is done
                GameManager.instance.playerId = int.Parse(feedBackID[0]);
                GameManager.instance.SaveIdentify();
            }
            
        }
    }
    //Makes any givin string have the first letter upper case
    public string FirstCharToUpper(string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("Empty! or null!");
        return input.First().ToString().ToUpper() + input.Substring(1);
    }
}
