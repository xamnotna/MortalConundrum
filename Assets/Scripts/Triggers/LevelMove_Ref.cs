using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelMove_Ref : MonoBehaviour
{
    // store scene to load
    public int sceneBuildIndex;
    private bool canMove = false;
    // add corrdinates for player to move to
    public float x;
    public float y;

    private Transform player;
    // when trigger is entered and player presses E, store position and sceneBuildIndex to use in LevelManager
    // Update is called once per frame

    public void newVector()
    {
        MainManager.Instance.playerPosition = new Vector3(x, y, 1);
        MainManager.Instance.sceneBuildIndex = sceneBuildIndex;
    }

    void Update()
    {
        if (canMove == true && Input.GetKeyDown(KeyCode.E))
        {
            // load scene
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            // SceneLoaded(SceneManager.GetSceneByBuildIndex(sceneBuildIndex), LoadSceneMode.Single);

            // move player to new location
            //GameObject.Find("Player").transform.position = new Vector3(x, y, 1);

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
            canMove = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canMove = false;
        }
    }

    /*  public void SceneLoaded(Scene scene, LoadSceneMode mode)
     {
         MainManager.Instance.playerPosition = new Vector3(x, y, 1);
     } */
}

/*     // store scene to load
    public int sceneBuildIndex;
    private bool canMove = false;
    // add corrdinates for player to move to
    public float x;
    public float y;



    // Level move zone by press in E, if colliding with player, load scene
    // Update is called once per frame 
    void Update()
    {
        if (canMove == true && Input.GetKeyDown(KeyCode.E))
        {
            // load scene
            //SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            // move player to new location
            //GameObject.Find("Player").transform.position = new Vector3(x, y, 1);
        
            // move player to new location


            StartCoroutine(LoadLevel());
            canMove = false;
            
        }
        // if scene is loaded, move player to new location
        if (SceneManager.GetActiveScene().buildIndex == sceneBuildIndex)
        {
            GameObject.Find("Player").transform.position = new Vector3(x, y, 1);
        }
        
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        
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
               // other.transform.position = new Vector3(x, y, 1);
                //SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            }

}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canMove = false;
        }
    } */