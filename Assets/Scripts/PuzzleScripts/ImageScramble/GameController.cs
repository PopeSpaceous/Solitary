using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    int countPoint = 0;
    int countImageKey = 0;
    int numberOfPuzzles = 2;
    int PuzzleRandom = 0;
    int countCorrect = 0;

    bool win = false;
    
    public int level;
    public int row, col, countStep;
    public int sizeRow, sizeCol;
    public int rowBlank, colBlank;

    public bool startControl;
    public bool checkComplete;

    GameObject temp;

    public List<GameObject> imageKeyList;
    public List<GameObject> tilesList;
    public List<GameObject> checkPointsList;

    GameObject[,] imageKey;
    GameObject[,] tiles;
    GameObject[,] checkPoints;

    public Sprite[] planetOne3x3;
    public Sprite[] planetTwo3x3;
    public Sprite[] planetOne4x4;
    public Sprite[] planetTwo4x4;
    public Sprite[] planetOne5x5;
    public Sprite[] planetTwo5x5;

    void Start () {
        imageKey = new GameObject [sizeRow, sizeCol];
        tiles = new GameObject [sizeRow, sizeCol];
        checkPoints = new GameObject [sizeRow, sizeCol];

        //Chooses easy puzzle
        if (level == 1) {
            sizeRow = 3;
            sizeCol = 3;
            PuzzleRandom = Random.Range (1, numberOfPuzzles + 1);
            if (PuzzleRandom == 1) {
                LoadPlanetOne3x3 ();
            } else if (PuzzleRandom == 2) {
                LoadPlanetTwo3x3 ();
            }
            EasyLevel ();
        }

        //Chooses medium puzzle
        if (level == 2) {
            sizeRow = 4;
            sizeCol = 4;
            PuzzleRandom = Random.Range (1, numberOfPuzzles + 1);
            if (PuzzleRandom == 1) {
                LoadPlanetOne4x4 ();
            } else if (PuzzleRandom == 2) {
                LoadPlanetTwo4x4 ();
            }
            MediumLevel ();
        }

        //Chooses hard puzzle
        if (level == 3) {
            sizeRow = 5;
            sizeCol = 5;
            PuzzleRandom = Random.Range (1, numberOfPuzzles + 1);
            if (PuzzleRandom == 1) {
                LoadPlanetOne5x5 ();
            } else if (PuzzleRandom == 2) {
                LoadPlanetTwo5x5 ();
            }
            HardLevel ();
        }

        CheckPointManager ();
        ImageKeyManager ();

        //Sets the location of the blank time
        for (int r = 0; r < sizeRow; r++) {
            for (int c = 0; c < sizeCol; c++) {
                if (tiles [r, c].CompareTag("Blank")) {
                    rowBlank = r;
                    colBlank = c;
                    break;
                }
            }
        }

        //Shuffles the puzzle
        Shuffle ();

    }

    void Update () {
        //Determines if a piece can move and then moves it
        if (startControl) {
            startControl = false;
            if (countStep == 1) {
                if ((rowBlank != row && colBlank == col)) {
                    //move
                    if (Mathf.Abs (row - rowBlank) == 1) {
                        SortImage ();
                        countStep = 0;
                    } else {
                        countStep = 0;
                    }
                } else if (rowBlank == row && colBlank != col) {
                    //move
                    if (Mathf.Abs (col - colBlank) == 1) {
                        SortImage ();
                        countStep = 0;
                    } else {
                        countStep = 0;
                    }
                } else if ((rowBlank == row && colBlank == col) || (rowBlank != row && colBlank != col)) {
                    //don't move
                    countStep = 0;
                } else {
                    //don't move
                    countStep = 0;
                }
            }
        }
    }

    void FixedUpdate () {
        //checks if all the pieces are in the right order
        if(checkComplete) {
            checkComplete = false;
            //cycles through puzzle and compares it to the key
            for(int r = 0; r < sizeRow; r++) {
                for(int c = 0; c < sizeCol; c++) {
                    if(imageKey[r,c].gameObject.name.CompareTo(tiles[r,c].gameObject.name) == 0) {
                        countCorrect++;
                    }
                }
            }
            if(countCorrect == sizeRow * sizeCol) { //if all 9 images match the key then the puzzle is complete
                win = true;
            } else {
                countCorrect = 0;
            }
        }
    }

    //Sets the checkpoints from the list
    void CheckPointManager () {
        for (int r = 0; r < sizeRow; r++) {
            for (int c = 0; c < sizeCol; c++) {
                checkPoints [r, c] = checkPointsList [countPoint];
                countPoint++;
            }
        }
    }

    //Sets the image key from the list
    void ImageKeyManager () {
        for (int r = 0; r < sizeRow; r++) {
            for (int c = 0; c < sizeCol; c++) {
                imageKey [r, c] = imageKeyList [countImageKey];
                countImageKey++;
            }
        }
    }

    //sorts the image when a tile is clicked
    void SortImage () {
        temp = tiles [rowBlank, colBlank]; // save blank location to temp
        tiles [rowBlank, colBlank] = null; // set blank to null

        tiles [rowBlank, colBlank] = tiles [row, col]; // sets blank to clicked tile
        tiles [row, col] = null; // set selected tile to null
        tiles [row, col] = temp; // sets selected tile to blank location

        //move for image
        tiles [rowBlank, colBlank].GetComponent<ImageController> ().target = checkPoints [rowBlank, colBlank]; //set new point for blank image
        tiles [row, col].GetComponent<ImageController> ().target = checkPoints [row, col]; //set new point for image

        tiles [rowBlank, colBlank].GetComponent<ImageController> ().startMove = true;
        tiles [row, col].GetComponent<ImageController> ().startMove = true;

        //set row and col for image blank
        rowBlank = row;
        colBlank = col;
    }

    //shuffles the puzzle
    void Shuffle () {
        //randomizes puzzle by switching random tiles for 500 iterations
        for (int i = 0; i < 1000; i++) {
            //picks random tiles for shuffling
            row = Random.Range (0, sizeRow);
            col = Random.Range (0, sizeCol);

            //makes sure random tile is next to a blank to make the puzzle solvable
            if ((rowBlank != row && colBlank == col)) {
                if (Mathf.Abs (row - rowBlank) == 1) {
                    temp = tiles [rowBlank, colBlank]; // save blank location to temp
                    tiles [rowBlank, colBlank] = null; // set blank to null

                    tiles [rowBlank, colBlank] = tiles [row, col]; // sets blank to clicked tile
                    tiles [row, col] = null; // set selected tile to null
                    tiles [row, col] = temp; // sets selected tile to blank location

                    //move for image
                    tiles [rowBlank, colBlank].GetComponent<ImageController> ().target = checkPoints [rowBlank, colBlank]; //set new point for blank
                    tiles [row, col].GetComponent<ImageController> ().target = checkPoints [row, col];

                    tiles [rowBlank, colBlank].GetComponent<ImageController> ().startMove = true;
                    tiles [row, col].GetComponent<ImageController> ().startMove = true;

                    //set row and col for image blank
                    rowBlank = row;
                    colBlank = col;
                }
            } 
            else if ((rowBlank == row && colBlank != col)) {
                if (Mathf.Abs (col - colBlank) == 1) {
                    temp = tiles [rowBlank, colBlank]; // save blank location to temp
                    tiles [rowBlank, colBlank] = null; // set blank to null

                    tiles [rowBlank, colBlank] = tiles [row, col]; // sets blank to clicked tile
                    tiles [row, col] = null; // set selected tile to null
                    tiles [row, col] = temp; // sets selected tile to blank location

                    //move for image
                    tiles [rowBlank, colBlank].GetComponent<ImageController> ().target = checkPoints [rowBlank, colBlank]; //set new point for blank
                    tiles [row, col].GetComponent<ImageController> ().target = checkPoints [row, col];

                    tiles [rowBlank, colBlank].GetComponent<ImageController> ().startMove = true;
                    tiles [row, col].GetComponent<ImageController> ().startMove = true;

                    //set row and col for image blank
                    rowBlank = row;
                    colBlank = col;
                }
            }
        }
    }

    //Loads in easy puzzle from list
    void EasyLevel () {
        //Row 1
        tiles [0, 0] = tilesList [0];   //A1
        tiles [0, 1] = tilesList [1];   //A2
        tiles [0, 2] = tilesList [2];   //A3

        //Row 2
        tiles [1, 0] = tilesList [3];   //B1
        tiles [1, 1] = tilesList [4];   //B2
        tiles [1, 2] = tilesList [5];   //B3

        //Row 3
        tiles [2, 0] = tilesList [6];   //C1
        tiles [2, 1] = tilesList [7];   //C2
        tiles [2, 2] = tilesList [8];   //Blank
    }

    //Loads in a medium puzzle from list
    void MediumLevel () {
        //Row 1
        tiles [0, 0] = tilesList [0];   //A1
        tiles [0, 1] = tilesList [1];   //A2
        tiles [0, 2] = tilesList [2];   //A3
        tiles [0, 3] = tilesList [3];   //A4

        //row 2
        tiles [1, 0] = tilesList [4];   //B1
        tiles [1, 1] = tilesList [5];   //B2
        tiles [1, 2] = tilesList [6];   //B3
        tiles [1, 3] = tilesList [7];   //B4

        //row 3
        tiles [2, 0] = tilesList [8];   //C1
        tiles [2, 1] = tilesList [9];   //C2
        tiles [2, 2] = tilesList [10];  //C3
        tiles [2, 3] = tilesList [11];  //C4
        
        //row 4
        tiles [3, 0] = tilesList [12];  //D1
        tiles [3, 1] = tilesList [13];  //D2
        tiles [3, 2] = tilesList [14];  //D3
        tiles [3, 3] = tilesList [15];  //D4
    }

    //Loads in Hard puzzle from list
    void HardLevel () {
        //Row 1
        tiles [0, 0] = tilesList [0];   //A1
        tiles [0, 1] = tilesList [1];   //A2
        tiles [0, 2] = tilesList [2];   //A3
        tiles [0, 3] = tilesList [3];   //A4
        tiles [0, 4] = tilesList [4];   //A5

        //Row 2
        tiles [1, 0] = tilesList [5];   //B1
        tiles [1, 1] = tilesList [6];   //B2
        tiles [1, 2] = tilesList [7];   //B3
        tiles [1, 3] = tilesList [8];   //B4
        tiles [1, 4] = tilesList [9];   //B5

        //Row 3
        tiles [2, 0] = tilesList [10];   //C1
        tiles [2, 1] = tilesList [11];   //C2
        tiles [2, 2] = tilesList [12];  //C3
        tiles [2, 3] = tilesList [13];  //C4
        tiles [2, 4] = tilesList [14];  //C4

        //Row 4
        tiles [3, 0] = tilesList [15];  //D1
        tiles [3, 1] = tilesList [16];  //D2
        tiles [3, 2] = tilesList [17];  //D3
        tiles [3, 3] = tilesList [18];  //D4
        tiles [3, 4] = tilesList [19];  //D4

        //Row 5
        tiles [4, 0] = tilesList [20];  //D1
        tiles [4, 1] = tilesList [21];  //D2
        tiles [4, 2] = tilesList [22];  //D3
        tiles [4, 3] = tilesList [23];  //D4
        tiles [4, 4] = tilesList [24];  //D4
    }

    //Loads in planet 3x3 Planet One Sprites
    void LoadPlanetOne3x3 () {
        for (int i = 0; i <= tilesList.Count - 2; i++) {
            tilesList [i].AddComponent<SpriteRenderer> ();
            tilesList [i].GetComponent<SpriteRenderer> ().sprite = planetOne3x3 [i];
            tilesList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            tilesList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;

            imageKeyList [i].AddComponent<SpriteRenderer> ();
            imageKeyList [i].GetComponent<SpriteRenderer> ().sprite = planetOne3x3 [i];
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;
        }
    }

    //Loads in planet 3x3 Planet Two Sprites
    void LoadPlanetTwo3x3 () {
        for (int i = 0; i <= tilesList.Count - 2; i++) {
            tilesList [i].AddComponent<SpriteRenderer> ();
            tilesList [i].GetComponent<SpriteRenderer> ().sprite = planetTwo3x3 [i];
            tilesList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            tilesList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;

            imageKeyList [i].AddComponent<SpriteRenderer> ();
            imageKeyList [i].GetComponent<SpriteRenderer> ().sprite = planetTwo3x3 [i];
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;
        }
    }

    //Loads in planet 4x4 Planet One Sprites
    void LoadPlanetOne4x4 () {
        for (int i = 0; i <= tilesList.Count - 2; i++) {
            tilesList [i].AddComponent<SpriteRenderer> ();
            tilesList [i].GetComponent<SpriteRenderer> ().sprite = planetOne4x4 [i];
            tilesList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            tilesList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;

            imageKeyList [i].AddComponent<SpriteRenderer> ();
            imageKeyList [i].GetComponent<SpriteRenderer> ().sprite = planetOne4x4 [i];
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;
        }
    }

    //Loads in planet 4x4 Planet Two Sprites
    void LoadPlanetTwo4x4 () {
        for (int i = 0; i <= tilesList.Count - 2; i++) {
            tilesList [i].AddComponent<SpriteRenderer> ();
            tilesList [i].GetComponent<SpriteRenderer> ().sprite = planetTwo4x4 [i];
            tilesList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            tilesList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;

            imageKeyList [i].AddComponent<SpriteRenderer> ();
            imageKeyList [i].GetComponent<SpriteRenderer> ().sprite = planetTwo4x4 [i];
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;
        }
    }

    //Loads in planet 5x5 Planet One Sprites
    void LoadPlanetOne5x5 () {
        for (int i = 0; i <= tilesList.Count - 2; i++) {
            tilesList [i].AddComponent<SpriteRenderer> ();
            tilesList [i].GetComponent<SpriteRenderer> ().sprite = planetOne5x5 [i];
            tilesList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            tilesList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;

            imageKeyList [i].AddComponent<SpriteRenderer> ();
            imageKeyList [i].GetComponent<SpriteRenderer> ().sprite = planetOne5x5 [i];
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;
        }
    }

    //Loads in planet 5x5 Planet Two Sprites
    void LoadPlanetTwo5x5 () {
        for (int i = 0; i <= tilesList.Count - 2; i++) {
            tilesList [i].AddComponent<SpriteRenderer> ();
            tilesList [i].GetComponent<SpriteRenderer> ().sprite = planetTwo5x5 [i];
            tilesList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            tilesList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;

            imageKeyList [i].AddComponent<SpriteRenderer> ();
            imageKeyList [i].GetComponent<SpriteRenderer> ().sprite = planetTwo5x5 [i];
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingLayerName = "Puzzle";
            imageKeyList [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;
        }
    }
}
