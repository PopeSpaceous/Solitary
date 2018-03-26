﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotDoor : WorldObject {

    private Animator ani;
    private BoxCollider2D door;


    private void Awake()
    {
        ani = GetComponent<Animator>();
        door = GetComponent<BoxCollider2D>();
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
