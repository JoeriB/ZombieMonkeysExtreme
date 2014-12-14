﻿using UnityEngine;
using System.Collections;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Enemy Stats: Contains all information about an enemy such as Health and Sound info.
 */
public class EnemyStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth = 0;
    public AudioClip deathSound;

    PlayerStats player;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    public void ApplyDamage(int damage)
    {
        //TODO: animation???
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            player.IncrementKills();
            Dead();
        }
    }

    private void Dead()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(this.gameObject);
    }
}
