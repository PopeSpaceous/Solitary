// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * The main door for entering a level
 * this door can also use worldobjects if connected
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	public string levelName;
    public int levelID = 0;
	public bool isDoorlocked = false;
    public WorldObject worldObject;
    // Level will be the only scene to populate this var
    public Level level = null;
    // Hub will be the only scene to populate this var
    public Level_Hub levelHub = null;
    // ref of the fade UI object. This must be filled in the inspecter
    public GameObject fadeObject; 
    //Fade vars
    private Animator fader;
    private Image im;

    void Awake()
    {
        fader = fadeObject.GetComponent<Animator>();
        im = fadeObject.GetComponent<Image>();
    }
    //Set the worldobject state. This method will be called by level_hub
    public void SetWorldObject() {
        if (worldObject != null)
        {
            worldObject.Lock();

            if (!isDoorlocked && !GameManager.instance.isGameComplete)
            {
                worldObject.Unlock();
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
	{
        //if a player is on the trigger and is pressing the action buttion it will go to next scene
        if (col.gameObject.CompareTag("Player") && Player.instance.actionButtion && !isDoorlocked) 
		{
            
            DoorTigger();
        }
	}
    public  void DoorTigger() {
        // make sure we don't keep calling the load level scene while the scene is still the fade transition
        if (!fader.GetBool("Fade"))
        { 
            NextSceneManager.instance.LoadLevelScene(levelName, fader, im);
        }
        //Exit the level
        if (level != null)
        {
            level.LevelExit();
        }
        else
        {
            levelHub.Exit();
        }
    }
}
