using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform[] partySpawnPoints;
    [SerializeField] private Transform[] enemySpawnPoints;

    [Header("Battlers")]
    [SerializeField] private List<BattleEntities> allBattlers = new List<BattleEntities>();
    [SerializeField] private List<BattleEntities> enemyBattlers = new List<BattleEntities>();
    [SerializeField] private List<BattleEntities> playerBattlers = new List<BattleEntities>();

    [Header("UI")]
    [SerializeField] private GameObject[] enemySelectionButtons;
    [SerializeField] private GameObject battleMenu;
    [SerializeField] private GameObject enemySelectionMenu;
    [SerializeField] private TextMeshProUGUI actionText;

    private PartyManager partyManager;
    private EnemyManager enemyManager;
    private int currentPlayer;

    private const string ACTION_MESSAGE = "'s action:";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        partyManager = GameObject.FindAnyObjectByType<PartyManager>();
        enemyManager = GameObject.FindAnyObjectByType<EnemyManager>();

        CreatePartyEntities();
        CreateEnemyEntities();
        ShowBattleMenu();
    }

    private void CreatePartyEntities()
    {
        // get current party
        List<PartyMember> currentParty = new List<PartyMember>();
        currentParty = partyManager.GetCurrentParty();

        // create battle entities for each party member
        for (int i = 0; i < currentParty.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();
            // Set the values for the battle entity based on the party member's stats
            tempEntity.SetEntityValues(currentParty[i].MemberName, currentParty[i].CurrentHealth,
            currentParty[i].MaxHealth, currentParty[i].CurrentMana, currentParty[i].MaxMana,
            currentParty[i].Strength, currentParty[i].Defense, currentParty[i].Magic,
            currentParty[i].Initiative, currentParty[i].Level, true);

            // Instantiate battle visual prefab for each party member and set starting values
            BattleVisuals tempBattleVisuals = Instantiate(currentParty[i].MemberBattleVisualPrefab,
            partySpawnPoints[i].position, Quaternion.identity).GetComponent<BattleVisuals>();
            // Set starting values for the battle visual based on the party member's stats
            tempBattleVisuals.SetStartingValues(currentParty[i].MaxHealth, currentParty[i].MaxHealth,
            currentParty[i].MaxMana, currentParty[i].MaxMana, currentParty[i].Level);
            // Assign the battle visual to the battle entity
            tempEntity.BattleVisuals = tempBattleVisuals;

            allBattlers.Add(tempEntity);
            playerBattlers.Add(tempEntity);
        }

    }

    private void CreateEnemyEntities()
    {
        // get current enemies
        List<Enemy> currentEnemies = new List<Enemy>();
        currentEnemies = enemyManager.GetCurrentEnemies();

        // create battle entities for each enemy
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();

            tempEntity.SetEntityValues(currentEnemies[i].EnemyName, currentEnemies[i].CurrentHealth,
            currentEnemies[i].MaxHealth, currentEnemies[i].CurrentMana, currentEnemies[i].MaxMana,
            currentEnemies[i].Strength, currentEnemies[i].Defense, currentEnemies[i].Magic,
            currentEnemies[i].Initiative, currentEnemies[i].Level, false);
            // Instantiate battle visual prefab for each enemy and set starting values
            BattleVisuals tempBattleVisuals = Instantiate(currentEnemies[i].EnemyVisualPrefab,
            enemySpawnPoints[i].position, Quaternion.identity).GetComponent<BattleVisuals>();
            // Set starting values for the battle visual based on the enemy's stats
            tempBattleVisuals.SetStartingValues(currentEnemies[i].MaxHealth, currentEnemies[i].MaxHealth,
            currentEnemies[i].MaxMana, currentEnemies[i].MaxMana, currentEnemies[i].Level);
            // Assign the battle visual to the battle entity
            tempEntity.BattleVisuals = tempBattleVisuals;

            allBattlers.Add(tempEntity);
            enemyBattlers.Add(tempEntity);
        }

    }

    public void ShowBattleMenu()
    {
        // whos action it is
        actionText.text = playerBattlers[currentPlayer].Name + ACTION_MESSAGE;
        battleMenu.SetActive(true);
        // enabling our battle menu
    }

    public void ShowEnemySelectionMenu()
    {
        // disable the battle menu
        battleMenu.SetActive(false);
        // set our enemy selection buttons
        SetEnemySelectionButtons();
        // enable our selection menu
        enemySelectionMenu.SetActive(true);

    }

    private void SetEnemySelectionButtons()
    {
        // disable all buttons first
        for (int i = 0; i < enemySelectionButtons.Length; i++)
        {
            enemySelectionButtons[i].SetActive(false);
        }
        // enable buttons for each enemy
        for (int j = 0; j < enemyBattlers.Count; j++)
        {
            enemySelectionButtons[j].SetActive(true);
            // set the button text to the enemy name
            enemySelectionButtons[j].GetComponentInChildren<TextMeshProUGUI>().text = enemyBattlers[j].Name;
        }
        // change the button text¨


    }


}

[System.Serializable]
public class BattleEntities
{

    public enum Action { Attack, Magic, Item, Defend } // the possible actions that an entity can perform on their turn
    public Action BattleAction; // the action that the entity will perform on their turn

    public string Name;
    public int CurrentHealth;
    public int MaxHealth;
    public int CurrentMana;
    public int MaxMana;
    public int Strength;
    public int Defense;
    public int Magic;
    public int Initiative;
    public int Level;
    public bool IsPlayer;
    public BattleVisuals BattleVisuals;
    public int Target;

    public void SetEntityValues(string name, int currentHealth, int maxHealth, int currentMana, int maxMana, int strength, int defense, int magic, int initiative, int level, bool isPlayer)
    {
        Name = name;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
        CurrentMana = currentMana;
        MaxMana = maxMana;
        Strength = strength;
        Defense = defense;
        Magic = magic;
        Initiative = initiative;
        Level = level;
        IsPlayer = isPlayer;
    }

    public void SetTarget(int target)
    {
        Target = target;
    }

}
