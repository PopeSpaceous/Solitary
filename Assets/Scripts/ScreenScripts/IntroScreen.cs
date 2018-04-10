using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : MonoBehaviour {


    public Button goToButtonHub;

    public GameObject fade;
	// Use this for initialization
	void Start () {
		
        goToButtonHub.onClick.AddListener(GoToHub);
    }
	

    public void GoToHub() {
        //Note: If you want to add a fade. Make sure you add the fader prefab in your scene. 
        //and fill the needed vars for NextSceneManager.instance.LoadLevelScene() using the fader GO ref.
        NextSceneManager.instance.LoadLevelScene("Hub", fade.GetComponent<Animator>(), fade.GetComponent<Image>());

    }
}
