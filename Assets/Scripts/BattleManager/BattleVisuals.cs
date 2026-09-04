using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class BattleVisuals : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI levelText;

    private int currentHealth;
    private int currentMana;
    private int maxMana;
    private int maxHealth;
    private int level;

    private Animator anim;

    private const string LEVEL_ABB = "Lvl: ";

    private const string IS_ATTACK_PARAM = "IsAttack";
    private const string IS_HIT_PARAM = "IsHit";
    private const string IS_RANGE_PARAM = "IsRange";
    private const string IS_DEAD_PARAM = "IsDead";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void SetStartingValues(int currentHealth, int maxHealth, int currentMana, int maxMana, int level)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.currentMana = currentMana;
        this.maxMana = maxMana;
        this.level = level;
        levelText.text = LEVEL_ABB + level.ToString();
        UpdateHealthBar();
    }

    public void ChangeHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
        // if health is 0 -> play death animation -> Destroy our battle visual 
        if (currentHealth <= 0)
        {
            PlayDeathAnimation();
            Destroy(gameObject, 1f);
        }
        UpdateHealthBar();

    }

    public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void PlayAttackAnimation()
    {
        anim.SetTrigger(IS_ATTACK_PARAM);
    }

    public void PlayHitAnimation()
    {
        anim.SetTrigger(IS_HIT_PARAM);
    }

    public void PlayRangeAnimation()
    {
        anim.SetTrigger(IS_RANGE_PARAM);
    }

    public void PlayDeathAnimation()
    {
        anim.SetTrigger(IS_DEAD_PARAM);
    }

}
