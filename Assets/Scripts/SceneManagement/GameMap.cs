using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameMap : MonoBehaviour
{
    [SerializeField] public GameObject StationSelectPanel;

    [Header("Stations")]
    [SerializeField] private GameObject[] stations;

    // add gameobject OnClickSetPos to the button in the inspector
    private OnClickSetPos onClickSetPos;

    private bool isMapOpen = false;

    private bool canOpenMap = false;

    private bool canCloseMap = false;


    private int currentStationIndex = 0;

    // station pressed by player string name
    private string stationPressed;

    //Vector2 playerPosition;

    private static GameMap instance;

    public string sceneToLoad;

    //public Vector2 playerPosition;
    //public VectorValue playerStorage;

    // Singleton  
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameMap GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        StationSelectPanel.SetActive(false);


    }

    private void Update()
    {
        canOpenMap = Input.GetKeyDown(KeyCode.M) && !StationSelectPanel.activeSelf || MapTrigger.canMove && Input.GetKeyUp(KeyCode.E) && !StationSelectPanel.activeSelf;
        canCloseMap = Input.GetKeyDown(KeyCode.Escape) && StationSelectPanel.activeSelf || Input.GetKeyDown(KeyCode.M) && StationSelectPanel.activeSelf || MapTrigger.canMove && Input.GetKeyDown(KeyCode.Q) && StationSelectPanel.activeSelf;

        if (canOpenMap)
        {
            StationSelectPanel.SetActive(true);
            isMapOpen = true;
        }
        else if (canCloseMap)
        {
            StationSelectPanel.SetActive(false);
            isMapOpen = false;
        }

        if (StationSelectPanel.activeSelf && isMapOpen)
        {
            // if player hover over the station button using keyboard use OnclickSetPos to set player position
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                currentStationIndex = (currentStationIndex < stations.Length - 1) ? currentStationIndex + 1 : 0;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                currentStationIndex = (currentStationIndex > 0) ? currentStationIndex - 1 : stations.Length - 1;
            }

            EventSystem.current.SetSelectedGameObject(stations[currentStationIndex]);

            // if player press E key, but E key is already in use wait for the player to press the E key again
            if (Input.GetKeyDown(KeyCode.E))
            {
                stations[currentStationIndex].GetComponent<Button>().onClick.Invoke();
            }


        }

    }

    public void StationPressed(string stationName)
    {
        //playerPosition = playerPos;
        stationPressed = stationName;
        StationSelectPanel.SetActive(false);
        StartCoroutine(TransitionToStation());
    }




    // add player position to the player storage vector value when UI.button is pressed
    // public void NewPlayerPosition(Vector2 playerPos)
    // {
    //     playerStorage.initialValue = playerPos;
    // }

    /* public void NewPlayerPosition(string posZ, string posX)
    {
        playerPosition = new Vector2(float.Parse(posZ), float.Parse(posX));
        playerStorage.initialValue = playerPosition;
    } */



    /*  public void SetPlayerPosition(Vector2 playerPos)
     {
         playerPosition = playerPos;
     } */


    private IEnumerator TransitionToStation()
    {
        // playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(stationPressed);
        yield return null;
    }


}

/* 
private void Update()
{
    if (Input.GetKeyDown(KeyCode.M) && !StationSelectPanel.activeSelf || MapTrigger.canMove == true && Input.GetKeyDown(KeyCode.E) && !StationSelectPanel.activeSelf)
    {
        StationSelectPanel.SetActive(true);
        isMapOpen = true;
    }
    else if (Input.GetKeyDown(KeyCode.Escape) && StationSelectPanel.activeSelf || Input.GetKeyDown(KeyCode.M) && StationSelectPanel.activeSelf || MapTrigger.canMove == true && Input.GetKeyDown(KeyCode.Q) && StationSelectPanel.activeSelf)
    {
        StationSelectPanel.SetActive(false);
        isMapOpen = false;
    }



    if (StationSelectPanel.activeSelf && isMapOpen)
    {
        // if player hover over the station button using keyboard use OnclickSetPos to set player position
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (currentStationIndex < stations.Length - 1)
            {
                currentStationIndex++;
            }
            else
            {
                currentStationIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (currentStationIndex > 0)
            {
                currentStationIndex--;
            }
            else
            {
                currentStationIndex = stations.Length - 1;
            }
        }

        EventSystem.current.SetSelectedGameObject(stations[currentStationIndex]);

        if (Input.GetKeyDown(KeyCode.E))
        {
            stations[currentStationIndex].GetComponent<Button>().onClick.Invoke();
        }

    }


} */