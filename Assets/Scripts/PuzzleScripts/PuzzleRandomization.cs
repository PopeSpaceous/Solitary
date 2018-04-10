using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PuzzleRandomization : MonoBehaviour {

    public Level level; //ref of level, will be set in the inspecter

    //These will a list of the puzzle scene names
    public string[] listOfPuzzles; // Testing Note: populate this array in the inspecter, and just add one if u wanna test your own
    public PuzzlePlaceholder[] placeholders; // Note: make sure you add a placeholders in the inspecter as well    

    //These are sequential arrays. The order of these arrays are important!
    public Sprite[] sprites_P;
    public RuntimeAnimatorController[] animatorConrollers;


    //a key value pair, used for finding the right needed refs for a puzzleplaceholder
    private Dictionary<string, PlaceholderRefs> refDictionary;

    //This struct will be used to warp the needed unique refs for each puzzle placeholder
    struct PlaceholderRefs {
        public Sprite sprite;
        public RuntimeAnimatorController aniControl;

        public PlaceholderRefs (Sprite s, RuntimeAnimatorController c) {
            sprite = s;
            aniControl = c;
        }
    }

    void Awake () {
        puzzleRandomization ();
        //populate Dictionary. The order of the refs need to match the key name
        refDictionary = new Dictionary<string, PlaceholderRefs> ();
        refDictionary.Add ("Anagram", new PlaceholderRefs (sprites_P [0], null));
        refDictionary.Add ("Crytogram", new PlaceholderRefs (sprites_P [1], null)); // Spelling error, the scene name must be change first to fix this
        refDictionary.Add ("WireConnection", new PlaceholderRefs (sprites_P [2], animatorConrollers [2]));
        //TODO: Add more to dictionary when more sprites are made

        if (placeholders != null)
            DebugPlacePuzzles ();
    }

    //TODO: Complete PuzzleRandomization
    void puzzleRandomization () {
        listOfPuzzles = new string [placeholders.Length];
        int rnd = 0;
        for (int i = 0; i < placeholders.Length; i++) {
            //change the range from 0 -> however many puzzles, 5 at the moment
            rnd = UnityEngine.Random.Range (0, 6);
            if (rnd == 0) {
                listOfPuzzles [i] = "Anagram";
            }
            if (rnd == 1) {
                listOfPuzzles [i] = "Crytogram";
            }
            if (rnd == 2) {
                listOfPuzzles [i] = "ImageScramble";
            }
            if (rnd == 3) {
                listOfPuzzles [i] = "Tangrams";
            }
            if (rnd == 4) {
                listOfPuzzles [i] = "WireConnection";
            }
            if (rnd == 5) {
                listOfPuzzles [i] = "WordPasscode";
            }
            //uncomment and change randomization range finished
            /*if (rnd == 6) {
                listOfPuzzles [i] = "SimonSays";
            }*/
        }
    }

    //Debugging Use Only
    //Will place puzzles in order
    void DebugPlacePuzzles () {
        int puzzleCounter = 0;
        for (int counter = 0; counter < placeholders.Length; counter++) {


            placeholders [counter].puzzleGoTo = listOfPuzzles [puzzleCounter];
            //Set refs
            if (refDictionary.ContainsKey (listOfPuzzles [puzzleCounter])) { //TODO: Maybe hash the keys and listOfPuzzles and compare the hashes
                placeholders [counter].puzzleSprite = refDictionary [listOfPuzzles [puzzleCounter]].sprite; // set sprite
                placeholders [counter].aniControl = refDictionary [listOfPuzzles [puzzleCounter]].aniControl; // set animation controller
            }
            //Set level ref
            placeholders [counter].level = this.level;

            puzzleCounter++;
            //loop back from the start of the list
            if (puzzleCounter == listOfPuzzles.Length) {
                puzzleCounter = 0;
            }

        }


    }
}
