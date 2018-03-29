using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishPuzzle : MonoBehaviour {

    public PuzzleTemplate tem;
    public Button buttion;

	// Use this for initialization
	void Start () {
        buttion.GetComponent<Button>().onClick.AddListener(tem.FinishPuzzle);
    }
	

}
