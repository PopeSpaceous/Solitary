using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneManager : MonoBehaviour {
	
	public static NextSceneManager instance = null;

	void Awake () {

		//Set the instance only once.
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//Enforces that there will always be one instance of a gameObject. This is for type errors prevention
			Destroy (gameObject);
			Debug.Log ("Ran");
		}

	}
	
	public void LoadLevelScene (string sceneName)
	{
		if (!SceneManager.GetSceneByName (sceneName).isLoaded) 
		{
			//Load new Scene
			SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
		}
	}
		
	public void LoadPuzzleScene (string sceneName)
	{
		if (!SceneManager.GetSceneByName (sceneName).isLoaded) 
		{
			//Load Scene on top of the existing one
			SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);

		}
	}
}
