// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * This class will be used for opening and exiting a puzzle instruction
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {

    public Button exitButton;
    public Button instrButtion;
    public Puzzle puz;

    void Start () {
        exitButton.onClick.AddListener(InstruExit);
        instrButtion.onClick.AddListener(ShowInstructions);
        if (puz == null) {
            Debug.LogError("Set a ref of puzzle in Instructions the script");
        }
        gameObject.SetActive(false);
        //Check to see if the instructions has been show to the player if not then it will show the instructions when the puzzle starts.
        switch (puz.puzzleID)
        {
            case 100:
                if (Player.instance.playerProgress.i_PuzzleTemplate_100)
                {
                    Player.instance.playerProgress.i_PuzzleTemplate_100 = false;
                    gameObject.SetActive(true);
                }
                break;
            case 101:
                if (Player.instance.playerProgress.i_WordPasscode_101)
                {
                    Player.instance.playerProgress.i_WordPasscode_101 = false;
                    gameObject.SetActive(true);
                }
                break;
            case 102:
                if (Player.instance.playerProgress.i_SimonSays_102)
                {
                    Player.instance.playerProgress.i_SimonSays_102 = false;
                    gameObject.SetActive(true);
                }
                break;
            case 103:
                if (Player.instance.playerProgress.i_Tangrams_103)
                {
                    Player.instance.playerProgress.i_Tangrams_103 = false;
                    gameObject.SetActive(true);
                }
                break;
            case 104:
                if (Player.instance.playerProgress.i_Cryptogram_104)
                {
                    Player.instance.playerProgress.i_Cryptogram_104 = false;
                    gameObject.SetActive(true);
                }
                break;
            case 105:
                if (Player.instance.playerProgress.i_WireConnection_105)
                {
                    Player.instance.playerProgress.i_WireConnection_105 = false;
                    gameObject.SetActive(true);
                }
                break;
            case 106:
                if (Player.instance.playerProgress.i_anagrams_106)
                {
                    Player.instance.playerProgress.i_anagrams_106 = false;
                    gameObject.SetActive(true);
                }
                break;
            case 107:
                if (Player.instance.playerProgress.i_ImageScramble_107)
                {
                    Player.instance.playerProgress.i_ImageScramble_107 = false;
                    gameObject.SetActive(true);
                }
                break;
        }
    }


    //Instruction buttion
    public void ShowInstructions() {
        gameObject.SetActive(true);
    }
    //unactivate the partent gameobject attched to this script
    public void InstruExit() {
        gameObject.SetActive(false);
    }
}
