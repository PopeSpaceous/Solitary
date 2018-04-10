using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/* PuzzlePlaceholder */
public class PuzzlePlaceholder : MonoBehaviour {

	//difficultyNeed will be used to influence the PuzzlerRanomization difficulty selection. 
	//PuzzlerRanomization will ultimately decide on the puzzle's difficulty
	//For Debugging use, difficultyNeed will be the actual difficulty for the puzzle.
	public int difficultyNeed = 1; 

	public WorldObject worldObject = null;

    //Will flip the lock calls for a worldobject
    public bool flipLockState = false;

    public bool isLastPuzzle = false; // If it is last a level completion call will be done
    [HideInInspector]
    public bool isPuzzleComplete = false;
    //use for setting the appropriate set when a puzzle is completed
    [HideInInspector]
    public Level level; //Must be set by PuzzleRandomization class
    [HideInInspector]
    public Sprite puzzleSprite = null; //Must be set by PuzzleRandomization class
    //[HideInInspector]
    public int difficulty = 1;//The actual puzzle difficulty //Must be set by PuzzleRandomization class    
    [HideInInspector]
    public RuntimeAnimatorController aniControl; // must be set by puzzleRandomization
    //To what puzzle this placeholder must go to
    //[HideInInspector]
	public string puzzleGoTo= "PuzzleTemplate"; //Must be set by PuzzleRandomization class

    private Animator puzzleAni;

    private void Start()
    {
        
        //Set the puzzle sprite its given to
        if (puzzleSprite != null)
            GetComponent<SpriteRenderer>().sprite = puzzleSprite;
        //Lock or unlock the world object
        if (worldObject != null)
        {
            if (flipLockState)
            {
                worldObject.Unlock();
            }
            else {
                worldObject.Lock();
            }
        }
        //Add a animator controller if there is one
        if (aniControl != null) {
            puzzleAni = GetComponent<Animator>();
            puzzleAni.runtimeAnimatorController = aniControl;
        }
        //Debugging only
        if (isPuzzleComplete) {
            PuzzleExit(true);
        }

        //difficulty randomization
        int diffpercent = 0;
        if (difficultyNeed == 1)
        {
            diffpercent = Random.Range(0, 101);
            if (diffpercent <= 70)
            {
                difficulty = 1;
            }
            else
            {
                difficulty = 2;
            }
        }
        if (difficultyNeed == 2)
        {
            diffpercent = Random.Range(0, 101);
            if (diffpercent <= 70)
            {
                difficulty = 2;
            }
            else
            {
                difficulty = 3;
            }
        }
        if (difficultyNeed == 3)
        {
            diffpercent = Random.Range(0, 101);
            if (diffpercent <= 70)
            {
                difficulty = 3;
            }
            else
            {
                difficulty = 2;
            }
        }
    }

    void OnTriggerStay2D( Collider2D col)
	{
		if(col.gameObject.CompareTag("Player") && Player.instance.actionButtion && !Player.instance.isInPuzzle && !isPuzzleComplete)
        {
            //set the bool that the player is in a puzzle
            Player.instance.isInPuzzle = true;
            //Give the placeholder ref to player. So when animation is ready to start the puzzle it will call GoToPuzzle()
            Player.instance.puzzle = this;
            //start animation state for starting a puzzle
            Player.instance.animstate.SetBool("IsInPuzzle", Player.instance.isInPuzzle);

        }

	}
    //Load the puzzle scene
    public void GoToPuzzle() {
        //Place the difficulty values in a NextSceneManager var. So when the loaded puzzle scene
        //is loaded in its Awake() function it will get the NextSceneManager var and set that difficulity 
        NextSceneManager.instance.setPuzzledifficulty = difficulty;
        //pass this current placeholder instance for actions changes when the puzzle is complete
        NextSceneManager.instance.placeholder = this;

        //Load Puzzle Scene on top the current scene
        NextSceneManager.instance.LoadPuzzleScene(puzzleGoTo);
    }

    //Unload the puzzle scene and do the appropriate calls and sets. When a puzzle has exited
    public void PuzzleExit(bool isCompleted)
    {
        
        //set the bool that the player is not in a puzzle
        Player.instance.isInPuzzle = false;
        Player.instance.puzzle = null;
        Player.instance.animstate.SetBool("IsInPuzzle", false);
        
        if (isCompleted)
        {
            isPuzzleComplete = true;
            level.PuzzleUpdateScore(difficulty);

            if (worldObject != null)
            {
                //unlock or lock the world Object
                if (flipLockState)
                {
                    worldObject.Lock();
                }
                else {
                    worldObject.Unlock();
                } 
            }
            if (isLastPuzzle) {
                //level call completion call
                level.LevelComplete();
            }
        }
        //Unload Puzzle scene
        NextSceneManager.instance.UnloadPuzzleScene(puzzleGoTo);
    }
		
}
