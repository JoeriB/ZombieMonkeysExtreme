using UnityEngine;
using System.Collections;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Main Menu
 */
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject characterSelect;

    private AudioSource audio;

    void Start()
    {
        audio.GetComponent<AudioSource>();
    }
    public void StartGame()
    {
        characterSelect.SetActive(true);
        GetComponent<CharacterMenu>().InitiateSelectCharacter();
        mainMenu.SetActive(false);
    }

    public void ShowOptions()
    {
        GetComponent<OptionsMenu>().optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
