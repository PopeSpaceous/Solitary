using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tans : MonoBehaviour {
    Vector3 centrePoint;
    float direction; //0-7 * 45 rotating on z axis
    bool flipped; //false is original side \\\\\\\\\ parellogram true is flipped /////////
    int type; //5 is big tri, 4 is med tri, 3 is small tri, 2 is square, 1 is parellelogram
    bool placed; 
    
    void turn()
    {
        if (direction < 7) direction++;
        else direction = 0;
    }
    void move(Vector3 newPoint)
    {
        centrePoint = newPoint;
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
