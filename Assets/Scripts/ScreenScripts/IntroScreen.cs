// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara, Nathan Misener
// Date: 04/13/2018
/* Summary: 
 * Small triggers for viewing text in the IntroScene, and loading the hub scene
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : MonoBehaviour {

    //button ref to go to the control view, and then hub scene
    public Button goToButtonHub;
    //Views refs
    public GameObject controlsView;
    public GameObject introText;
    //Fader object
    public GameObject fade;
	
	void Start () {
		//Set ref
        goToButtonHub.onClick.AddListener(ShowControls);
    }
    //Turns on the controls view
    void ShowControls() {
        controlsView.SetActive(true);
        introText.SetActive(false);
        goToButtonHub.onClick.AddListener(GoToHub);
    }
    //Triggers a load scene to the hub, with fading
    public void GoToHub() {
        //Note: If you want to add a fade. Make sure you add the fader prefab in your scene. 
        //and fill the needed vars for NextSceneManager.instance.LoadLevelScene() using the fader GO ref.
        NextSceneManager.instance.LoadLevelScene("Hub", fade.GetComponent<Animator>(), fade.GetComponent<Image>());

    }
}
