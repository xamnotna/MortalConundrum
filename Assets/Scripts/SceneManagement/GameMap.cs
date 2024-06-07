
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameMap : MonoBehaviour
{
    [SerializeField] public GameObject StationSelectPanel;

    [Header("Stations")]
    [SerializeField] private GameObject[] stations;

    private int currentStationIndex = 0;

    private static GameMap instance;

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
        if (Input.GetKeyDown(KeyCode.M))
        {
            StationSelectPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
        {
            StationSelectPanel.SetActive(false);
        }
        if (StationSelectPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Enter scene
                //StationSelectPanel.SetActive(false);
                Debug.Log("Enter scene");
            }


            /*   if (Input.GetKeyDown(KeyCode.W))
              {
                  // Navigate up
              }
              else if (Input.GetKeyDown(KeyCode.A))
              {
                  // Navigate left

              }
              else if (Input.GetKeyDown(KeyCode.S))
              {
                  // Navigate down
              }
              else if (Input.GetKeyDown(KeyCode.D))
              {
                  // Navigate right
              }
              else if (Input.GetKeyDown(KeyCode.E))
              {
                  // Enter scene

              } */
        }
    }
}


