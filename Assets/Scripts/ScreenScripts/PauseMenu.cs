using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    
    //bool for checking if the game has been paused on other scripts
    public static bool isGamePaused = false;
    //menu sceen reference
    public GameObject pauseMenu;

    void Update () {
        //check if the player has pressed on the cancel key
        if (Player.instance.escapeInput) {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        isGamePaused = true;
        //Freeze time
        Time.timeScale = 0;
        //show pause screen
        pauseMenu.SetActive(true);
    }

    public void ResumeGame() {
        isGamePaused = false;
        //Unfreeze time
        Time.timeScale = 1f;
        //unshow the pause screen
        pauseMenu.SetActive(false);
    }
    //Trigger a back to the main menu procedure 
    public void Menu() {
        ResumeGame();
        GameManager.instance.ExitBackMainMenu();
    }

}
