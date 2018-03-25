using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour {

    private int score;
    private int highScore;

    public bool[] doorLocks = null; 

    //bools to store which level has been completed
    public bool level1 = false;
    public bool level2 = false;
    public bool level3 = false;
    public bool level4 = false;
    public bool level5 = false;


    private void Awake()
    {
        
    }

    private void Start()
    {
        doorLocks = GameManager.instance.doorLocks; // get a ref by the gm
        //load game gets called here
    }

    public void UpdatePlayerStats(int s) {
        if (s >= highScore)
        {
            highScore = s;
        }
        score = s;      
    }

    public void SaveGame() {
        //TODO: complete save game        
    }

    public void LoadGame() {

        //TODO: Complete load game
        //Replace values of doorLocks from GM and here.

        GameManager.instance.LoadGame(score);
    }
}
