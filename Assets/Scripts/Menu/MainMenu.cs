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

    public void ClickButton(string text)
    {
        if (text.Equals("start"))
        {
            StartGame();
        }
        if (text.Equals("credits"))
        {
            //TODO: credits
        }
        if (text.Equals("quit"))
        {
            Application.Quit();
        }
    }

    private void StartGame()
    {
        characterSelect.SetActive(true);
        GetComponent<CharacterMenu>().InitiateSelectCharacter();
        mainMenu.SetActive(false);
    }
}
