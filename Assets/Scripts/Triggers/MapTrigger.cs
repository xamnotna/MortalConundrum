using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapTrigger : MonoBehaviour
{

    static public bool canMove = false;





    private void OnTriggerEnter2D(Collider2D other)
    {
        // print("Trigger Entered");
        //Could use other.GetComponent<PlayerController>() to see if the object has a PlayerController component
        //Tags Work too. Maybe some players have different script components, but all have the same tag
        if (other.tag == "Player" && !other.isTrigger)
        {
            canMove = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            canMove = false;
        }
    }



}
