using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Options Menu: handles in/outside game options.
 */
public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    [SerializeField]
    private GameObject menuVolumeButton;
    [SerializeField]
    private GameObject gameVolumeButton;
    [SerializeField]
    private GameObject sensitivityButton;
    [SerializeField]
    private GameObject sensitivityErrorText;
    [SerializeField]
    private GameObject gameVolumeErrorText;

    [SerializeField]
    private AudioSource menuAudio;
    [SerializeField]
    private AudioSource inGameAudio;

    void Start()
    {
        sensitivityErrorText.GetComponent<Text>().enabled = false;
    }

    public void AdjustMenuVolume(float volume)
    {
        menuAudio.volume = volume / 10f;
        menuVolumeButton.GetComponentInChildren<Text>().text = volume.ToString();
    }
    public void AdjustGameVolume(float volume)
    {
        GameObject player = GameObject.FindGameObjectWithTag(TagManager.player);
        if (player != null)
        {
            inGameAudio.volume = volume / 10f;
            gameVolumeButton.GetComponentInChildren<Text>().text = volume.ToString();
            gameVolumeErrorText.GetComponent<Text>().enabled = false;
        }
        else
            gameVolumeErrorText.GetComponent<Text>().enabled = true;
    }

    public void AdjustMouseSensitivty(float sensitivty)
    {
        GameObject player = GameObject.FindGameObjectWithTag(TagManager.player);
        if (player != null)
        {
            sensitivityErrorText.GetComponent<Text>().enabled = false;
            sensitivityButton.GetComponentInChildren<Text>().text = sensitivty.ToString();
            player.GetComponent<FPSMouseMovement>().sensitivity = sensitivty;
        }
        else
        {
            sensitivityErrorText.GetComponent<Text>().enabled = true;
        }
    }

    public void SaveSettings()
    {
        optionsMenu.SetActive(false);
    }
}
