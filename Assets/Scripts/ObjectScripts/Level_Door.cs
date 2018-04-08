using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*This Level_Door class will be used for door that are in a level */
public class Level_Door : WorldObject {

	private Animator animat;
	private BoxCollider2D door;
	private AudioSource mySound;
	private void Awake()
	{
		objectName = "Level_Door";
		door = GetComponent<BoxCollider2D>();
		animat = GetComponent<Animator>();
		mySound = GetComponent<AudioSource> ();
		//Start the door locked
		Lock();
	}

    private void Awake()
    {
        objectName = "Level_Door";
        door = GetComponent<BoxCollider2D>();
        animat = GetComponent<Animator>();

    }
    private void Start()
    {
        if (!isOpen)
        {
            //Start the door locked
            Lock();
        }
        else {
            //Start the door unlocked
            Unlock();
        }
    }
	public override void Lock()
	{
		isLocked = true;
		animat.SetBool("IsLocked", isLocked);
	}
	public override void Unlock()
	{
		isLocked = false;
		animat.SetBool("IsLocked", isLocked);
		mySound.Play ();
	}

	public override void OpenMove()
	{
		if (!isLocked) {
			door.enabled = false;
			isOpen = true;
		}

	}

	public override void CloseMove()
	{
		if (isLocked)
		{
			door.enabled = true;
			isOpen = false;
		}
	}
	//The animation controller will take care when to take off the collider, or put on.
	public void AffectDoorState(AnimationEvent eventOpen) {
		if (eventOpen.intParameter == 0)
		{

			OpenMove();
		}
		else {
			CloseMove();
		}
	}

}
