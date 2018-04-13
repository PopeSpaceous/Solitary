// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * A child of WorldObject for the pivot door sprite that can be interacted with another object that support WorldObjects
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotDoor : WorldObject {

    private Animator ani;
    private BoxCollider2D door;

    //Lift the animation to open to the left, then the right
    public bool flipY = false;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        door = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        ani.SetBool("Flip", flipY);
    }

    public override void Lock()
    {
        isLocked = false;
        CloseMove();        
    }
    public override void Unlock()
    {
        isLocked = true;
        OpenMove();
    }
    public override void OpenMove()
    {
        isOpen = true;
        ani.SetBool("isOpen", isOpen);
        door.enabled = false;
    }

    public override void CloseMove()
    {
        isOpen = false;
        ani.SetBool("isOpen", isOpen);
        door.enabled = true;

    }
}
