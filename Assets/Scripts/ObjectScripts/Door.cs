using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*This Door class will only be used for door that switch to another level*/
public class Door : MonoBehaviour {

	public string levelName;
    public int levelID = 0;
	public bool isDoorlocked = false;
    public Level level = null;// hub will be the only scene to not populate this var
    public Level_Hub levelHub = null;

    public GameObject fadeObject; // ref of the fade UI object. This must be filled in the inspecter

    //Needed fade vars
    private Animator fader;
    private Image im;

    //TODO: Complete door locking / unlocking for level entry

    void Awake()
    {
        fader = fadeObject.GetComponent<Animator>();
        im = fadeObject.GetComponent<Image>();
    }

    void OnTriggerStay2D(Collider2D col)
	{
		//if a player is on the trigger and is pressing the action buttion it will go to next scene
		if(col.gameObject.CompareTag("Player") && Player.instance.actionButtion && !isDoorlocked) 
		{
            DoorTigger();
        }
	}
    public  void DoorTigger() {
        if (!fader.GetBool("Fade"))
        { // make sure we don't keep calling the load level scene while the scene is still the fade transition
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
