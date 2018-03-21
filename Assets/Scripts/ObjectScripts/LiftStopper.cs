using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class will used to stop the lift if the player is in the way of the lift */
public class LiftStopper : MonoBehaviour {

    public Lift lift; // ref of lift to affect

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Will stop the lift if the player is not in the lift, and the lift is going down
        if (collision.CompareTag("Player") && !lift.isPlayerIn && !lift.DirectionUp)
        {
            lift.activate = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && lift.isOpen)
        {
            lift.activate = true;
        }
    }

}
