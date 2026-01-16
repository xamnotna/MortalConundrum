using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "New Party Member")]
public class PartyMemberInfo : ScriptableObject
{
    public string MemberName;
    public int StartingLevel;
    public int BaseHealth;
    public int BaseMana;
    public int BaseStr;
    public int BaseDef;
    public int BaseMag;
    public int BaseInitiative;
    public GameObject MemberBattleVisualPrefab; // what will be shown in battle scene
    public GameObject MemberOverworldVisualPrefab;  // what will be shown in the overworld



}
