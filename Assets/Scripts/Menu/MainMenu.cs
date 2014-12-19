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

    public void StartGame()
    {
        characterSelect.SetActive(true);
        GetComponent<CharacterMenu>().InitiateSelectCharacter();
        mainMenu.SetActive(false);
    }

    public void ShowOptions()
    {
        //TODO: Options
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
