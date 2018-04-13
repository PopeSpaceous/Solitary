// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary:
 * Object container for holding the identifcation (high scores , and id) of the player
 * Main use is for the data base
 * This is the class that will be saved and be loaded by SaveLoad
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class IdentifyData {

    public static IdentifyData current;

    //Player id
    public int id = 0 ;
    //Player highscore
    public int highScore = 1;

    //default constructor
    public IdentifyData()
    {

    }
}
