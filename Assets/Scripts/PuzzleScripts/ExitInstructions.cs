using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * This class will be used for exiting the insturctions panel
 * 
*/
public class ExitInstructions : MonoBehaviour {

    public Button exitButton;

    public GameObject parent;

    void Start () {
        Button btn = exitButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    //unactivate the partent gameobject attched to this script
    public void TaskOnClick() {
        parent.SetActive(false);
    }
}
