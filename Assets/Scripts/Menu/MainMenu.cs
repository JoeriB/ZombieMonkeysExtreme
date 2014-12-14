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
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

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
        mainMenu.SetActive(false);
    }
}
