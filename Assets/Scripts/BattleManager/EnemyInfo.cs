using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string EnemyName;
    public int StartingLevel;
    public int BaseHealth;
    public int BaseStr;
    public int BaseDef;
    public int BaseMag;
    public int BaseInitiative;
    public GameObject EnemyVisualPrefab; // used in battle scene
}
