using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClickSetPos : MonoBehaviour
{
    public Vector2 playerPosition;

    public VectorValue playerStorage;

    public string sceneToLoad;


    //public GameObject GameMap; // reference to the GameMap object

    void Start()
    {

        GetComponent<Button>().onClick.AddListener(onClick);



    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Clicked!, Player Position Set!");
        }

    }

    // set player position when hovering over button
    public void SetPlayerPosition()
    {
        playerStorage.initialValue = playerPosition;

    }

    void onClick()
    {
        playerStorage.initialValue = playerPosition;
        SetPlayerPosition();
        Debug.Log("Clicked!");
    }

}
