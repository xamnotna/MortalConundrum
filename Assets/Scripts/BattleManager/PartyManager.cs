using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartyManager : MonoBehaviour
{
    [SerializeField] private PartyMemberInfo[] allMembers; // List of all possible party members info ScriptableObjects
    [SerializeField] private List<PartyMember> currentParty; // List of current party members in the party

    [SerializeField] private PartyMemberInfo defaultPartyMember; // Default party member info ScriptableObject

    private void Awake()
    {
        AddMemberToPartyByName(defaultPartyMember.MemberName);
    }
    public void AddMemberToPartyByName(string memberName)
    {
        for (int i = 0; i < allMembers.Length; i++)
        {
            if (allMembers[i].MemberName == memberName)
            {
                PartyMember newPartyMember = new PartyMember();
                newPartyMember.MemberName = allMembers[i].MemberName;
                newPartyMember.Level = allMembers[i].StartingLevel;
                //newPartyMember.CurrentExp = 0;
                //newPartyMember.ExpToNextLevel = 100; // Example value, can be adjusted
                newPartyMember.CurrentHealth = allMembers[i].BaseHealth;
                newPartyMember.MaxHealth = newPartyMember.CurrentHealth;
                newPartyMember.CurrentMana = allMembers[i].BaseMana;
                newPartyMember.MaxMana = newPartyMember.CurrentMana;
                newPartyMember.Strength = allMembers[i].BaseStr;
                newPartyMember.Defense = allMembers[i].BaseDef;
                newPartyMember.Magic = allMembers[i].BaseMag;
                newPartyMember.Initiative = allMembers[i].BaseInitiative;
                newPartyMember.MemberBattleVisualPrefab = allMembers[i].MemberBattleVisualPrefab;
                newPartyMember.MemberOverworldVisualPrefab = allMembers[i].MemberOverworldVisualPrefab;

                currentParty.Add(newPartyMember);
            }
        }

    }

    public List<PartyMember> GetCurrentParty()
    {
        return currentParty;
    }
}

[System.Serializable]
public class PartyMember
{
    public string MemberName;
    public int Level;
    public int CurrentHealth;
    public int MaxHealth;
    public int CurrentMana;
    public int MaxMana;
    public int Strength;
    public int Defense;
    public int Magic;
    public int Initiative;
    public int CurrentExp;
    public int ExpToNextLevel;
    public int MaxExp;
    public GameObject MemberBattleVisualPrefab;
    public GameObject MemberOverworldVisualPrefab;
}

