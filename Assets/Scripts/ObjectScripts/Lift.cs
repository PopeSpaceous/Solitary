using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : WorldObject {

    [HideInInspector]
    public bool activate = false;//Begin moving the lift when true

    public float liftDistance = 5f;
    public float liftSpeed = 3f;
    public bool DirectionUp = true; //if false, the direction will be down
    public float waitTime = 1f; // wait time before it should start moving again after it reaches its distance

    private Animator liftSate;
    private bool isPlayerIn = false; // checks if the player is on the platfrom or not
    private float currentDistance = 0;
    private Rigidbody2D rigdL;

    private void Awake()
    {
        rigdL = GetComponent<Rigidbody2D>();
        liftSate = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        objectName = "Lift";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only activate the platfrom once
        if (collision.CompareTag("Player") && !isLocked) {
            //Once activated this will not be called again
            if (!activate)
            {
                activate = true;
                isOpen = true;
            }
            isPlayerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerIn = false;
        }
    }
    // FixedUpdate will handle the lift's movement
    void FixedUpdate () {

        if (isOpen && activate)
        {            
            //Keep applying distance
            if (currentDistance < liftDistance)
            {                
                currentDistance+= 0.1f;     //cut the distance    
                OpenMove();
            }
            else
            {                
                isOpen = false;
                StartCoroutine(NextMove());
            }
        }
        else if(rigdL.velocity.y != 0) {

            if (isPlayerIn) { // cut the velocity of the player when the lift stops
                Player.instance.GetComponent<Rigidbody2D>().velocity = new Vector3(Player.instance.GetComponent<Rigidbody2D>().velocity.x, 0, 0);
            }
            
            rigdL.velocity = new Vector3(0, 0, 0);
        }

        liftSate.SetBool("IsOpen", isOpen);
        liftSate.SetBool("DirectionUp", DirectionUp);
    }

    public override void Lock()
    {
        isLocked = true;
        activate = false;
    }
    /* Note: if you unlokcing a lift and want to activate 
     * right now, then set the activate bool to true as well*/
    public override void Unlock()
    {
        isLocked = false;
    }

    public override void OpenMove()
    {
        //Depending the Direction apply the needed velocity
        if (DirectionUp) {
            rigdL.velocity = new Vector3(0, liftSpeed, 0);
        }
        else        
        {
            if (isPlayerIn && rigdL.velocity.y != liftSpeed * -1) // set the velocity of the player when the lift start going down
            {
                Player.instance.GetComponent<Rigidbody2D>().velocity = new Vector3(Player.instance.GetComponent<Rigidbody2D>().velocity.x, liftSpeed  * -1, 0);
            }
            rigdL.velocity = new Vector3(0, liftSpeed * -1, 0);
        }
    }

    public override void CloseMove()
    {
        //flip direction
        currentDistance = 0;        
        DirectionUp = !DirectionUp;        
        isOpen = true;
    }

    //Wait to move again
    IEnumerator NextMove() {
        yield return new WaitForSeconds(waitTime);
        CloseMove();
    }

}
