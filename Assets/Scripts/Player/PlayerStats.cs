using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    // Use this for initialization
    void Start()
    {
        currentHealth = playerHealth;
        playerHealthText.text = currentHealth + "/" + playerHealth;
        playerKillsText.text = "Kill Score: " + currentKills;
    }

    //Script oproepen in npc combat.
    public void IncrementKills()
    {
        currentKills++;
        playerKillsText.text = "Kill Score: " + currentKills;
    }
}
