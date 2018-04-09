using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : MonoBehaviour {

    //For Development only
    //TODO: remove during deployment, or keep it if there is a need
    public Button goToButtonHub;

	// Use this for initialization
	void Start () {
		
        goToButtonHub.onClick.AddListener(GoToHub);
    }
	

    public void GoToHub() {
        //Note: If you want to add a fade. Make sure you add the fader prefab in your scene. 
        //and fill the needed vars for NextSceneManager.instance.LoadLevelScene() using the fader GO ref.
        NextSceneManager.instance.LoadLevelScene("Hub");

    }
}
