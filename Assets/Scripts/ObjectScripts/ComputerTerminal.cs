using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTerminal : MonoBehaviour {
	public Animator ani;
	public ComputerScreen comp;
	// Use this for initialization
	void Start () {
		ani.speed = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player") && Player.instance.actionButtion) {
			//comp.toggleView();
			comp.toggleView();
		}
	}
}
