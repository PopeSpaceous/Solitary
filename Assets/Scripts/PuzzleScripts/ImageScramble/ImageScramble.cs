using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageScramble: Puzzle {

    public int level;
    public int row, col, countStep;
    public int sizeRow, sizeCol;
    public int rowBlank, colBlank;  //position of blank tile
    int countPoint = 0;
    int countImageKey = 0;

    public bool startControl = false;
    public bool checkComplete;

    GameObject temp;

    public List<GameObject> imageKeyList;   //run from 0 -> list.count
    public List<GameObject> imageOfPictureList;
    public List<GameObject> checkPointList;

    GameObject[,] imageKeyMatrix;
    GameObject[,] imageOfPictureMatrix;
    GameObject[,] checkPointMatrix;

    // Sets the parent fields
    void Awake () {
		puzzleName = "ImageScramble";
		difficulty = NextSceneManager.instance.setPuzzledifficulty;
		placeholder = NextSceneManager.instance.placeholder;
		Debug.Log ("Difficulty for puzzle " + puzzleName + " is: "+ this.difficulty);
	}


    /* Your wonderful startup puzzle code here :3 */
    void Start () {
        imageKeyMatrix = new GameObject [sizeRow, sizeCol];
        imageOfPictureMatrix = new GameObject [sizeRow, sizeCol];
        checkPointMatrix = new GameObject [sizeRow, sizeCol];

        if (level == 1) {
            ImageOfEasyLevel ();
        }
        else if (level == 2) {
            ImageOfMediumLevel ();
        }
        else if (level == 3) {
            ImageOfHardLevel ();
        }

        CheckPointManager ();
        ImageKeyManager ();

        for (int r = 0; r < sizeRow; r++) {  //run row
            for (int c = 0; c < sizeCol; c++) { //run col
               if(imageOfPictureMatrix [r, c].name.CompareTo("C3") == 0) {
                    rowBlank = r;
                    colBlank = c;
                    break;
                }
            }
        }
    }

    void Update () {
        if (startControl) { //move for image of picture
            startControl = false;
            if(countStep == 1) {
                if (imageOfPictureMatrix [row, col] != null && imageOfPictureMatrix [row, col].name.CompareTo ("C3") != 0) { //check if position is image not image blank
                    if(rowBlank != row && colBlank == col) {
                        if (Mathf.Abs (row - rowBlank) == 1) {
                            //move
                            SortImage ();   //call SortImage
                            countStep = 0;  //reset countstep
                        } else {
                            countStep = 0;  //reset countstep
                        }
                    }
                    else if(rowBlank == row && colBlank == col) {
                        if (Mathf.Abs (col - colBlank) == 1) {
                            //move
                            SortImage ();   //call SortImage
                            countStep = 0;
                        } else {
                            countStep = 0;
                        }
                    }
                    else if ((rowBlank == row && colBlank == col) || rowBlank != row && colBlank != col) {
                        //not move
                        countStep = 0;
                    }
                } else {
                    //not move
                    countStep = 0;
                }
            }
        }
    }

    void SortImage () {
        //sort
        temp = imageOfPictureMatrix [rowBlank, colBlank];   //select image blank and save at temp variable
        imageOfPictureMatrix [rowBlank, colBlank] = null;

        imageOfPictureMatrix [rowBlank, colBlank] = imageOfPictureMatrix [row, col];    //image is not blank and save at new position
        imageOfPictureMatrix [row, col] = null;

        imageOfPictureMatrix [row, col] = temp;

        //move image
        imageOfPictureMatrix [rowBlank, colBlank].GetComponent<ImageController> ().target = checkPointMatrix [rowBlank, colBlank]; //new point for image blank
        imageOfPictureMatrix [row, col].GetComponent<ImageController> ().target = checkPointMatrix [row, col];

        imageOfPictureMatrix [rowBlank, colBlank].GetComponent<ImageController> ().startMove = true;
        imageOfPictureMatrix [row, col].GetComponent<ImageController> ().startMove = true;

        //set row and col
        rowBlank = row;
        colBlank = col;
    }

    void CheckPointManager () {
        for (int r = 0; r < sizeRow; r++) {  //run row
            for (int c = 0; c < sizeCol; c++) { //run col
                checkPointMatrix [r, c] = checkPointList [countPoint];
                countPoint++;
            }
        }
    }

    void ImageKeyManager () {
        for (int r = 0; r < sizeRow; r++) {  //run row
            for (int c = 0; c < sizeCol; c++) { //run col
                imageKeyMatrix [r, c] = imageKeyList [countImageKey];
                countImageKey++;
            }
        }
    }

    void ImageOfEasyLevel () {
        //row 1
        imageOfPictureMatrix [0, 0] = imageOfPictureList [0];   //A1
        imageOfPictureMatrix [0, 1] = imageOfPictureList [1];   //A2
        imageOfPictureMatrix [0, 2] = imageOfPictureList [2];   //A3

        //row 2
        imageOfPictureMatrix [1, 0] = imageOfPictureList [3];   //B1
        imageOfPictureMatrix [1, 1] = imageOfPictureList [4];   //B2
        imageOfPictureMatrix [1, 2] = imageOfPictureList [5];   //B3

        //row 3
        imageOfPictureMatrix [2, 0] = imageOfPictureList [6];   //C1
        imageOfPictureMatrix [2, 1] = imageOfPictureList [7];   //C2
        imageOfPictureMatrix [2, 2] = imageOfPictureList [8];   //C3 Image Blank
    }

    void ImageOfMediumLevel () {

    }

    void ImageOfHardLevel () {

    }

}
