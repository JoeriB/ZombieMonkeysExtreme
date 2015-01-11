using UnityEngine;
using System.Collections;
using System;
/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Enemy Combat: handles our Zombie Combat: Attacking/Death
 */
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

    [SerializeField]
    private int currentHealth;
    private PlayerStats player;
    private float cooldownTimer;
    private Animator animator;

    void Start()
    {
        currentHealth = enemy.maxHealth;
        player = GameObject.FindGameObjectWithTag(TagManager.player).GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
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

            if (enemy.attackStyle == EnemyAttackStyle.MELEE)
                PlayAnimation(Animator.StringToHash("MeleeAttack"));
            if (enemy.attackStyle == EnemyAttackStyle.RANGED)
                Debug.Log("Ranged stuff throw stuff");
            if (!player.isPlayerDead())
                StartCoroutine(player.HandleIncomingDamage(enemy.attackDamage));
        }
    }

    public void HandleIncomingDamage(int damage)
    {
        if (sounds.hurtShound != null)
        {
            //Apply damage
            currentHealth -= damage;
            PlayAnimation(Animator.StringToHash("Hurt"));
            //Check if the enemy is dead
            if (currentHealth <= 0)
                HandleDeath();
            else
                AudioSource.PlayClipAtPoint(sounds.hurtShound, transform.position);
        }
    }

    private void HandleDeath()
    {
        player.GetComponent<PlayerStats>().IncrementKills();
        AudioSource.PlayClipAtPoint(sounds.deathSound, transform.position);
        Destroy(this.gameObject);
    }

    private void PlayAnimation(int animationHash)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).nameHash.Equals(animationHash))
            animator.SetTrigger(animationHash);
    }
}
