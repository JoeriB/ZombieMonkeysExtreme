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
            //Todo: Gooien met items? Melee systeem? Depends on zombie Type
            if (enemy.attackStyle == EnemyAttackStyle.MELEE)
                PlayAnimation(Animator.StringToHash("MeleeAttack"));
            if (enemy.attackStyle == EnemyAttackStyle.RANGED)
                Debug.Log("Ranged stuff throw stuff");
            StartCoroutine(player.HandleIncomingDamage(enemy.attackDamage));
        }
    }

    public void HandleIncomingDamage(int damage)
    {
        if (sounds.hurtShound != null)
        {
            //TODO: Hurt/Death Animation
            //Apply damage
            currentHealth -= damage;
            //Check if the enemy is dead
            if (currentHealth <= 0)
                HandleDeath();
            //Play hurt sound
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
