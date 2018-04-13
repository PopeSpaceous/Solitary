// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * Script for making the stairs act like stairs.
 * It will cut the player's y velocity
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           Player.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Player.instance.GetComponent<Rigidbody2D>().velocity.x, 0);
        }
    }


}
