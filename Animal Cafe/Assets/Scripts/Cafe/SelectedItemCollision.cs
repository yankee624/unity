using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemCollision : MonoBehaviour {

    public bool collidePoo = false;
    public GameObject collidedObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Poo")
        {
            collidePoo = true;
            collidedObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Poo")
        {
            collidePoo = false;
        }
    }

}
