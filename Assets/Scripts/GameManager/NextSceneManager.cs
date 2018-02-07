using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		if (!isPuzzleLoaded) 
		{
			isPuzzleLoaded = true;
			//Load Scene on top of the existing one
			SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);

		}
	}

	public void UnloadPuzzleScene(string sceneName){
		isPuzzleLoaded = false;
		SceneManager.UnloadSceneAsync (sceneName);
	}

}
