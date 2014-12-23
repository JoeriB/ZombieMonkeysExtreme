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
        public int attackDamage;
        public float attackDistance;
        public float attackCooldown;
        public EnemyAttackStyle attackStyle;
    }
    public EnemyStats enemy;
    public SoundConfig sounds;

    private int currentHealth;
    private PlayerStats player;
    private float cooldownTimer;

    void Start()
    {
        currentHealth = enemy.maxHealth;
        player = GameObject.FindGameObjectWithTag(TagManager.player).GetComponent<PlayerStats>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }
    public void HandleCombat()
    {
        if (cooldownTimer >= enemy.attackCooldown && !player.isPlayerDead())
        {
            cooldownTimer = 0f;
            //Todo: Gooien met items? Melee systeem? Depends on zombie Type
            if (enemy.attackStyle == EnemyAttackStyle.MELEE)
                Debug.Log("DO Melee stuff");
            if (enemy.attackStyle == EnemyAttackStyle.RANGED)
                Debug.Log("Ranged stuff throw stuff");
            player.HandleIncomingDamage(enemy.attackDamage);
        }
    }

    public void HandleIncomingDamage(int damage)
    {
        if (sounds.hurtShound != null)
        {
            //TODO: Hurt/Death Animation
            //Apply damage
            currentHealth -= damage;
            AudioSource.PlayClipAtPoint(sounds.hurtShound, transform.position);
            //Check if the enemy is dead
            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }
    }

    private void HandleDeath()
    {
        player.GetComponent<PlayerStats>().IncrementKills();
        AudioSource.PlayClipAtPoint(sounds.deathSound, transform.position);
        Destroy(this.gameObject);
    }
}
