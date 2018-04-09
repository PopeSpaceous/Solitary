using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//was following tutorial to add a new user (with a password)
//change password to score with int field
public class DataInserter : MonoBehaviour {

    //testing
    public string inputUsername;
    public int totalscore;

    string CreateUserURL = "https://anthonynguyen435.000webhostapp.com/insertUser.php";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) CreateUser(inputUsername, totalscore);
    }

    public void CreateUser(string username, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerPost", username);
        form.AddField("scorePost", score);

        WWW www = new WWW(CreateUserURL, form);
    }
}
