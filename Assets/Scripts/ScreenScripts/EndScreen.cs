using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    //For Development only
    //TODO: remove during deployment, or keep it if there is a need
    public Button goToMainMenu;
	public Text myText;

    // Use this for initialization
    void Start()
    {
		StartCoroutine(AudioFadeIn.FadeIn(GetComponent<AudioSource>(), 3f));
        goToMainMenu.onClick.AddListener(GoToMainMenu);

        //Call Game End
        GameManager.instance.GameEnd();

		if (Player.instance.playerProgress.goToPhobos) {
			myText.text = "Attempts were made by the USSR to send probes to Phobos in 1988, and again in 2011. All attempts were classified as failures.\n\nYou set your course for Phobos.\n\n\n\nThe End";
		} else {
			myText.text = "Attempts were made by the USSR to send probes to Phobos in 1988, and again in 2011. All attempts were classified as failures.\n\nYou set your course for Mars to continue your mission to terraform Mars.\n\n\n\nThe End";
		}
    }




    public void GoToMainMenu()
    {
        //Note: If you want to add a fade. Make sure you add the fader prefab in your scene. 
        //and fill the needed vars for NextSceneManager.instance.LoadLevelScene() using the fader GO ref.
        NextSceneManager.instance.LoadLevelScene("MainMenu");

    }
}
