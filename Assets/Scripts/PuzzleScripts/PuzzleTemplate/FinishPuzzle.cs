// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * Deubgging use only for calling a puzzle completion call
*/
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
