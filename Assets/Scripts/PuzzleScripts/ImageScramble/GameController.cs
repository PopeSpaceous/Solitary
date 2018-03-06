using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    int countPoint = 0;
    int countImageKey = 0;

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

    void Start () {
        imageKey = new GameObject [sizeRow, sizeCol];
        tiles = new GameObject [sizeRow, sizeCol];
        checkPoints = new GameObject [sizeRow, sizeCol];

        if(level == 1) {
            EasyLevel ();
        }

        CheckPointManager ();
        ImageKeyManager ();

        for (int r = 0; r < sizeRow; r++) {
            for (int c = 0; c < sizeCol; c++) {
                if (tiles [r, c].name.CompareTo ("Blank") == 0) {
                    rowBlank = r;
                    colBlank = c;
                    break;
                }
            }
        }

    }

    void CheckPointManager () {
        for (int r = 0; r < sizeRow; r++) {
            for (int c = 0; c < sizeCol; c++) {
                checkPoints [r, c] = checkPointsList [countPoint];
                countPoint++;
            }
        }
    }

    void ImageKeyManager () {
        for (int r = 0; r < sizeRow; r++) {
            for (int c = 0; c < sizeCol; c++) {
                imageKey [r, c] = imageKeyList [countImageKey];
                countImageKey++;
            }
        }
    }

    void Update () {
        if (startControl) {
            startControl = false;
            if (countStep == 1) {
                if (tiles [row, col] != null && tiles [row, col].tag != "Blank") {
                    if ((rowBlank != row && colBlank == col)) {
                        //move
                        if (Mathf.Abs(row - rowBlank) == 1) {
                            SortImage ();
                            countStep = 0;
                        } else {
                            countStep = 0;
                        }
                    } else if (rowBlank == row && colBlank != col) {
                        //move
                        if (Mathf.Abs(col - colBlank) == 1) {
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
    }

    void SortImage () {
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

    void EasyLevel () {
        tiles [0, 0] = tilesList [0];   //A1
        tiles [0, 1] = tilesList [1];   //A2
        tiles [0, 2] = tilesList [2];   //A3

        //row 2
        tiles [1, 0] = tilesList [3];   //B1
        tiles [1, 1] = tilesList [4];   //B2
        tiles [1, 2] = tilesList [5];   //B3

        //row 3
        tiles [2, 0] = tilesList [6];   //C1
        tiles [2, 1] = tilesList [7];   //C2
        tiles [2, 2] = tilesList [8];   //Blank
    }
}
