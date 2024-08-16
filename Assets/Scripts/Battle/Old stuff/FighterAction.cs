using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hero;

    [SerializeField]
    private GameObject meleePrefab;

    [SerializeField]
    private GameObject rangePrefab;

    /*    [SerializeField]
       private GameObject magicPrefab;

       [SerializeField]
       private GameObject defendPrefab;

       [SerializeField]
       private GameObject itemPrefab; */

    [SerializeField]
    private Sprite faceIcon;

    private GameObject currentAttack;

    /*  private GameObject magicAttack;
     private GameObject defendAttack;
     private GameObject itemAttack; */

    private string attackType;

    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

    }

    public void SelectAttack(string btn)
    {
        GameObject victim = hero;
        if (tag == "Hero")
        {
            victim = enemy;
        }
        if (btn.CompareTo("melee") == 0)
        {
            meleePrefab.GetComponent<ActionScript>().Attack(victim);
        }
        else if (btn.CompareTo("range") == 0)
        {
            rangePrefab.GetComponent<ActionScript>().Attack(victim);
        }
        else
        {
            Debug.Log("Run Button Pressed!");
        }
    }


}
