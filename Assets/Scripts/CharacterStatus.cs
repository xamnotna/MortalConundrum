using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthStatusData", menuName = "StatusObjects/Health", order = 1)]
public class CharacterStatus : ScriptableObject
{
    public string characterName = "name";
    // save last position if player
    //public Vector2 initialValue;
    public GameObject characterGameObject;
    public int level = 1;
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float maxMana = 100;
    public float currentMana = 100;
}
