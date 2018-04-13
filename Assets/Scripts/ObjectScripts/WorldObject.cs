// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
* Any gameobject that is part of the world and is interactable, besides the puzzle placeholder,
* all inherhit from world Object. The need functions and vars must be coded when they inherit this class 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour {

    //Need vars for all world objects
    [HideInInspector]
    public string objectName;
    public bool isLocked = false;
    public bool isOpen = false;



    //Need functions for all WorldObjects
    public abstract void Lock();
    public abstract void Unlock();
    public abstract void OpenMove();
    public abstract void CloseMove();

}
