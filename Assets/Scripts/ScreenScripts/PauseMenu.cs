// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * Will show the pause menu if the scene has the UI canvas prefab.
 * Game will freeze time, and show options for the player.
 * Options include: Resume Game, Main menu, and audio sliders for music, sfx, and puzzle sfx.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    
    //bool for checking if the game has been paused on other scripts
    public static bool isGamePaused = false;
    //menu sceen reference
    public GameObject pauseMenu;
    //hacking tool to be shown when in puzzle and it is puased
    public GameObject hackingTool;
    //Audio sliders UI refs
    public Slider musicSlider;
    public Slider sFXSlider;
    public Slider puzzleSFXSlider;
    //a bool to check if the audio sliders has been set with the proper values
    private bool setSliderListen = false;

   
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
        if (!NextSceneManager.instance.isPuzzleLoaded)
        {
            hackingTool.SetActive(false);
        }
        //first time audio slider sets
        if (!setSliderListen)
        {
            setSliderListen = true;

            musicSlider.value = AudioMix.instance.musiclvl;
            sFXSlider.value = AudioMix.instance.SFXlvl;
            puzzleSFXSlider.value = AudioMix.instance.puzzlvl;

            musicSlider.onValueChanged.AddListener(delegate { AudioMix.instance.SetMusicLvl(musicSlider.value); });
            sFXSlider.onValueChanged.AddListener(delegate { AudioMix.instance.SetSFXLvl(sFXSlider.value); });
            puzzleSFXSlider.onValueChanged.AddListener(delegate { AudioMix.instance.SetPuzzLvl(puzzleSFXSlider.value); });
        }
    }

    public void ResumeGame() {
        isGamePaused = false;
        //Unfreeze time
        Time.timeScale = 1f;
        //unshow the pause screen
        pauseMenu.SetActive(false);
        hackingTool.SetActive(true);
    }
    //Trigger a back to the main menu procedure 
    public void Menu() {
        
        GameManager.instance.ExitGame("MainMenu");
        ResumeGame();
    }

}
