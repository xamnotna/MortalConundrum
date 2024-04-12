using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private bool magicAttack;

    [SerializeField]
    private float magicCost;

    [SerializeField]
    private float minAttackMultiplier;

    [SerializeField]
    private float maxAttackMultiplier;

    [SerializeField]
    private float minDefenseMultiplier;

    [SerializeField]
    private float maxDefenseMultiplier;

    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;
    private float xMagicNewScale;
    private Vector2 magicScale;

    private void Start()
    {
        magicScale = GameObject.Find("HeroMagicFill").GetComponent<RectTransform>().localScale;
    }

    public void Attack(GameObject victim)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        /* if (attackerStats.magic >= magicCost) // check if attacker has enough mana to perform magic attack
        {
            float multiplier = Random.Range(minAttackMultiplier, maxAttackMultiplier); // randomize attack multiplier
            attackerStats.updateMagicFill(magicCost); // update mana bar

            damage = multiplier * attackerStats.attack;
            if (magicAttack) // if magic attack, calculate damage based on magic stat
            {
                damage = multiplier * attackerStats.magic;
                attackerStats.magic = attackerStats.magic - magicCost;
            }
            float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier); // randomize defense multiplier
            damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense)); // calculate damage
            owner.GetComponent<Animator>().Play(animationName); // play attack animation
            targetStats.ReceiveDamage(damage); // apply damage to target
        } */

    }
}


