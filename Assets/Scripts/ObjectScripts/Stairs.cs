using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           Player.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Player.instance.GetComponent<Rigidbody2D>().velocity.x, 0);
           //Debug.Log("Cut Vel");
        }
    }


}
