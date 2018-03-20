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

	public WorldObject wObject = null;

    //use for setting the appropriate set when a puzzle is completed
    [HideInInspector]
    public Level level; //Must be set by PuzzleRandomization class
    [HideInInspector]
    public Sprite puzzleSprite = null; //Must be set by PuzzleRandomization class

    //To what puzzle this placeholder must go to
    //[HideInInspector]
	public string puzzleGoTo= "PuzzleTemplate"; //Must be set by PuzzleRandomization class

    private void Start()
    {

        //Set the puzzle sprite its given to
        if (puzzleSprite != null)
            GetComponent<SpriteRenderer>().sprite = puzzleSprite;
        //Lock the world object
        if(wObject != null)
            wObject.Lock();
    }

    void OnTriggerStay2D( Collider2D col)
	{
		if(col.gameObject.CompareTag("Player") && Player.instance.actionButtion && !Player.instance.isInPuzzle)
        {
			//Lock the player's movement
			Player.instance.ChangeMovementLock(false);
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
        NextSceneManager.instance.setPuzzledifficulty = difficultyNeed;
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
        
        //unLocking the player's movement will be handled by the animator 

        if (isCompleted)
        {
            //TODO: add the other updates it needs to do when the puzzle is completed

               //Door unlock
            if (wObject != null)
            {
                //unlock the world Object
               wObject.Unlock();
            }
        }
        //Unload Puzzle scene
        NextSceneManager.instance.UnloadPuzzleScene(puzzleGoTo);
    }
		
}
