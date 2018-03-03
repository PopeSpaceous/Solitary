using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScrambleMovement : MonoBehaviour {

    public int row, col;
    ImageScramble gameMN;

    // Use this for initialization
    void Start () {
        GameObject gamemanager = GameObject.Find ("ImageScramble");
        gameMN = gamemanager.GetComponent<ImageScramble> ();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown () {
        Debug.Log ("Row is : " + row + " Col is: " + col); // test
        gameMN.countStep += 1;
        gameMN.row = row;
        gameMN.col = col;
        gameMN.startControl = true;
    }
}
