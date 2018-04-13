// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Jacob Holland
// Date: 04/13/2018
/* Summary: 
 *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour {
    public int row, col;

    GameController gameMN;

    // Use this for initialization
    void Start () {
        GameObject gamemanager = GameObject.Find ("GameController");
        gameMN = gamemanager.GetComponent<GameController> ();
    }

    // Update is called once per frame
    void Update () {

    }

    void OnMouseDown () {
        gameMN.countStep += 1;
        gameMN.row = row;
        gameMN.col = col;
        gameMN.startControl = true;
    }
}
