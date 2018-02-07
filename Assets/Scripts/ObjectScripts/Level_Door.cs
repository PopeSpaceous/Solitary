using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*This Level_Door class will be used for door that are in a level. The type of door can change */
public class Level_Door : MonoBehaviour {

	//TODO: Complete door unlocking when a puzzle is finished

	public bool isDoorlocked = false;

	/*Level door trigger and lock and unlock code here */
	
	//the passed bool will either lock or unlock the door
	public void DoorLockChange(bool key){
		isDoorlocked = key;
	}
}
