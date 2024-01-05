using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainManager Instance;

    // cordinates of the player for new scene
    public Vector3 playerPosition = new Vector3(0, 0, 1);
    // scene to load
    public int sceneBuildIndex = 1;



    private void Awake()
    {
        // 
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
