using UnityEngine;
using System.Collections;
using System;

public class EnemyCombat : MonoBehaviour
{
    [Serializable]
    public class SoundConfig
    {
        public AudioClip deathSound;
        public AudioClip hurtShound;
        public AudioClip attackSound;
    }

    [Serializable]
    public class EnemyStats
    {
        public int maxHealth;
        public float attackDistance;
        public EnemyAttackStyle attackStyle;
    }
    public EnemyStats enemy;
    public SoundConfig sounds;

    private int currentHealth;
    private GameObject player;
    // Use this for initialization
    void Start()
    {
        currentHealth = enemy.maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void HandleCombat()
    {
        //Todo: Gooien met items? Melee systeem? Depends on zombie Type
        if (enemy.attackStyle == EnemyAttackStyle.MELEE)
            Debug.Log("DO Melee stuff");
        if (enemy.attackStyle == EnemyAttackStyle.RANGED)
            Debug.Log("Ranged stuff throw stuff");
        Debug.Log(player);
    }

    public void HandleIncomingDamage(int damage)
    {
        //Play our hurt sound + animation
        AudioSource.PlayClipAtPoint(sounds.hurtShound, transform.position);
        //TODO: Hurt Animation
        //Apply damage
        currentHealth -= damage;
        //Check if the enemy is dead
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        player.GetComponent<PlayerStats>().IncrementKills();
        //TODO: Death animation
        AudioSource.PlayClipAtPoint(sounds.attackSound, transform.position);
        Destroy(this.gameObject);
    }
}
