// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * This class will used to stop the lift if the player is at at the bottom of the lift
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftStopper : MonoBehaviour {

    //Reference to the lift to affect
    public Lift lift;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Will stop the lift if the player is not in the lift, and the lift is going down
        if (collision.CompareTag("Player") && !lift.isPlayerIn && !lift.DirectionUp && lift.isOpen)
        {
            lift.activate = false;            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && lift.isOpen && !lift.isPlayerIn)
        {
            lift.activate = true;
        }
    }


    

}
