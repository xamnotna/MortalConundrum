using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;
    private bool canMove = false;

    // Level move zone by press in E, if colliding with player, load scene
    // Update is called once per frame
    void Update()
    {
        if (canMove == true && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            canMove = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       // print("Trigger Entered");
        //Could use other.GetComponent<PlayerController>() to see if the object has a PlayerController component
        //Tags Work too. Maybe some players have different script components, but all have the same tag
        if (other.tag == "Player")
            {
               // print("Switching to scene " + sceneBuildIndex);
                canMove = true;
                //SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            }

}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canMove = false;
        }
    }
}