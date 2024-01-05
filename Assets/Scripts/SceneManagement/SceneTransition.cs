using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    private bool canMove = false;

    /* public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    } */

    void Update()
    {
        if (canMove == true && Input.GetKeyDown(KeyCode.E))
        {
            // load scene
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
            canMove = false;
        }
    }



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
