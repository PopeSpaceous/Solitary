using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour {

    public GameObject target;
    public bool startMove = false;
    ImageScramble gameMN;

	// Use this for initialization
	void Start () {
        GameObject gamemanager = GameObject.Find ("ImageScramble");
        gameMN = gamemanager.GetComponent<ImageScramble> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (startMove) {
            startMove = false;
            this.transform.position = target.transform.position; // move to new position
            gameMN.checkComplete = true;
        }
	}
}
