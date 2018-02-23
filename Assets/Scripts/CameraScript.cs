
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

	private Transform playerTrans;       //Public variable to store a reference to the player game object

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    private Camera cam;
    private float halfHeight;
    private float halfWidth;

    public BoxCollider2D boundBox;
    private Vector3 minBounds;      //Will store the minimum bounds for the scene
    private Vector3 maxBounds;      //Will store the maximum bounds for the scene


    // Use this for initialization
    void Start()
    {
		playerTrans = Player.instance.GetComponent<Transform>(); // get the instance of the player. We place it in the start since the instance is set in its awake
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - playerTrans.position;

        minBounds = boundBox.bounds.min;    //Gets the minimum x and y for the bounds
        maxBounds = boundBox.bounds.max;    // Gets the maximum x and y for the bounds

        //Gets the width and height of the camera view
        cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }

    void LateUpdate()
    {
		this.transform.position = new Vector3(playerTrans.position.x + offset.x,
											  playerTrans.position.y + offset.y,
											  playerTrans.position.z + offset.z);

		float clampedX = Mathf.Clamp(playerTrans.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
		float clampedY = Mathf.Clamp(playerTrans.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        this.transform.position = new Vector3(clampedX, clampedY, transform.position.z);

    }

}
