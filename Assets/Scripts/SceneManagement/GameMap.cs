using System.Collections;
using System.Collections.Generic;
using System;
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

    [Header("Map availability")]
    [SerializeField] private string[] scenesWithInactiveMap;
    [Header("Map unavailable message")]
    [SerializeField] private TMP_Text mapUnavailableMessageText;
    [SerializeField] private string mapUnavailableMessage = "There is no map available.";
    [SerializeField] private float messageFadeDuration = 0.35f;
    [SerializeField] private float messageVisibleDuration = 2f;

    // add gameobject OnClickSetPos to the button in the inspector
    private OnClickSetPos onClickSetPos;


    private int currentStationIndex = 0;

    // station pressed by player string name
    private string stationPressed;
    private bool isMapEnabledInCurrentScene = true;
    private Coroutine mapUnavailableMessageCoroutine;
    private bool hasLoggedMissingMessageTextReference;

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

        HideMapUnavailableMessageInstant();
    }

    public static GameMap GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        isMapEnabledInCurrentScene = IsMapEnabledForScene(SceneManager.GetActiveScene().name);
        StationSelectPanel.SetActive(false);
        HideMapUnavailableMessageInstant();
    }

    private void OnDisable()
    {
        if (mapUnavailableMessageCoroutine != null)
        {
            StopCoroutine(mapUnavailableMessageCoroutine);
            mapUnavailableMessageCoroutine = null;
        }

        HideMapUnavailableMessageInstant();
    }

    private bool IsMapEnabledForScene(string sceneName)
    {
        if (scenesWithInactiveMap == null || scenesWithInactiveMap.Length == 0)
        {
            return true;
        }

        for (int i = 0; i < scenesWithInactiveMap.Length; i++)
        {
            string inactiveSceneName = scenesWithInactiveMap[i];
            if (string.IsNullOrWhiteSpace(inactiveSceneName))
            {
                continue;
            }

            if (string.Equals(inactiveSceneName.Trim(), sceneName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

        return true;
    }

    private void Update()
    {
        if (!isMapEnabledInCurrentScene)
        {
            if (StationSelectPanel.activeSelf)
            {
                StationSelectPanel.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                ShowMapUnavailableMessage();
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.M) && !StationSelectPanel.activeSelf)
        {
            StationSelectPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && StationSelectPanel.activeSelf || Input.GetKeyDown(KeyCode.M) && StationSelectPanel.activeSelf)
        {
            StationSelectPanel.SetActive(false);
        }



        if (StationSelectPanel.activeSelf)
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

            // Only set selected object if stations array and current station are valid
            if (stations != null && stations.Length > 0 && currentStationIndex >= 0 && currentStationIndex < stations.Length && stations[currentStationIndex] != null)
            {
                EventSystem.current.SetSelectedGameObject(stations[currentStationIndex]);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                stations[currentStationIndex].GetComponent<Button>().onClick.Invoke();
            }








        }
    }

    private void ShowMapUnavailableMessage()
    {
        if (mapUnavailableMessageText == null)
        {
            if (!hasLoggedMissingMessageTextReference)
            {
                Debug.LogWarning("GameMap: 'Map Unavailable Message Text' is not assigned.", this);
                hasLoggedMissingMessageTextReference = true;
            }

            return;
        }

        if (mapUnavailableMessageCoroutine != null)
        {
            StopCoroutine(mapUnavailableMessageCoroutine);
        }

        mapUnavailableMessageCoroutine = StartCoroutine(ShowMapUnavailableMessageRoutine());
    }

    private IEnumerator ShowMapUnavailableMessageRoutine()
    {
        SetMapUnavailableMessageAlpha(0f);
        mapUnavailableMessageText.gameObject.SetActive(true);
        mapUnavailableMessageText.text = mapUnavailableMessage;

        yield return FadeMapUnavailableMessage(0f, 1f, messageFadeDuration);
        yield return new WaitForSeconds(messageVisibleDuration);
        yield return FadeMapUnavailableMessage(1f, 0f, messageFadeDuration);

        mapUnavailableMessageText.gameObject.SetActive(false);
        mapUnavailableMessageCoroutine = null;
    }

    private IEnumerator FadeMapUnavailableMessage(float fromAlpha, float toAlpha, float duration)
    {
        if (duration <= 0f)
        {
            SetMapUnavailableMessageAlpha(toAlpha);
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            SetMapUnavailableMessageAlpha(Mathf.Lerp(fromAlpha, toAlpha, t));
            yield return null;
        }

        SetMapUnavailableMessageAlpha(toAlpha);
    }

    private void SetMapUnavailableMessageAlpha(float alpha)
    {
        Color color = mapUnavailableMessageText.color;
        color.a = alpha;
        mapUnavailableMessageText.color = color;
    }

    private void HideMapUnavailableMessageInstant()
    {
        if (mapUnavailableMessageText == null)
        {
            return;
        }

        SetMapUnavailableMessageAlpha(0f);
        mapUnavailableMessageText.gameObject.SetActive(false);
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
