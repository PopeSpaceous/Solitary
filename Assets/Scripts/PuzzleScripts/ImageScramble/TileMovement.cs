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
        Debug.Log (gameMN.countStep);
        gameMN.countStep += 1;
        gameMN.row = row;
        gameMN.col = col;
        gameMN.startControl = true;
    }
}
