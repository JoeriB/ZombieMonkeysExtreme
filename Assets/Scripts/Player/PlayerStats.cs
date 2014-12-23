using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Player Stats: Contains information about our player such as health, kills, infection etc..
 */
public class PlayerStats : MonoBehaviour
{
    public int currentKills = 0;
    public int maxHealth = 100;
    public int currentHealth = 0;

    [Serializable]
    public class HUD
    {
        [HideInInspector]
        public Text playerKillsText;
        [HideInInspector]
        public Text playerHealthText;
        [HideInInspector]
        public Text gameTimeText;
        [HideInInspector]
        public Slider playerHealthBarSlider;
    }
    [Serializable]
    public class Sound
    {
        public AudioClip hurtSound;
        public AudioClip deathSound;
    }

    public HUD hud;
    public Sound sound;

    private float timeInGame;
    private bool isDead = false;
    private Animator animator;
    private string characterName;

    void Start()
    {
        hud.playerHealthText = GameObject.FindGameObjectWithTag(TagManager.playerHealthText).GetComponent<Text>();
        hud.gameTimeText = GameObject.FindGameObjectWithTag(TagManager.gameTimeText).GetComponent<Text>();
        hud.playerKillsText = GameObject.FindGameObjectWithTag(TagManager.playerKillsText).GetComponent<Text>();
        hud.playerHealthBarSlider = GameObject.FindGameObjectWithTag(TagManager.playerHealthBar).GetComponent<Slider>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        hud.playerHealthText.text = currentHealth + "/" + maxHealth;
        hud.playerKillsText.text = "Kill Score: " + currentKills;
    }

    void Update()
    {
        timeInGame += Time.deltaTime;
        TimeSpan ts = TimeSpan.FromSeconds(timeInGame);

        hud.gameTimeText.text = string.Format("Game Time: {0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
    }

    public void IncrementKills()
    {
        currentKills++;
        hud.playerKillsText.text = "Kill Score: " + currentKills;
    }

    public void HandleIncomingDamage(int damage)
    {
        if (!isPlayerDead())
        {
            currentHealth -= damage;
            hud.playerHealthText.text = currentHealth + "/" + maxHealth;
            hud.playerHealthBarSlider.value = currentHealth;
            animator.SetTrigger(Animator.StringToHash("Hurt"));
            AudioSource.PlayClipAtPoint(sound.hurtSound, transform.position);

        }
        if (currentHealth <= 0)
        {
            isDead = true;
            AudioSource.PlayClipAtPoint(sound.deathSound, transform.position);
            StartCoroutine(GetComponent<EndGame>().EndTheGame());
        }
    }

    public bool isPlayerDead()
    {
        return isDead;
    }

    public string GetEndGameText()
    {
        TimeSpan ts = TimeSpan.FromSeconds(timeInGame);
        StringBuilder sb = new StringBuilder();
        sb.Append("Game Over! Thank you for playing ").Append(TagManager.gameName).Append("!").Append(Environment.NewLine);
        sb.Append(Environment.NewLine);
        sb.Append("Played as ").Append(characterName).Append(Environment.NewLine);
        string time = string.Format("{0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
        sb.Append("Total Game Time: ").Append(time).Append(Environment.NewLine);
        sb.Append("Total Zombies Killed: ").Append(currentKills).Append(Environment.NewLine);
        sb.Append("Items collected: ").Append(GetComponent<Inventory>().getItemCount()).Append("/").Append(GetComponent<Inventory>().slots.Length).Append(Environment.NewLine);
        return sb.ToString();
    }

    public void SetCharacterName(string name)
    {
        characterName = name;
    }
}
