using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Player will always be static throughout the game and forced to not be unloaded when a new scene is loaded */
public class Player : MonoBehaviour {

	//make this instance static so it can be used across scripts
	public static Player instance = null;

	//Used for locking the player movement
	private bool canPlayerMove = true;

    //bool when or not the player is in a puzzle
    public bool isInPuzzle = false;
    //Animator 
    [HideInInspector]
    public Animator animstate;
    //this will be the current ref has to what placeholder the player is in
    [HideInInspector]
    public PuzzlePlaceholder puzzle = null;

    [HideInInspector] 
	public bool actionButtion = false;
	[HideInInspector] 
	public bool jumpInput = false;
	[HideInInspector] 
	public float horizontalInput;


	void Awake () {
		
		//Set the instance only once.
		if (instance == null) {			
			instance = this;
		} else if (instance != this) {
			//Enforces that there will always be one instance of a gameObject. This is for type errors prevention
			Destroy (gameObject);
			Debug.LogWarning ("Another instance of Player have been created and destoryed!");
		}
			
		//Makes the gameobject not be unloaded when entering a new scene
		DontDestroyOnLoad (this);

        animstate = GetComponent<Animator>();
    }

	// This Update function will be use to check user Input
	void Update(){
		//Check if the player can move
		if (canPlayerMove) {
			
			CheckInput ();

		} else {
			
			actionButtion = false;
			jumpInput = false;
			horizontalInput = 0;

		}
	}

	void CheckInput(){
		//Action buttion will be used for any interactions
		actionButtion = Input.GetButton ("Action");

		//Checks if the user pressed 'a' or 'b' keys based on InputManager
		horizontalInput = Input.GetAxis ("Horizontal");

		//Checks if the user pressed the Jumping Key based on InputManager
		jumpInput = Input.GetButton("Jump");
	
	}

    //GoInPuzzle will be called by an animation event call
    void GoInPuzzle() {
        puzzle.GoToPuzzle();
    }

    //Set the movement lock done by a animation
    public void ChangeMovementLock(AnimationEvent animationEvent = null) {
        canPlayerMove = (animationEvent.intParameter == 0) ? true : false;
    }
    //Set the movement lock done by a script call
    public void ChangeMovementLock(bool set) {
        canPlayerMove = set;
    }

}
