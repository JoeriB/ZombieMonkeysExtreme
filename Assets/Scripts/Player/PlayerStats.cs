using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Player Stats: Contains information about our player such as health, kills, infection etc..
 */
public class PlayerStats : MonoBehaviour
{
    public int currentKills = 0;
    public int maxHealth = 100;
    public int currentHealth = 0;
    public float deathDelay;

    private Text playerKillsText;
    private Text playerHealthText;
    private Text gameTimeText;

    private Slider playerHealthBarSlider;

    private GameObject uiPanel;
    private GameObject deathScreen;

    private float timeLeft;
    private bool isDead = false;

    private Animator animator;
    private EndGame endGame;

    void Start()
    {
        playerHealthText = GameObject.FindGameObjectWithTag(TagManager.playerHealthText).GetComponent<Text>();
        gameTimeText = GameObject.FindGameObjectWithTag(TagManager.gameTimeText).GetComponent<Text>();
        playerKillsText = GameObject.FindGameObjectWithTag(TagManager.playerKillsText).GetComponent<Text>();
        playerHealthBarSlider = GameObject.FindGameObjectWithTag(TagManager.playerHealthBar).GetComponent<Slider>();
        uiPanel = GameObject.FindGameObjectWithTag(TagManager.uiPanel);
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        playerHealthText.text = currentHealth + "/" + maxHealth;
        playerKillsText.text = "Kill Score: " + currentKills;
    }

    void Update()
    {
        timeLeft += Time.deltaTime;
        TimeSpan ts = TimeSpan.FromSeconds(timeLeft);

        gameTimeText.text = string.Format("Game Time: {0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
    }

    public void IncrementKills()
    {
        currentKills++;
        playerKillsText.text = "Kill Score: " + currentKills;
    }

    public void HandleIncomingDamage(int damage)
    {
        if (!isPlayerDead())
        {
            currentHealth -= damage;
            playerHealthText.text = currentHealth + "/" + maxHealth;
            playerHealthBarSlider.value = currentHealth;
            animator.SetTrigger(Animator.StringToHash("Hurt"));
        }
        if (currentHealth <= 0)
        {
            isDead = true;
            endGame.EndGame();
        }
    }

    public bool isPlayerDead()
    {
        return isDead;
    }
}
