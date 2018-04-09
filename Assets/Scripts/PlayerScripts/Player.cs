using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Player will always be static throughout the game and forced to not be unloaded when a new scene is loaded */
public class Player : MonoBehaviour {

	//make this instance static so it can be used across scripts
	public static Player instance = null;

	//Used for locking the player movement
	private bool canPlayerMove = true;

    //Used to take priority which movement lock can change the canPlayerMove value. 
    //This is so it can help prevent race conditions to which lock change can change the playermovement.
    //The script call of lock change will take priority 
    private bool plocked = false; 
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
    [HideInInspector]
    public bool escapeInput = false;

    public PlayerProgress playerProgress;

    //Audio Sources
    AudioSource[] audControls;
    //Audio Clips
    public AudioClip jog_LandSFX;
    public AudioClip jog_SwitchSFX;
    public AudioClip jumpStartSFX;
    public AudioClip jumpLandSFX;

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

        audControls = GetComponents<AudioSource>();

    }

	// This Update function will be use to check user Input
	void Update(){
		//Check if the player can move
		if (canPlayerMove) {
			
			CheckMovementInput ();

		} else {
			
			actionButtion = false;
			jumpInput = false;
			horizontalInput = 0;

		}
        //check for eascape button input
        escapeInput = Input.GetButtonDown("Cancel");
    }

	void CheckMovementInput(){
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
        if (!plocked)
            canPlayerMove = (animationEvent.intParameter == 0) ? true : false;
    }
    //Set the movement lock done by a script call
    public void ChangeMovementLock(bool set) {
        canPlayerMove = set;
        plocked = !set;
    }

    //Audio Event Methods

    //Play jog_land audio clip, with sound balancing 
    public void PlayJogLand() {

        if (audControls[0].isPlaying)
        {
           audControls[1].clip = jog_LandSFX;
           audControls[1].Play();
        }
        else {
           audControls[0].clip = jog_LandSFX;
           audControls[0].Play();
        }
    }
    //Play jog_switch audio clip, with sound balancing 
    public void PlayJogSwitch()
    {

        if (audControls[0].isPlaying)
        {
            audControls[1].clip = jog_SwitchSFX;
            audControls[1].Play();
        }
        else
        {
            audControls[0].clip = jog_SwitchSFX;
            audControls[0].Play();
        }
    }
    //Stop any current sounds, and play jump start sound
    public void JumpStartSFX() {
        audControls[0].Stop();
        audControls[0].clip = jumpStartSFX;
        audControls[0].Play();

    }
    //Stop any current sounds, and play hard landing sound
    public void JumpHardLandFX()
    {
        audControls[0].Stop();
        audControls[0].clip = jumpLandSFX;
        audControls[0].Play();
    }

}
