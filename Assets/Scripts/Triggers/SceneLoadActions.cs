using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoadActions : MonoBehaviour
{

    // use store value from  LevelMove_Ref
    // public static Vector3 TempPosition;
    // yourDesiredScene
    public int yourDesiredScene = 1; //the scene you want to teleport your player to



    [SerializeField] private Transform player; //drag player reference onto here
    private Vector3 targetPosition; //here you store the position you want to teleport your player to



    private void OnEnable()
    {
        //This is where you add the method you want to be called every time a new scene is loaded
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        //This is where you remove the method you want to be called every time a new scene is loaded
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    //After adding t$$anonymous$$s method to the delegate, t$$anonymous$$s method will be called every time
    //that a new scene is loaded. You can then compare the scene loaded to your desired
    //scenes and do actions according to the scene loaded.
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == yourDesiredScene)
        {
            //Here you set the position of your player to the position you want it to be in the new scene
            player.position = targetPosition;
        }
    }

    //This method is called when you want to teleport your player to the new scene
    public void TeleportPlayer()
    {
        //Here you store the position you want your player to be in the new scene
        targetPosition = new Vector3(0, 0, 0);
        //Here you load the new scene
        SceneManager.LoadScene(yourDesiredScene, LoadSceneMode.Single);
    }

    //This method is called when you want to teleport your player to the new scene
}
