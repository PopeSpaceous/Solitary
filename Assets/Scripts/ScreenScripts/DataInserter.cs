using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInserter : MonoBehaviour {

 
    public string inputUsername;

    string CreateUserURL = "https://anthonynguyen435.000webhostapp.com/insertUser.php";
    string GetIDURL = "https://anthonynguyen435.000webhostapp.com/GetId.php";
    string updateScoreURL = "https://anthonynguyen435.000webhostapp.com/UpdateUser.php";

    bool hasIDInDB = false;
    // Use this for initialization
    void Start () {
        StartCoroutine(NewScoreEntry());

    }

    public void UploadHighScores() {
        if (Player.instance.playerProgress.id != 0) {
            StartCoroutine(LoadID());

        }
        else {
            StartCoroutine(NewScoreEntry());
        }
    }

    IEnumerator LoadID() {
        WWWForm form = new WWWForm();
       // form.AddField("idPost", Player.instance.playerProgress.id);
        form.AddField("idPost", 3);

        WWW www = new WWW(GetIDURL, form);

        yield return www;


        if (www.text != "") {
            //if (int.Parse(www.text) == Player.instance.playerProgress.id) {
            //Element 0 = id, 1 = name
            string[] feedBackData = www.text.Split(';');
            if (int.Parse(feedBackData[0]) == 3 && feedBackData[1] != null) {
                Debug.Log("Id Found! Username: "+ feedBackData[1] + " updating...");
                hasIDInDB = true;
                StartCoroutine(UpdateScore());
            }
        }
    }

    IEnumerator UpdateScore()
    {
        WWWForm form = new WWWForm();

        //form.AddField("idPost", Player.instance.playerProgress.id);
        form.AddField("idPost", 3);
        form.AddField("scorePost", 696969);

        WWW www = new WWW(updateScoreURL, form);

        yield return www;
        Debug.Log("Updated data? :" + www.text);
        //TODO: need feedback

    }


    IEnumerator NewScoreEntry() {

        WWWForm form = new WWWForm();
        form.AddField("playerPost", inputUsername);
        form.AddField("scorePost", 69);
        //form.AddField("scorePost", Player.instance.playerProgress.highScore);

        WWW www = new WWW(CreateUserURL, form);

        yield return www;

        if (www.text != "") {
            string[] feedBackID = www.text.Split(';');
            if (feedBackID[0] != null) {
                Debug.Log("NEW ENRTY ID:" + feedBackID[0]);
                //update the playerprogess id
            }
            
        }
    }
}
