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
    public int playerHealth = 100;
    public int currentHealth = 0;
    public Text playerHealthText;
    public Text playerKillsText;

    private float timeLeft;
    private Text gameTimeText;
    // Use this for initialization
    void Start()
    {
        currentHealth = playerHealth;
        playerHealthText.text = currentHealth + "/" + playerHealth;
        playerKillsText.text = "Kill Score: " + currentKills;
        gameTimeText = GameObject.FindGameObjectWithTag("GameTimeText").GetComponent<Text>();
    }

    void Update()
    {
        timeLeft += Time.deltaTime;
        TimeSpan ts = TimeSpan.FromSeconds(timeLeft);

        gameTimeText.text = string.Format("Game Time: {0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
    }

    //Script oproepen in npc combat.
    public void IncrementKills()
    {
        currentKills++;
        playerKillsText.text = "Kill Score: " + currentKills;
    }
}
