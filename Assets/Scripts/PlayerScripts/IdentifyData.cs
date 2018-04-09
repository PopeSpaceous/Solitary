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
