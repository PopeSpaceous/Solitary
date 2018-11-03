// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * For loading in a level scene, or a puzzle scene.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneManager : MonoBehaviour {
	
	public static NextSceneManager instance = null;

	//These two vars are for giving information to the current puzzle scene
	public int setPuzzledifficulty = 0;
	public PuzzlePlaceholder placeholder;

	public bool isPuzzleLoaded = false;

	void Awake () {

		//Set the instance only once.
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//Enforces that there will always be one instance of a gameObject. This is for type errors prevention
			Destroy (gameObject);
			Debug.LogWarning ("Another instance of NextSceneManager have been created and destoryed!");
		}

	}
	//Load / unloads a level scene with fade or not
	public void LoadLevelScene (string sceneName, Animator fader = null, Image im = null)
	{
		if (!SceneManager.GetSceneByName (sceneName).isLoaded) 
		{
            // will allow a fade if the needed vars are not null
            if (im != null && fader != null) 
            {
                if (!fader.GetBool("Fade"))  // Really make sure we don't keep calling the coroutine while the scene is still the fade transition.
                {
                    //Fade to next scene
                    StartCoroutine(FadeToNextScene(fader, im, sceneName));
                }
            }
            else
            {
                //Load new Scene
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
		}
	}
	//Load in a puzzle scene
	public void LoadPuzzleScene (string sceneName)
	{
		if (!isPuzzleLoaded) 
		{
			isPuzzleLoaded = true;
			//Load Scene on top of the existing one
			SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);

		}
	}
    //unload a puzzle scene
	public void UnloadPuzzleScene(string sceneName){
		isPuzzleLoaded = false;
		SceneManager.UnloadSceneAsync (sceneName);
	}

    //Will start a wait for fade transition. It will wait once the fade had fully faded to load in the nextr scene
    IEnumerator FadeToNextScene(Animator f, Image i, string sceneName)
    {
        f.SetBool("Fade", true);
        // wait until fade has ended
        yield return new WaitUntil(() => i.color.a >= 1); 
        //Load next scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    }

}
