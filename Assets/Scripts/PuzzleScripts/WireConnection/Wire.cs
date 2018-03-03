using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Leonel Jara
public class Wire : MonoBehaviour {

	public int wireIDLink = 0; // will be assgined by InitPuzzle()
	public Connection connection = null;
	
	public float scaleLimit = 20f; // limit the scale. so its does not scale infinitely

    public GameObject neckGO; // the ref of the neck wire

	private Camera cameraTarget; // Change this if u remove the puzzle camera

    //original positions for the head and neck of the wire
	private Vector2 orgWireHeadWidth;
    private Vector2 orgWireNeckWidth;

    //current ref of the head and neck of the wire
    private Transform wireHeadTrans;
    private Transform wireNeckTrans;

    //ref of the neck and head for the sprite
    private SpriteRenderer wireSpriteHead;
    private SpriteRenderer wireSpriteNeck;

    private Color[] color = { Color.green, Color.gray, Color.white, Color.yellow, Color.red };

    void Awake(){
        wireSpriteNeck = neckGO.GetComponent<SpriteRenderer>();
        wireSpriteHead = GetComponent<SpriteRenderer>();
        wireNeckTrans = neckGO.GetComponent<Transform>();
        wireHeadTrans = GetComponent<Transform>();
        cameraTarget = GameObject.Find ("PuzzleCamera").GetComponent<Camera> (); // TODO: maybe have puzzlecamera have its own static instance
	}

	// Use this for initialization
	void Start () {
		orgWireHeadWidth = new Vector2 (wireSpriteHead.size.x, wireSpriteHead.size.y);
        orgWireNeckWidth = new Vector2(wireSpriteNeck.size.x, wireSpriteNeck.size.y);

        wireSpriteNeck.color = color[wireIDLink - 1];
    }


	void OnMouseDrag(){
		//get mouse position that reps in the game world
		Vector3 mousePoint = cameraTarget.ScreenToWorldPoint (Input.mousePosition);
		//Follow the mouse
		FollowTarget (mousePoint);
	}

	void OnMouseUp(){
		//if the wire is not connected it will snap back to its orignal scale and rotation
		if( connection == null ){
			SnapWireBack ();
		}
		else {
			//if the wire is connected and has not been disconnected; snap to the connection block.
			connection.SnapWire ();
		}
	}
	//When the player mouse down on the wire that is already connected this will causes it to disconnect	
	void OnMouseDown(){
		if(connection != null){
			connection.DisconnectWireAffect ();
		}
	}
	//Place the wire to its orginal position
	public void SnapWireBack(){
		wireSpriteHead.size = new Vector2 (orgWireHeadWidth.x ,orgWireHeadWidth.y);
        wireSpriteNeck.size = new Vector2(orgWireNeckWidth.x, orgWireNeckWidth.y);
        wireHeadTrans.rotation = Quaternion.Euler (new Vector3(0 , 0, 0));
        wireNeckTrans.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        connection = null;
	}

	//Given the position, the wire will scale on the x axis and rotate to that position
	public void FollowTarget(Vector3 targetPos){
		
		//rotation to follow 
		Vector3 toTargetVector = targetPos - wireHeadTrans.position;
		float zRotation = Mathf.Atan2( toTargetVector.y, toTargetVector.x ) * Mathf.Rad2Deg;
		wireHeadTrans.rotation = Quaternion.Euler(new Vector3 ( 0, 0, zRotation));
        wireNeckTrans.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation ));

        //Scale to follow
        float distance = Vector2.Distance(wireHeadTrans.position,  targetPos); // calculates the magnitude between the mouse and this gameobject
		float widthScaleChange = Mathf.Abs(distance) * 1.2f; // offset the value by 11.5f. This is so it can scale with the distance.

        //will scale only if the scale change is not less then the current scale. And not passing the limit scale
        if (widthScaleChange <= scaleLimit && widthScaleChange >= orgWireNeckWidth.x)
        {
            //Apply scale
            wireSpriteHead.size = new Vector2(widthScaleChange + 0.8f, wireSpriteHead.size.y);
            wireSpriteNeck.size = new Vector2(widthScaleChange, wireSpriteNeck.size.y);

        }
        else if(widthScaleChange < orgWireNeckWidth.x) {
            wireSpriteHead.size = new Vector2(orgWireHeadWidth.x, orgWireHeadWidth.y);
            wireSpriteNeck.size = new Vector2(orgWireNeckWidth.x, orgWireNeckWidth.y);
        }
	}
		
}
